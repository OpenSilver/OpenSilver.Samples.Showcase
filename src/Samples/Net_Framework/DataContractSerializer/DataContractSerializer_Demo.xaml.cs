using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

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
            [DataMember]
            public string TextField { get; set; }
            [DataMember]
            public DateTime DateField { get; set; }
            [DataMember]
            public Guid GuidField { get; set; }
            [DataMember]
            public bool BooleanField { get; set; }
        }

        void ButtonSerializeDeserialize_Click(object sender, RoutedEventArgs e)
        {
            // Serialize:
            var dataContractSerializer = new DataContractSerializer(typeof(ClassToSerialize));
#if OPENSILVER
            string xml = null;
            MemoryStream stream1 = new MemoryStream();
            dataContractSerializer.WriteObject(stream1, _classToSerialize);

            stream1.Seek(0, SeekOrigin.Begin);
            using (var streamReader = new StreamReader(stream1))
            {
                xml = streamReader.ReadToEnd();
            }
#else
            var xml = dataContractSerializer.SerializeToString(_classToSerialize);
#endif

            // Display the result of the serialization:
            MessageBox.Show("Result of the serialization:" + Environment.NewLine + Environment.NewLine + xml);

            // Deserialize:
            dataContractSerializer = new DataContractSerializer(typeof(ClassToSerialize));
#if OPENSILVER
            ClassToSerialize deserializedObject = (ClassToSerialize)dataContractSerializer.ReadObject(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml)));
#else
            ClassToSerialize deserializedObject = (ClassToSerialize)dataContractSerializer.DeserializeFromString(xml); 
#endif

            // Display the result of the deserialization:
            SerializationDestinationPanel.DataContext = deserializedObject;
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DataContractSerializer_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DataContractSerializer_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml.cs"
                }
            });
        }

    }
}
