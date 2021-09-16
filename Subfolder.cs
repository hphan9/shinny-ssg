using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace shinny_ssg
{
    class Subfolder
    {

        //recursive method
        public void createFolder(string parent, string des)
        {
            DirectoryInfo dSource = new DirectoryInfo(parent);
            DirectoryInfo dDestination = new DirectoryInfo(des);
            //Getting only text files
            foreach (FileInfo f in dSource.GetFiles("*.txt"))
            {
                var src = $"{dSource.FullName}\\{f.Name}";
                FileText temp = new FileText(src, des);
                temp.saveFile();
                Console.WriteLine(src);
            }
            //check all the folder
            foreach (DirectoryInfo subDir in dSource.GetDirectories())
            {
                var name = subDir.Name;
                var newdir = dDestination.CreateSubdirectory($"{name}");
                createFolder(subDir.ToString(), newdir.FullName);
            }
        }
    }
}
