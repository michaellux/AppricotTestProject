using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal class FileSystemCollector
    {
        
        internal class FileSystemCollectorItem
        {
            private int levelInHierarchy;
            private Type? fileSystemType;
            private long size;
            private string name;

            public FileSystemCollectorItem(int levelInHierarchy, FileSystemInfo fileSystemInfo)
            {
                this.levelInHierarchy = levelInHierarchy;
                this.fileSystemType = fileSystemInfo.GetType();
                if (fileSystemInfo is FileInfo)
                {
                    this.size = ((FileInfo)fileSystemInfo).Length;
                }
                this.name = fileSystemInfo.Name;
            }

            public int LevelInHierarchy { get => levelInHierarchy; }
            public long Size { get => size; }
            public string Name { get => name; }
        }
    }
}
