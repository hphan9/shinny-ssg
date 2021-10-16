using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace shinny_ssg
{
    class Generator
    {
        private string _input;
        private string _outputFolder;
        private string _CssUrl;
        private string _langAtr;
        public Generator(string input, string outputFolder, string CssUrl, string langAtr)
        {
            _input = input;
            _outputFolder = outputFolder;
            _CssUrl = CssUrl;
            _langAtr = langAtr;
        }

        public int Run()
        {
            if (File.Exists(_input))
            {
                GenerateFile(_input, _outputFolder);
            }
            else if (Directory.Exists(_input))
            {
                GenerateFolder(_input, _outputFolder);
            }
            else
            {
                Console.Error.WriteLine("The Input Path is not valid");
                return -1;
            }
            return 0;
        }

        private void GenerateFile(string src, string outputFolder)
        {
            var temp = new FileText();
            if (temp.CreateFile(src, outputFolder, _CssUrl, _langAtr))
            {
                if (temp.SaveFile())
                {
                    Console.WriteLine($" {Path.GetFileName(src)} --------- {Path.GetFullPath(outputFolder)} ");
                }
            }

        }
        //recursive method 
        private void GenerateFolder(string parent, string outputFolder)
        {
            DirectoryInfo dSource = new DirectoryInfo(parent);
            DirectoryInfo dDestination = new DirectoryInfo(outputFolder);
            //Getting only text files and markdown files
            foreach (FileInfo f in dSource.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                var src = Path.Combine(dSource.FullName, f.Name);
                GenerateFile(src, outputFolder);
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
