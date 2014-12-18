using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace EApp.Common.Compression
{
    public class WinZipper : ICompression
    {
        public byte[] Zip(object obj)
        {
            throw new NotImplementedException();
        }

        public void ZipFile(string sourceFilePath, string targetFilePath)
        {
            throw new NotImplementedException();
        }

        public object Unzip(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public void UnzipFile(string sourceFilePath, string targetFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
