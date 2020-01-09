using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace CSHTML5.Samples.Showcase
{
    public partial class JSON_Serializer_Demo : UserControl
    {
        private string _json;
        private Product _product;

        public JSON_Serializer_Demo()
        {
            this.InitializeComponent();

            _product = new Product()
            {
                Name = "TestProduct",
                ProductType = ProductType.B2C,
                Price = 12.50,
                Count = 341,
                IsAvailable = true,
                Sizes = new string[] { "Small", "Medium", "Large" },
                Features = new List<Feature>()
                {
                    new Feature() { Name = "TestFeature1" },
                    new Feature() { Name = "TestFeature2" },
                    new Feature() { Name = "TestFeature3" }
                },
                ReleaseDate = DateTime.Now
            };
        }

        private void Button_Click_Serialization(object sender, RoutedEventArgs e)
        {
            // Serialize:
            _json = JsonConvert.SerializeObject(_product);

            // Indent:
            string indentedJson = _json.Replace(",", ",\n");

            // Display the result:
            MessageBox.Show(indentedJson);

            /*
            // Expected Result:
            {  
               "Name":"TestProduct",
               "ProductType":"B2C",
               "Price":12.5,
               "Count":341,
               "IsAvailable":true,
               "Sizes":[  
                  "Small",
                  "Medium",
                  "Large"
               ],
               "Features":[  
                  {  
                     "Name":"TestFeature1"
                  },
                  {  
                     "Name":"TestFeature2"
                  },
                  {  
                     "Name":"TestFeature3"
                  }
               ],
               "ReleaseDate":"2017-04-10T16:26:41.754Z"
            }
            */
        }

        private async void Button_Click_StronglyTypedDeserialization(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_json))
            {
                Product deserializedProduct = await JsonConvert.DeserializeObject<Product>(_json);

                MessageBox.Show("Name of the second feature: " + deserializedProduct.Features[1].Name);
                MessageBox.Show("Name of the third available size: " + deserializedProduct.Sizes[2]);
                MessageBox.Show("Release date: " + deserializedProduct.ReleaseDate.ToString());

                // Expected Result: "Name of the second feature: TestFeature2"
                // Expected Result: "Name of the third available size: Large"
            }
            else
            {
                MessageBox.Show("Please click on the Serialize button first.");
            }
        }

        private async void Button_Click_DynamicDeserialization(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_json))
            {
                IJsonType deserializedObject = await JsonConvert.DeserializeObject(_json);

                MessageBox.Show("Product name: " + deserializedObject["Name"].Value.ToString());
                MessageBox.Show("Name of the second feature: " + deserializedObject["Features"][1]["Name"].Value.ToString());
                MessageBox.Show("Name of the third available size: " + deserializedObject["Sizes"][2].Value.ToString());

                // Expected Result: "Product name: TestProduct"
                // Expected Result: "Name of the second feature: TestFeature2"
                // Expected Result: "Name of the third available size: Large"
            }
            else
            {
                MessageBox.Show("Please click on the Serialize button first.");
            }
        }

        public class Product
        {
            public string Name { get; set; }
            public ProductType ProductType { get; set; }
            public double Price { get; set; }
            public int Count { get; set; }
            public bool IsAvailable { get; set; }
            public string[] Sizes { get; set; }
            public List<Feature> Features { get; set; }
            public DateTime ReleaseDate { get; set; }
        }

        public class Feature
        {
            public string Name { get; set; }
        }

        public enum ProductType
        {
            B2B, B2C
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "JSON_Serializer_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/JSON_Serializer/JSON_Serializer_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "JSON_Serializer_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/JSON_Serializer/JSON_Serializer_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "JsonConvert.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/JSON_Serializer/JsonConvert.cs"
                }
            });
        }

    }
}