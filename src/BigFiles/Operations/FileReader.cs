using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
namespace BigFiles.Operations
{
    public class FileReader: IOperation
    {
        private string path;
        private static IFileSystem fs;
        public static IFileSystem FileSystem
        {
            get { return fs; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("FileSystem");
                }

                fs = value;
            }
        }

        static FileReader()
        {
            FileSystem = new System.IO.Abstractions.FileSystem();
        }

        public FileReader(string path)
        {
            Log.Debug("NullOperation for {path}", path);
            this.path = path;

            if (!FileSystem.File.Exists(path))
            {
                throw new FileNotFoundException("Could not find input file", path);
            }
        }

        public IEnumerable<InputLine> ReadLines()
        {
            string line;
            int lineNo = 0;

            using (var stream = FileSystem.File.OpenRead(path))
            {
                using (var reader = new StreamReader(stream))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return new InputLine() { Content = line, OrgLineNumber = lineNo++ };
                    }
                }
            }
        }
    }
}
