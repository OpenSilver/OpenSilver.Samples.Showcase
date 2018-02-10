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
using CSHTML5.Extensions.Json;

namespace CSHTML5.Samples.Showcase
{
    public partial class JSON_Serializer_Demo : UserControl
    {
        private bool _serialized;
        private string _json;
        private Product _product;

        public JSON_Serializer_Demo()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        #region Tests Class & Methods

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

        #endregion

        #region Events

        //----------------------------------
        // Deserialization (strongly-typed):
        //----------------------------------
        private async void Button_Click_StronglyTypedDeserialization(object sender, RoutedEventArgs e)
        {
            Product deserializedProduct = await JsonConvert.DeserializeObject<Product>(_json);

            MessageBox.Show("Name of the second feature: " + deserializedProduct.Features[1].Name);
            MessageBox.Show("Name of the third available size: " + deserializedProduct.Sizes[2]);
            MessageBox.Show("Release date: " + deserializedProduct.ReleaseDate.ToString());

            TextContent.Text = _json;

            // Result: "Name of the second feature: TestFeature2"
            // Result: "Name of the third available size: Large"
        }

        //---------------
        // Serialization:
        //---------------
        private void Button_Click_Serialization(object sender, RoutedEventArgs e)
        {
            _json = JsonConvert.SerializeObject(_product);

            TextContent.Text = CodeIndentationAfterComas(_json);

            // Result:
            /*
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

        //---------------------------
        // Deserialization (dynamic):
        //---------------------------
        private async void Button_Click_DynamicDeserialization(object sender, RoutedEventArgs e)
        {
            IJsonType deserializedObject = await JsonConvert.DeserializeObject(_json);

            MessageBox.Show("Product name: " + deserializedObject["Name"].Value.ToString());
            MessageBox.Show("Name of the second feature: " + deserializedObject["Features"][1]["Name"].Value.ToString());
            MessageBox.Show("Name of the third available size: " + deserializedObject["Sizes"][2].Value.ToString());

            TextContent.Text = _json;

            // Result: "Product name: TestProduct"
            // Result: "Name of the second feature: TestFeature2"
            // Result: "Name of the third available size: Large"

        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
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

        #endregion

        #region Utilities

        private string CodeIndentationAfterComas(string text)
        {
            return text.Replace(",", ",\n");
        }

        #endregion
    }
}