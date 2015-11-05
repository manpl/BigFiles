using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
namespace BigFiles.Operations
{
    [Usage("/null")]
    public class NullOperation : IFileOperation
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

        static NullOperation()
        {
            FileSystem = new System.IO.Abstractions.FileSystem();
        }

        public NullOperation(string path)
        {
            Log.Debug("NullOperation for {0}", path);
            this.path = path;

            if (!FileSystem.File.Exists(path))
            {
         //       throw new FileNotFoundException("Could not find input file", path);
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

        public void Execute()
        {
        }
    }
}
