using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace shinny_ssg
{
    class FileText
    {
        private string _sourcePath;
        private string _folder;
        private string _name;
        private Page _page;
        public FileText() { }

        public bool CreateFile(string source, string folder, string CssUrl, string langAtr)
        {
            this._sourcePath = source;
            if (Path.GetExtension(_sourcePath) != ".txt" && Path.GetExtension(_sourcePath) != ".md")
            {
                return false;
            }

            this._folder = folder;
            _name = Path.GetFileNameWithoutExtension(_sourcePath);
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
            _page = new Page(text, CssUrl, langAtr);
            return true;
        }

        public bool SaveFile()
        {

            var newPath = Path.Combine(_folder, $"{_name}.html");
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
