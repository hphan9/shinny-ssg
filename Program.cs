﻿using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;

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
                            Globals.cssUrl = jObj.ContainsKey("stylesheet") ? (string)jObj["stylesheet"] : null;
                            Globals.langAtr = jObj.ContainsKey("lang") ? (string)jObj["lang"] : null;
                            destination = jObj.ContainsKey("lang") ? (string)jObj["output"] : default;
                        }
                        else if (inputValue != null)
                        {
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
                            inputname = inputValue;
                        }
                        else
                        {
                            throw new Exception("Either Input or Config File must have a valid value ");
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

            return 0;
        }

    }
}