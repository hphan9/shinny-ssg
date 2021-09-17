using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace shinny_ssg
{
    class Subfolder
    {

        //recursive method
        public void CreateFolder(string parent, string des)
        {
            DirectoryInfo dSource = new DirectoryInfo(parent);
            DirectoryInfo dDestination = new DirectoryInfo(des);
            //Getting only text files
            foreach (FileInfo f in dSource.GetFiles("*.txt"))
            {
                var src = $"{dSource.FullName}\\{f.Name}";
                var temp = new FileText();
                if (temp.CreateFile(src, des))
                {
                    temp.SaveFile();
                    Console.WriteLine($"File {f.Name} is converted suceesful in {dDestination} folder");
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
