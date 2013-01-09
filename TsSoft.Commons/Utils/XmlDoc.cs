using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TsSoft.Commons.Utils
{
    public class XmlDoc
    {
        private XmlDocument xmlDoc = new XmlDocument();
        private string configFile;

        public XmlDoc(string configFile)
        {
            this.configFile = configFile;
            using (Stream stream = new FileStream(configFile, FileMode.Open))
            {
                xmlDoc.Load(stream);
                stream.Close();
            }
        }

        public void CreateNode(string xpath, string value, Dictionary<string, string> attributes)
        {
            XmlNode newNode = null;
            var pathParts = xpath.Split('/');
            var xmlPath = string.Empty;
            XmlNode parentNode = null;
            for (var i = 0; i < pathParts.Length; i++)
            {
                var pathPart = pathParts[i];
                var quoteIndex = pathPart.IndexOf('[');
                var nodeName = quoteIndex >= 0 ? pathPart.Substring(0, quoteIndex) : pathPart;
                var lastElement = i == pathParts.Length - 1;

                xmlPath = xmlPath + (xmlPath.Length > 0 ? "/" : string.Empty) + pathPart;
                var existingNode = xmlDoc.SelectSingleNode(xmlPath);
                if (lastElement || existingNode == null)
                {
                    parentNode = parentNode ?? xmlDoc.DocumentElement;
                    newNode = xmlDoc.CreateElement(nodeName);
                    if (parentNode != null)
                    {
                        parentNode.AppendChild(newNode);
                    }
                    else
                    {
                        xmlDoc.AppendChild(newNode);
                    }
                    if (lastElement && !string.IsNullOrEmpty(value))
                    {
                        newNode.InnerText = value;
                    }

                    parentNode = newNode;
                }
                else
                {
                    parentNode = existingNode;
                }
            }

            if (newNode != null && attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    var a = (XmlAttribute)newNode.Attributes.GetNamedItem(attribute.Key);
                    if (a == null)
                    {
                        a = xmlDoc.CreateAttribute(attribute.Key);
                        newNode.Attributes.Append(a);
                    }

                    a.Value = attribute.Value;
                }
            }
        }

        public void UpdateText(string xpath, string value)
        {
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                xmlNode.InnerText = value;
            }
        }

        public void UpdateAttribute(string xpath, string value)
        {
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                if (xmlNode is XmlAttribute)
                {
                    (xmlNode as XmlAttribute).Value = value;
                }
            }
            else
            {
                var quoteIndex = xpath.LastIndexOf('/');
                if (quoteIndex >= 0)
                {
                    var attributeName = xpath.Substring(quoteIndex + 2);
                    var elementPath = xpath.Substring(0, quoteIndex);
                    var elementNode = xmlDoc.SelectSingleNode(elementPath);
                    if (elementNode != null)
                    {
                        var attribute = xmlDoc.CreateAttribute(attributeName);
                        attribute.Value = attribute.Value;
                        elementNode.Attributes.Append(attribute);
                    }
                }
            }
        }

        public void RemoveNode(string xpath)
        {
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            xmlNode.RemoveAll();
        }

        public void Save()
        {
            xmlDoc.Save(configFile);
        }
    }
}