using AppBlocks.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace AppBlocks.Models
{
    public static class Common
    {
        public static string GetUrl(string url)
        {
            var content = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ServerCertificateValidationCallback = (message, cert, chain, errors) => { return true; };
                // response.GetResponseStream();
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    //_logger?.LogInformation($"HttpStatusCode:{response.StatusCode}");
                    var responseValue = string.Empty;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = $"{typeof(Item).FullName} ERROR: Request failed. Received HTTP {response.StatusCode}";
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }

                    content = responseValue;
                }
            }
            catch (Exception exception)
            {
                //_logger?.LogInformation($"{nameof(Item)}.FromUri({uri}) ERROR:{exception.Message} {exception}");
                Trace.WriteLine($"{nameof(Item)}.GetUrl({url}) ERROR:{exception.Message} {exception}");
            }
            return content;
        }
        /// <summary>
        /// FromFile
        /// </summary>
        public static string FromFile(string filePathOrId, string typeName = "Item")
        {
            Trace.WriteLine($"{typeof(Item).Name}.Read({filePathOrId}) started:{DateTime.Now.ToShortTimeString()}");

            if (File.Exists(filePathOrId))
            {
                Trace.WriteLine($"{typeof(Item).Name}.Read({filePathOrId}) found:{filePathOrId}");
                return System.IO.File.ReadAllText(filePathOrId);
            }

            var filePath = GetFilepath(filePathOrId, typeName);
            if (File.Exists(filePath))
            {
                Trace.WriteLine($"{typeof(Item).Name}.Read({filePathOrId}) found:{filePath}");
                return File.ReadAllText(filePath);
            }
            Trace.WriteLine($"{typeof(Item).Name}.Read({filePathOrId}):no file found");
            return null;
        }

        public static T FromXml<T>(string xml, string xslt) where T : Item
        {
            Item results;
            var serializer = new XmlSerializer(typeof(Item));
            XmlDocument xmlDocument = new XmlDocument();
            StringBuilder output = new StringBuilder();
            XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
            XmlWriterSettings writerSettings = new XmlWriterSettings { OmitXmlDeclaration = true };
            xmlDocument.LoadXml(xml);
            if (xml.Contains("<rss") && string.IsNullOrEmpty(xslt))
            {
                //xslt = Children?.FirstOrDefault(i => i.Name.EndsWith("XSL"))?.Data ?? string.Empty;
                xslt = "<?xml version=\"1.0\" encoding=\"utf-8\"?><xsl:stylesheet version=\"3.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\"><xsl:output method=\"xml\" version=\"1.0\" encoding=\"UTF-8\" indent=\"yes\"/><xsl:template match=\"/\"><?xml version=\"1.0\" encoding=\"utf-8\"?><Item xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Id>test</Id><Name>test</Name><Path>test</Path><Title>test</Title><TypeId>Item</TypeId><Created>2022-03-21T17:53:07.5104663-05:00</Created><Edited>2022-03-21T17:53:07.5104663-05:00</Edited></Item>";
            }
            if (xslt != null)
            {
                xslCompiledTransform.Load(xslt);

                using (XmlWriter transformedData = XmlWriter.Create(output, writerSettings))
                {
                    xslCompiledTransform.Transform(xmlDocument, transformedData);
                    using (StringReader xmlString = new StringReader(output.ToString()))
                    {
                        results = (Item)serializer.Deserialize(xmlString);
                    }
                }
            } else
            {
                using (TextReader reader = new StringReader(xml))
                {
                    results = (Item)serializer.Deserialize(reader);
                }
            }
            return (T)results;
        }

        public static T FromXmlList<T>(string xml, string xslt = null) where T : List<Item>
        {
            var results = new List<Item>();

            var file = FromFile(xml);
            if (!string.IsNullOrEmpty(file)) xml = file;

            file = FromFile(xslt);
            if (!string.IsNullOrEmpty(file)) xslt = file;

            if (xml.Contains("<rss") && string.IsNullOrEmpty(xslt))
            {
                //xslt = Children?.FirstOrDefault(i => i.Name.EndsWith("XSL"))?.Data ?? string.Empty;
                //var itemXslt = "";// "<Item xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Id>test</Id><Name>test</Name><Path>test</Path><Title>test</Title><TypeId>Item</TypeId><Created>2022-03-21T17:53:07.5104663-05:00</Created><Edited>2022-03-21T17:53:07.5104663-05:00</Edited></Item>";
                var itemXslt = "<xsl:for-each select=\"channel/item\"><Item xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><xsl:value-of select=\"description\"/></Item></xsl:for-each>";
                xslt = $"<?xml version=\"1.0\" encoding=\"utf-8\"?><xsl:stylesheet version=\"3.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\"><xsl:output method=\"xml\" version=\"1.0\" encoding=\"UTF-8\" indent=\"yes\"/><xsl:template match=\"rss\">{itemXslt}</xsl:template><xsl:template match=\"description\"><xsl:value-of select=\".\"/></xsl:template></xsl:stylesheet>";
            }

            //var serializer = new XmlSerializer(typeof(List<Item>));
            var serializer = new XmlSerializer(typeof(Items));
            XmlDocument xmlDocument = new XmlDocument();
            StringBuilder output = new StringBuilder();
            XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
            XmlWriterSettings writerSettings = new XmlWriterSettings { OmitXmlDeclaration = true };

            try
            {
                xmlDocument.LoadXml(xml);

                if (xslt != null)
                {
                    //xslCompiledTransform.Load(xslt);//path
                    //using (StringReader sr = new StringReader(xslt))
                    //{
                    //    using (XmlReader xr = XmlReader.Create(sr))
                    //    {
                    //        xslCompiledTransform.Load(xr);
                    //    }
                    //}
                    xslCompiledTransform.Parse(xslt);
                    using (XmlWriter transformedData = XmlWriter.Create(output, writerSettings))
                    {
                        xslCompiledTransform.Transform(xmlDocument, transformedData);
                        using (StringReader xmlString = new StringReader(output.ToString()))
                        {
                            results = (Items)serializer.Deserialize(xmlString);
                        }
                    }
                }
                else
                {
                    using (TextReader reader = new StringReader(xml))
                    {
                        results = (Items)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Common.FromXmlList<T>({xml},{xslt}) - ERROR:{exception.Message}");
            }
            return (T)results;
        }

        /// <summary>
        /// GetFilepath
        /// </summary>
        /// <param name="filePathOrId"></param>
        /// <returns></returns>
        public static string GetFilepath(string filePathOrId, string typeName = "Item", string schema = "json")
        {
            if (File.Exists(filePathOrId)) return filePathOrId;

            return $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\app_data\\blocks\\{typeName}.{filePathOrId}.{schema}";
        }

    }
}
