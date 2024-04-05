using System.Windows.Threading;

namespace OpenSilverBlazorServer.Wpf
{
    public class DispatcherWrapper
    {
        private readonly Dispatcher _current;

        public DispatcherWrapper()
        {
            _current = Dispatcher.CurrentDispatcher;
        }

        public void BeginInvokeShutdown()
        {
            _current.BeginInvokeShutdown(DispatcherPriority.Normal);
        }

        public void BeginInvoke(Delegate method)
        {
            _current.BeginInvoke(method);
        }

        public void Run()
        {
            Dispatcher.Run();
        }

        public void Invoke(Action action, TimeSpan timeOut)
        {
            _current.Invoke(action, timeOut);
        }

        public bool CheckAccess()
        {
            return _current.CheckAccess();
        }
    }
}
