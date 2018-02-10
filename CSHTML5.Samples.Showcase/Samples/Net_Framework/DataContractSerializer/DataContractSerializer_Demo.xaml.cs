using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
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
    public partial class DataContractSerializer_Demo : UserControl
    {
        private ClassToSerialize _classToSerialize;

        public DataContractSerializer_Demo()
        {
            this.InitializeComponent();

            _classToSerialize = new ClassToSerialize()
            {
                TextField = "Some Text",
                DateField = DateTime.Now,
                GuidField = Guid.NewGuid(),
                BooleanField = true
            };
            SerializationSourcePanel.DataContext = _classToSerialize;
        }


        [DataContract]
        public class ClassToSerialize
        {
            public string TextField { get; set; }
            public DateTime DateField { get; set; }
            public Guid GuidField { get; set; }
            public bool BooleanField { get; set; }
        }

        void ButtonSerializeDeserialize_Click(object sender, RoutedEventArgs e)
        {
            // Serialize:
            var dataContractSerializer = new DataContractSerializer(typeof(ClassToSerialize));
            var xml = dataContractSerializer.SerializeToString(_classToSerialize);

            // Display the result of the serialization:
            MessageBox.Show("Result of the serialization:" + Environment.NewLine + Environment.NewLine + xml);

            // Deserialize:
            dataContractSerializer = new DataContractSerializer(typeof(ClassToSerialize));
            ClassToSerialize deserializedObject = (ClassToSerialize)dataContractSerializer.DeserializeFromString(xml);

            // Display the result of the deserialization:
            SerializationDestinationPanel.DataContext = _classToSerialize;
        }
    }
}
