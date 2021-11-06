﻿// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Shinny_ssg
    {
    using System;
    using System.IO;
    using McMaster.Extensions.CommandLineUtils;
    using Newtonsoft.Json.Linq;

    internal class Program
        {
        private static int Main(string[] args)
            {
            var result = 0;
            var outputFolder = @".\dist";
            string cssUrl = @"<style type ='text/css'> body { display: block;max-width: 800px; margin: 20px auto; padding: 0 10px; word-wrap: break-word  }</style >";
            string langAtr = "lang= \"en-CA\"";
            try
                {
                // initialize new CLI application
                using (var app = new CommandLineApplication<Program>())
                    {
                    // help option
                    app.HelpOption();
                    app.VersionOption("-v|--version", "0.1", "Shinny SSG 0.1");

                    // config Option
                    var configOption = app.Option<string>("-c|--config", "Configuration JSON File", CommandOptionType.SingleValue);

                    // input file now is not required
                    var inputFileOption = app.Option<string>("-i|--input", "Input file/folder to convert source file to HTML", CommandOptionType.SingleValue);
                    var outputOption = app.Option<string>("-o|--output", "Output folder for converted file/files", CommandOptionType.SingleValue);
                    var cssOption = app.Option<string>("--stylesheet| -s", "Style Sheet for the converted HTML file", CommandOptionType.SingleValue);

                    // language option
                    var langOption = app.Option<string>("-l|--lang", "Language Attribute of the converted HTML file", CommandOptionType.SingleValue);

                    // on excute
                    app.OnExecute(() =>
                    {
                        var configName = configOption.HasValue() ? configOption.Value() : null;
                        var inputValue = inputFileOption.HasValue() ? inputFileOption.Value() : null;

                        // If config worked
                        var input = string.Empty;
                        if (configName != null && File.Exists(configName) && configName.EndsWith(".json"))
                            {
                            // start working with config File here
                            string jsonString = File.ReadAllText(configName);
                            JObject jObj = JObject.Parse(jsonString);
                            input = jObj.ContainsKey("input") ? (string)jObj["input"] : string.Empty;
                            cssUrl = jObj.ContainsKey("stylesheet") ? (string)jObj["stylesheet"] : default;
                            langAtr = jObj.ContainsKey("lang") ? (string)jObj["lang"] : default;
                            outputFolder = jObj.ContainsKey("output") ? (string)jObj["output"] : default;
                            }
                        else if (inputValue != null)
                            {
                            input = inputValue;
                            cssUrl = cssOption.HasValue() ? cssOption.Value() : default;
                            langAtr = langOption.HasValue() ? langOption.Value() : default;
                            outputFolder = outputOption.HasValue() ? outputOption.Value() : default;
                            }
                        else
                            {
                            throw new Exception("Either Input or Config File must have a valid value ");
                            }

                        // It will delete all file even though the read or write process fail
                        if (Directory.Exists(outputFolder))
                            {
                            System.IO.DirectoryInfo di = new DirectoryInfo(outputFolder);
                            di.Delete(true);
                            }

                        var newDir = Directory.CreateDirectory(outputFolder);

                        // Create Generator
                        var generator = new Generator(input, outputFolder, cssUrl, langAtr);
                        result = generator.Run();
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
