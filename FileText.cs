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
            string text = null;
            try
            {
                text = File.ReadAllText(source);
            }
            catch (Exception ex)
            {
                page = new Page();
                Console.Error.WriteLine($"Can not read file {name} in path {source}");
            }
            finally
            {
                page = new Page(text);
            }

        }

        public void saveFile()
        {
            if (!String.IsNullOrEmpty(page.getTitle()))
            {
                var newPath = $"{folder}\\{name.Replace("txt", "html")}";
                try
                {
                    File.WriteAllText(newPath, page.getPage());
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Can not write file  {name} to path {newPath}");
                }
            }
        }
    }
}
