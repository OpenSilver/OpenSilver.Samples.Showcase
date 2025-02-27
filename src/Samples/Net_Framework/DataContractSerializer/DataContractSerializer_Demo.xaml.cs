using OpenSilver.Samples.Showcase.Search;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("serialization", "deserialization", "data contract", "XML", "data exchange")]
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
    }
}
