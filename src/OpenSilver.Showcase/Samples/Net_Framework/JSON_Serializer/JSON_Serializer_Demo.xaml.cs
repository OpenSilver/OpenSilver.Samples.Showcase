using OpenSilver.Showcase.Search;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    [SearchKeywords("JSON", "serialization", "deserialization", "serialize")]
    public partial class JSON_Serializer_Demo : UserControl
    {
        private readonly Product _product;
        private string _json;

        public JSON_Serializer_Demo()
        {
            InitializeComponent();

            _product = new Product()
            {
                Name = "TestProduct",
                ProductType = ProductType.B2C,
                Price = 12.50,
                Count = 341,
                IsAvailable = true,
                Sizes = ["Small", "Medium", "Large"],
                Features =
                [
                    new Feature() { Name = "TestFeature1" },
                    new Feature() { Name = "TestFeature2" },
                    new Feature() { Name = "TestFeature3" }
                ],
                ReleaseDate = DateTime.Now
            };
        }

        private void Button_Click_Serialization(object sender, RoutedEventArgs e)
        {
            _json = JsonSerializer.Serialize(_product, new JsonSerializerOptions { WriteIndented = true });

            MessageBox.Show(_json);

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

        private void Button_Click_StronglyTypedDeserialization(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_json))
            {
                Product deserializedProduct = JsonSerializer.Deserialize<Product>(_json);

                MessageBox.Show("Name of the second feature: " + deserializedProduct.Features[1].Name +
                "\nName of the third available size: " + deserializedProduct.Sizes[2] +
                "\nRelease date: " + deserializedProduct.ReleaseDate.ToString());

                // Expected Result: "Name of the second feature: TestFeature2"
                //                  "Name of the third available size: Large"
                //                  "Release date: 2017-04-10T16:26:41.754Z"
            }
            else
            {
                MessageBox.Show("Please click on the Serialize button first.");
            }
        }

        private void Button_Click_DynamicDeserialization(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_json))
            {
                var deserializedObject = JsonDocument.Parse(_json).RootElement;

                MessageBox.Show("Product name: " + deserializedObject.GetProperty("Name").GetString() +
                "\nName of the second feature: " + deserializedObject.GetProperty("Features")[1].GetProperty("Name").GetString() +
                "\nName of the third available size: " + deserializedObject.GetProperty("Sizes")[2].GetString());

                // Expected Result: "Product name: TestProduct"
                //                  "Name of the second feature: TestFeature2"
                //                  "Name of the third available size: Large"
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
    }
}