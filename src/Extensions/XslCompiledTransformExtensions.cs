using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace AppBlocks.Models.Extensions
{
    public static class XslCompiledTransformExtensions
    {
        public static void Parse(this XslCompiledTransform xslCompiledTransform, string content)
        {
            using (StringReader sr = new StringReader(content))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    xslCompiledTransform.Load(xr);
                }
            }
        }
    }
}