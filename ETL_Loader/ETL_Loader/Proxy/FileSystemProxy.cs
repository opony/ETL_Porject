using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Loader.Proxy
{
    public class FileSystemProxy
    {
        public static byte[] ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("The file [" + path + "] isn't exists.");
            }

            return null;
        }
    }
}
