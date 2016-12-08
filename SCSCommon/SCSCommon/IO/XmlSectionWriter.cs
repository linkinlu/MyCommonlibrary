using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SCSCommon.IO
{
    public class XmlSectionWriter: XmlTextWriter
    {
        //public XmlSectionWriter(Stream w, Encoding encoding) : base(w, encoding)
        //{

        //}

        //public XmlSectionWriter(string filename, Encoding encoding) : base(filename, encoding)
        //{
        //}

        public XmlSectionWriter(TextWriter w) : base(w)
        {
            
            base.Namespaces = false;
        }

       
    }
}
