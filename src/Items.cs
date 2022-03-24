using System.Collections.Generic;
using System.Xml.Serialization;

namespace AppBlocks.Models
{
    [XmlType(TypeName = "items")]
    public class Items:List<Item> { }
}
