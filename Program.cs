using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Ling;

namespace shinny_ssg
{
    static class Globals
    {
        public static string cssUrl;
        public static string langAtr;
    }

    class Program
    {
        static int Main(string[] args)
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
                    var configOption = app.Option<string>("-c|--config", "Configuration JSON File", CommandOptionType.SingleValue);
                    var inputFileOption = app.Option<string>("-i|--input", "Input file/folder to convert source file to HTML", CommandOptionType.SingleValue)
                                           .IsRequired();
                    var outputOption = app.Option<string>("-o|--output", "Output folder for converted file/files", CommandOptionType.SingleValue);
                    var cssOption = app.Option<string>("--stylesheet| -s", "Style Sheet for the converted HTML file", CommandOptionType.SingleValue);
                    //language option
                    var langOption = app.Option<string>("-l|--lang", "Language Attribute of the converted HTML file", CommandOptionType.SingleValue);
                    //on excute

                    app.OnExecute(() =>
                    {
                        var configname = configOption.Value();
                        if (File.Exists(configname) && (configname.EndsWith(".json")))
                        {
                            string jsonString = File.ReadAllText(configname);
                            JObject jObj = JObject.Parse(jsonString);

                            foreach (KeyValuePair<string, JToken> keyValuePair in jObj)
                            {
                                switch (keyValuePair.Key) {
                                case "input":
                                    var inputname = (string)keyValuePair.Value;
                                    break;
                                case "output":
                                    var destination = (string)keyValuePair.Value;
                                    break;
                                case "stylesheet":
                                    Globals.cssUrl = (string)keyValuePair.Value;
                                    break;
                                case "lang":
                                    Globals.langAtr = (string)keyValuePair.Value;
                                    break;
                                }

                            }

                            if (string.IsNullOrEmpty(destination)){
                                destination = "\dist"
                            }
                            if(Directory.Exists(destination){
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

                             if (File.Exists(inputname) && (inputname.EndsWith(".txt") || inputname.EndsWith(".md")))
                        {
                            //if the file can not read or create , it will never be saved in the destinaiton folder
                            FileText temp = new FileText();
                            if (temp.CreateFile(inputname, destination))
                            {
                                if (temp.SaveFile())
                                {
                                    Console.WriteLine($"File {inputname} is converted sucessfull in {destination} folder");
                                }
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

                        }



                        }
                        else{

                        var inputname = inputFileOption.Value();
                        Globals.cssUrl = cssOption.HasValue() ? cssOption.Value() : null;
                        Globals.langAtr = langOption.HasValue() ? langOption.Value() : null;
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

                        if (File.Exists(inputname) && (inputname.EndsWith(".txt") || inputname.EndsWith(".md")))
                        {
                            //if the file can not read or create , it will never be saved in the destinaiton folder
                            FileText temp = new FileText();
                            if (temp.CreateFile(inputname, destination))
                            {
                                if (temp.SaveFile())
                                {
                                    Console.WriteLine($"File {inputname} is converted sucessfull in {destination} folder");
                                }
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

                        }

                    });

                    app.Execute(args);
                }
            }
            catch (CommandParsingException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("There is an error with the transfer process");
                Console.Error.WriteLine(ex.Message);
                return -1;
            }

            return 0;
        }

    }
}
