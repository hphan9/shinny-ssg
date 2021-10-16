using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace shinny_ssg
{
    class Generator
    {
        private string _input;
        private string _destination;
        private string _CssUrl;
        private string _langAtr;
        public Generator(string input, string destination, string CssUrl, string langAtr)
        {
            _input = input;
            _destination = destination;
            _CssUrl = CssUrl;
            _langAtr = langAtr;
        }

        public int Run()
        {
            if (File.Exists(_input))
            {
                GenerateFile(_input, _destination);
            }
            else if (Directory.Exists(_input))
            {
                GenerateFolder(_input, _destination);
            }
            else
            {
                Console.Error.WriteLine("The Input Path is not valid");
                return -1;
            }
            return 0;
        }

        private void GenerateFile(string src, string destination)
        {
            var temp = new FileText();
            if (temp.CreateFile(src, destination, _CssUrl, _langAtr))
            {
                if (temp.SaveFile())
                {
                    Console.WriteLine($" {Path.GetFileName(src)} --------- {Path.GetFullPath(destination)} ");
                }
            }

        }
        //recursive method 
        private void GenerateFolder(string parent, string destionation)
        {
            DirectoryInfo dSource = new DirectoryInfo(parent);
            DirectoryInfo dDestination = new DirectoryInfo(destionation);
            //Getting only text files and markdown files
            foreach (FileInfo f in dSource.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                var src = Path.Combine(dSource.FullName, f.Name);
                GenerateFile(src, destionation);
            }
            //check all the folder
            foreach (DirectoryInfo subDir in dSource.GetDirectories())
            {
                var name = subDir.Name;
                var newdir = dDestination.CreateSubdirectory($"{name}");
                GenerateFolder(subDir.ToString(), newdir.FullName);
            }
        }
    }
}
