using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
namespace shinny_ssg
{
    class Program
    {
        static void Main(string[] args)
        {

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
                    Subfolder folder = new Subfolder(inputname);
                    folder.createFolder(inputname, DESTINATION);
                }else
                {

                    Console.WriteLine("File Name is not valid");
                }
            });

            app.Execute(args);
        }
    }
}
