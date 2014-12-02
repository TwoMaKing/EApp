using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Compression
{
    public interface ICompression
    {
        byte[] Zip(object obj);

        void ZipFile(string sourceFilePath, string targetFilePath);

        object Unzip(byte[] bytes);

        void UnzipFile(string sourceFilePath, string targetFilePath);
        
    }
}
