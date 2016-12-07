using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Configuration
{
    public interface IConfigurationFile
    {
        T Resolve<T>();

        void GetFile(string filePath,string filename);

        ConfigFileExtension FileExtension { get; }
    }

    public enum ConfigFileExtension
    {
        //Config,
        Json,
        Xml
    }

}
