using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HerrOber2
{
    static public class Utils
    {
        public static T FromXML<T>(string xml) where T : class
        {
            object obj = default(T);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xml))
                {
                    T data = serializer.Deserialize(reader) as T;
                    return data;
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
            return obj as T;
        }

        public static string ToXml(object obj)
        {   
            XmlSerializer serializer = new XmlSerializer(obj.GetType());           

            StringBuilder builder = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(builder, settings);            

            serializer.Serialize(writer, obj);
            writer.Flush();
            writer.Close();

            string xml = builder.ToString();
            return xml;
        }
    }
}
