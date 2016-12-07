using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Configuration
{
    public abstract class ConfigurationBase:IConfigurationFile
    {
        public virtual T Resolve<T>()
        {
            if (string.IsNullOrEmpty(Content))
                return default(T);
            return Seriablize<T>();
        }

        public abstract T Seriablize<T>();

        public abstract void GetFile(string filePath,string fileName);

        public abstract string Content { get; }

        public abstract ConfigFileExtension FileExtension { get; }
    }
}
