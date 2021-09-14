using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace shinny_ssg
{
    class FileText
    {
        private string sourcePath;
        private string folder;
        private string name;
        private Page page;


        public FileText(string source, string folder)
        {
            this.sourcePath = source;
            this.folder = folder;
            var fi1 = new FileInfo(source);
           
            name = fi1.Name;
            var text = File.ReadAllText(source);
            page = new Page(text);
          
        }

        public void saveFile()
        {
            var newPath = $"{folder}\\{name.Replace("txt","html")}";
            Console.WriteLine(newPath);
            File.WriteAllText(newPath , page.getPage());
            
        }
    }
}
