using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IntegracaoCRF
{
    public class Serializer
    {
        public T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }

        public T DeserializeObject<T>(string objString)
        {
            Object obj = null;
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(objString)))
            {
                XmlTextReader xtr = new XmlTextReader(memoryStream);
                obj = xs.Deserialize(xtr);
            }
            return (T)obj;
        }

        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetString(characters);
        }

        private static byte[] StringToUTF8ByteArray(string xmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(xmlString);

        }
    }
}
