using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
namespace shinny_ssg
{
    class Program
    {

        static void Main(string[] args)
        {

            string DESTINATION = @"..\..\dist";

            try
            {
                //initialize new CLI application
                using (var app = new CommandLineApplication<Program>())
                {
                    //help option
                    app.HelpOption();
                    app.VersionOption("-v|--version", "0.1", "Shinny SSG 0.1");
                    var inputFileOption = app.Option<string>("-i|--input", "Input file/folder to convert to HTML", CommandOptionType.SingleValue)
                                           .IsRequired();
                    var outputOption = app.Option<string>("-o|--output", "Output folder for converted file", CommandOptionType.SingleValue);
                    var cssOption = app.Option<string>("---stylesheet| -s", "Style Sheet for the HTML file", CommandOptionType.SingleValue);
                    //on excute

                    app.OnExecute(() =>
                    {

                        var inputname = inputFileOption.Value();
                        var cssString = cssOption.HasValue() ? cssOption.Value() : null;
                        if (outputOption.HasValue() && Directory.Exists(outputOption.Value()))
                        {
                            DESTINATION = outputOption.Value();
                        }
                        else
                        {
                            //It will delete all file even though the read or write process fail
                            System.IO.DirectoryInfo di = new DirectoryInfo(DESTINATION);

                            foreach (FileInfo file in di.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in di.GetDirectories())
                            {
                                dir.Delete(true);
                            }

                        }
                        if (File.Exists(inputname))
                        {
                            //add try catch block here
                            //if file can write and read ok we can delete old folder and write new file
                            FileText temp = new FileText(inputname, DESTINATION, cssString);
                            temp.saveFile();
                        }
                        else if (Directory.Exists(inputname))
                        {
                            var f = new Subfolder();
                            f.createFolder(inputname, DESTINATION, cssString);
                        }
                        else
                        {
                            Console.WriteLine("File Name is not valid");
                        }

                        Console.WriteLine($"File is in folder {DESTINATION}");

                    });

                    app.Execute(args);
                }
            }
            catch (CommandParsingException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }



    }
}
