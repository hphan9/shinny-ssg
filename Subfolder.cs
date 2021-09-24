using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace shinny_ssg
{
    class Subfolder
    {

        //recursive method
        public void CreateFolder(string parent, string des)
        {
            DirectoryInfo dSource = new DirectoryInfo(parent);
            DirectoryInfo dDestination = new DirectoryInfo(des);
            //Getting only text files and markdown files
            foreach (FileInfo f in dSource.EnumerateFiles("*.*",SearchOption.AllDirectories).Where(file => file.Name.EndsWith(".txt") || file.Name.EndsWith(".md")))
            {
                var src = $"{dSource.FullName}\\{f.Name}";
                var temp = new FileText();
                if (temp.CreateFile(src, des))
                {
                    if (temp.SaveFile())
                    {
                        Console.WriteLine($" {f.Name} is converted sucessfull in {dDestination} folder");
                    }
                }

            }
            //check all the folder
            foreach (DirectoryInfo subDir in dSource.GetDirectories())
            {
                var name = subDir.Name;
                var newdir = dDestination.CreateSubdirectory($"{name}");
                CreateFolder(subDir.ToString(), newdir.FullName);
            }
        }
    }
}
