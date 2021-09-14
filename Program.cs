using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
namespace shinny_ssg
{
    class Program
    {
        //recursive method
        private void createFolder(string parent, string des)
        {
            DirectoryInfo dSource = new DirectoryInfo(parent);
            DirectoryInfo dDestination = new DirectoryInfo(des);
            //Getting only text files
            foreach (FileInfo f in dSource.GetFiles("*.txt"))
            {
                var src = $"{dSource.FullName}/{f.Name}";
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
        static void Main(string[] args)
        {
            Program p = new Program();
            string DESTINATION = @"C:\Users\khoit\Desktop\OSD600\shinny-ssg\bin\Debug\netcoreapp3.1\dist";
            //initialize new CLI application
            var app= new CommandLineApplication<Program>();
            Console.WriteLine("Hello World!");
            //help option
            app.HelpOption();
            app.VersionOption("-v|--version", "0.1", "Shinny SSG 0.1");
            var inputFileOption = app.Option<string>("-i|--input", "Input file/folder to convert to HTML", CommandOptionType.SingleValue)
                                   .IsRequired();
            //on excute
            app.OnExecute(() =>
            {
                var inputname = inputFileOption.Value();
                if (File.Exists(inputname))
                {
                    //add try catch block here
                    //if file can write and read ok we can delete old folder and write new file
                    FileText temp = new FileText(inputname, DESTINATION );
                    temp.saveFile();
                }
                else if(Directory.Exists(inputname)){
                     p.createFolder(inputname, DESTINATION);
                }else
                {

                    Console.WriteLine("File Name is not valid");
                }
            });

            app.Execute(args);
        }
    
    
    
    }
}
