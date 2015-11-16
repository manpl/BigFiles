using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigFiles.Operations
{
    [Usage("/ConsoleOutput")]
    public class FileOperation : FileOperationBase
    {
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

        private string Path { get; set; }

        public FileOperation(IOperation parent, String path)
            : base(parent)
        {
            FileSystem = new System.IO.Abstractions.FileSystem();
            Path = path;
        }

        public override IEnumerable<InputLine> ReadLines()
        {
            using (var writer = FileSystem.File.OpenWrite(Path))
            {
                using (var streamWriter = new StreamWriter(writer))
                {
                    foreach (var line in Parent.ReadLines())
                    {
                        streamWriter.WriteLine(line.Content);
                        yield return line;
                    }
                }
            }
        }

        public override void Execute()
        {
            Parent.Execute();
        }
    }
}
