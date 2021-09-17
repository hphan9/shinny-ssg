using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
namespace shinny_ssg
{
    static class Globals
    {
        public static string cssUrl;
    }

    class Program
    {
        static void Main(string[] args)
        {

            var destination = @".\dist";

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
                        Globals.cssUrl = cssOption.HasValue() ? cssOption.Value() : null;
                        if (outputOption.HasValue() && Directory.Exists(outputOption.Value()))
                        {
                            destination = outputOption.Value();
                        }
                        else
                        {
                            //It will delete all file even though the read or write process fail
                            System.IO.DirectoryInfo di = new DirectoryInfo(destination);
                            foreach (FileInfo file in di.GetFiles(""))
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
                            //if the file can not read or create , it will never be saved in the destinaiton folder
                            FileText temp = new FileText();
                            if (temp.CreateFile(inputname, destination))
                            {
                                temp.SaveFile();
                                Console.WriteLine($"File is converted suceesful in {destination} folder");
                            }

                        }
                        else if (Directory.Exists(inputname))
                        {
                            var f = new Subfolder();
                            f.CreateFolder(inputname, destination);
                        }
                        else
                        {
                            Console.WriteLine("Input Path is not valid.");
                        }

                    });

                    app.Execute(args);
                }
            }
            catch (CommandParsingException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("There is an error with the transfer process");
                Console.Error.WriteLine(ex.Message);
            }
        }

    }
}
