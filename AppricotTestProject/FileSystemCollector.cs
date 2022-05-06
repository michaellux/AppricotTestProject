using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal static class FileSystemCollector
    {
        public static readonly List<FileSystemCollectorItem> fileSystemCollectorItems = new List<FileSystemCollectorItem>();
        internal class FileSystemCollectorItem
        {
            private readonly int levelInHierarchy;
            private readonly FileSystemInfo? fileSystemType;
            private readonly string name;

            public FileSystemCollectorItem(int levelInHierarchy, FileSystemInfo fileSystemInfo)
            {
                this.levelInHierarchy = levelInHierarchy;
                this.fileSystemType = fileSystemInfo;
                if (fileSystemInfo is FileInfo)
                {
                    this.Size = ((FileInfo)fileSystemInfo).Length;
                }
                this.name = fileSystemInfo.Name;
            }

            public int LevelInHierarchy { get => levelInHierarchy; }
            public BigInteger Size { get; set; }
            public string Name { get => name; }
            public FileSystemInfo? FileSystemType => fileSystemType;
        }
    }
}
