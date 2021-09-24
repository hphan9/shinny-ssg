using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace shinny_ssg
{
    class FileText
    {
        private string _sourcePath;
        private string _folder;
        private string _name;
        private Page _page;
        public FileText() { }

        public bool CreateFile(string source, string folder)
        {
            this._sourcePath = source;
            this._folder = folder;
            var fi1 = new FileInfo(source);
            _name = fi1.Name;
            string text = null;
            try
            {
                text = File.ReadAllText(source);
            }
            catch (UnauthorizedAccessException)
            {

                Console.Error.WriteLine($"The file or directory cannot be authorized. There is no read permission for {_name} file or directory. ");
                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Can not read file {_name} from {source}");
                Console.WriteLine(ex.Message);
                return false;
            }
            _page = new Page(text);
            return true;
        }

        public bool SaveFile()
        {

            var newPath = $"{_folder}\\{_name.Replace("txt", "html")}";
            try
            {
                File.WriteAllText(newPath, _page.GetPage());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Can not write file {_name} to {newPath}");
                return false;
            }
            return true;
        }
    }
}
