using Android.App;
using Android.Content;
using Android.Provider;
using Android.Webkit;
using Android.Widget;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace OpenSilver.Showcase.MauiHybrid;

public class AndroidWebViewHandler : BlazorWebViewHandler
{
    protected override void ConnectHandler(Android.Webkit.WebView webView)
    {
        webView.Settings.SetSupportMultipleWindows(false);
        webView.Settings.TextZoom = 100; // Set text zoom to 100% to avoid scaling issues

        webView.SetDownloadListener(new FileDownloadListener());

        base.ConnectHandler(webView);
    }

    public class FileDownloadListener : Java.Lang.Object, IDownloadListener
    {
        public void OnDownloadStart(string? url, string? userAgent, string? contentDisposition, string? mimeType, long contentLength)
        {
            var fileName = URLUtil.GuessFileName(url, contentDisposition, mimeType);
            var uri = Android.Net.Uri.Parse(url);

            if (uri?.Scheme == "data")
            {
                DownloadBase64(url!, fileName, mimeType);
                return;
            }

            try
            {
                var request = new DownloadManager.Request(uri);
                request.SetTitle(fileName);
                request.SetDescription($"Downloading {fileName}...");
                request.SetMimeType(mimeType);
                request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
                request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, fileName);

                var downloadManager = Platform.CurrentActivity?.GetSystemService(Context.DownloadService) as DownloadManager;
                var downloadId = downloadManager?.Enqueue(request);
            }
            catch (Java.Lang.Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, $"Unable to download file: {ex.Message}", ToastLength.Long)?.Show();
            }
        }

        private static void DownloadBase64(string base64Data, string? fileName, string? mimeType)
        {
            try
            {
                // Remove "data:*;base64," prefix if present
                var base64Index = base64Data.IndexOf("base64,");
                if (base64Index >= 0)
                    base64Data = base64Data.Substring(base64Index + 7);

                byte[] fileBytes = Convert.FromBase64String(base64Data);

                var resolver = Android.App.Application.Context.ContentResolver;

                var contentValues = new ContentValues();
                contentValues.Put(MediaStore.IMediaColumns.DisplayName, fileName);
                contentValues.Put(MediaStore.IMediaColumns.MimeType, mimeType);
                contentValues.Put(MediaStore.IMediaColumns.RelativePath, Android.OS.Environment.DirectoryDownloads);

                if (resolver != null)
                {
                    var uri = resolver.Insert(MediaStore.Downloads.ExternalContentUri, contentValues);

                    if (uri != null)
                    {
                        using var outputStream = resolver.OpenOutputStream(uri);
                        if (outputStream != null)
                        {
                            outputStream.Write(fileBytes, 0, fileBytes.Length);
                            outputStream.Flush();
                            Toast.MakeText(Android.App.Application.Context, $"Saved {fileName} to Downloads folder", ToastLength.Long)?.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, $"Unable to download file: {ex.Message}", ToastLength.Long)?.Show();
            }
        }
    }
}
