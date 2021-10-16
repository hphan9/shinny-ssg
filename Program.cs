using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;

namespace shinny_ssg
{
    static class Globals
    {
        public static string cssUrl = "<style type ='text/css'> body { display: block;max-width: 800px; margin: 20px auto; padding: 0 10px; word-wrap: break-word  }</style >";
        public static string langAtr = "lang= \"en-CA\"";
    }

    class Program
    {
        static int Main(string[] args)
        {
            var result = 0;
            var destination = @".\dist";

            try
            {
                //initialize new CLI application
                using (var app = new CommandLineApplication<Program>())
                {
                    //help option
                    app.HelpOption();
                    app.VersionOption("-v|--version", "0.1", "Shinny SSG 0.1");
                    //config Option
                    var configOption = app.Option<string>("-c|--config", "Configuration JSON File", CommandOptionType.SingleValue);
                    // input file now is not required 
                    var inputFileOption = app.Option<string>("-i|--input", "Input file/folder to convert source file to HTML", CommandOptionType.SingleValue);
                    var outputOption = app.Option<string>("-o|--output", "Output folder for converted file/files", CommandOptionType.SingleValue);
                    var cssOption = app.Option<string>("--stylesheet| -s", "Style Sheet for the converted HTML file", CommandOptionType.SingleValue);
                    //language option
                    var langOption = app.Option<string>("-l|--lang", "Language Attribute of the converted HTML file", CommandOptionType.SingleValue);
                    //on excute

                    app.OnExecute(() =>
                    {
                        var configName = configOption.HasValue() ? configOption.Value() : null;
                        var inputValue = inputFileOption.HasValue() ? inputFileOption.Value() : null;

                        // If config worked 
                        var inputname = "";
                        if (configName != null && File.Exists(configName) && (configName.EndsWith(".json")))
                        {
                            //start working with config File here
                            string jsonString = File.ReadAllText(configName);
                            JObject jObj = JObject.Parse(jsonString);
                            inputname = jObj.ContainsKey("input") ? (string)jObj["input"] : "";
                            Globals.cssUrl = jObj.ContainsKey("stylesheet") ? (string)jObj["stylesheet"] : default;
                            Globals.langAtr = jObj.ContainsKey("lang") ? (string)jObj["lang"] : default;
                            destination = jObj.ContainsKey("output") ? (string)jObj["output"] : default;
                        }

                        else if (inputValue != null)
                        {
                            inputname = inputValue;
                            Globals.cssUrl = cssOption.HasValue() ? cssOption.Value() : default;
                            Globals.langAtr = langOption.HasValue() ? langOption.Value() : default;
                            destination = outputOption.HasValue() ? outputOption.Value() : default;
                        }
                        else
                        {
                            throw new Exception("Either Input or Config File must have a valid value ");
                        }

                        //It will delete all file even though the read or write process fail
                        if (Directory.Exists(destination))
                        {
                            System.IO.DirectoryInfo di = new DirectoryInfo(destination);
                            di.Delete(true);
                        }
                        DirectoryInfo newDir = Directory.CreateDirectory(destination);

                        //Create Generator
                        var gen = new Generator(inputname, destination);
                        result = gen.Run();
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
                Console.Error.WriteLine(ex.Message);
                return -1;
            }

            return result;
        }

    }
}