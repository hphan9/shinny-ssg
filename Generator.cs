// <copyright file="Generator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Shinny_ssg
    {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class Generator
        {
        private string input;
        private string outputFolder;
        private string cssUrl;
        private string langAtr;

        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        /// <param name="input">In put.</param>
        /// <param name="outputFolder"> out put folder.</param>
        /// <param name="cssUrl">CSS url.</param>
        /// <param name="langAtr">language attribute.</param>
        public Generator(string input, string outputFolder, string cssUrl, string langAtr)
            {
            this.input = input;
            this.outputFolder = outputFolder;
            this.cssUrl = cssUrl;
            this.langAtr = langAtr;
            }

        public int Run()
            {
            if (File.Exists(this.input))
                {
                this.GenerateFile(this.input, this.outputFolder);
                }
            else if (Directory.Exists(this.input))
                {
                this.GenerateFolder(this.input, this.outputFolder);
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
            if (temp.CreateFile(src, outputFolder, this.cssUrl, this.langAtr))
                {
                if (temp.SaveFile())
                    {
                    Console.WriteLine($" {Path.GetFileName(src)} --------- {Path.GetFullPath(outputFolder)} ");
                    }
                }
            }

        // recursive method
        private void GenerateFolder(string parent, string outputFolder)
            {
            DirectoryInfo dSource = new DirectoryInfo(parent);
            DirectoryInfo dDestination = new DirectoryInfo(outputFolder);

            // Getting only text files and markdown files
            foreach (FileInfo f in dSource.EnumerateFiles("*.*", SearchOption.AllDirectories))
                {
                var src = Path.Combine(dSource.FullName, f.Name);
                this.GenerateFile(src, outputFolder);
                }

            // check all the folder
            foreach (DirectoryInfo subDir in dSource.GetDirectories())
                {
                var name = subDir.Name;
                var newdir = dDestination.CreateSubdirectory($"{name}");
                this.GenerateFolder(subDir.ToString(), newdir.FullName);
                }
            }
        }
    }
