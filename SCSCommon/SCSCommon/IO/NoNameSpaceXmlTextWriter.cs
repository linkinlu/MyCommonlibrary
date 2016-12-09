using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SCSCommon.IO
{
    /// <summary>
    /// xml中所有节点不包含namespace 
    /// </summary>
    /// <seealso cref="System.Xml.XmlTextWriter" />
    public class NoNameSpaceXmlTextWriter: XmlTextWriter
    {
        private bool skip = false;

        public NoNameSpaceXmlTextWriter(TextWriter w) : base(w)

        {
            Formatting = Formatting.Indented;

        }

        /// <summary>
        /// Writes the start of an attribute.
        /// </summary>
        /// <param name="prefix">Namespace prefix of the attribute.</param>
        /// <param name="localName">LocalName of the attribute.</param>
        /// <param name="ns">NamespaceURI of the attribute</param>
        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            if (prefix == "xmlns" )
            {
                skip = true;
                return;
            }
            base.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteEndAttribute()
        {
            if (skip)
            {
                skip = false;
                return;
            }

            base.WriteEndAttribute();
        }

        public override void WriteString(string text)
        {
            if (skip)
            {
                return;
            }

            base.WriteString(text);
        }

    }


   
}
