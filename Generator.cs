// <copyright file="Generator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Shinny_ssg
	{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using Newtonsoft.Json.Linq;
	using shinny_ssg;
	public class Generator
		{
		private CommandLineOptions _options;
		private string cssUrl = @"https://cdn.jsdelivr.net/npm/water.css@2/out/water.css";
		private string langAtr = "lang= \"en-CA\"";

		public Generator(CommandLineOptions options)
			{
			_options = options;
			}

		public int Run()
			{
			var configName = this._options.ConfigFile;
			var inputValue = this._options.InputPath;
			var result = 0;
			var outputFolder = @".\dist";

			// If config worked
			var input = string.Empty;
			if (configName != null && File.Exists(configName) && configName.EndsWith(".json"))
				{
				// start working with config File here
				string jsonString = File.ReadAllText(configName);
				JObject jObj = JObject.Parse(jsonString);
				input = jObj.ContainsKey("input") ? (string)jObj["input"] : string.Empty;
				this.cssUrl = jObj.ContainsKey("stylesheet") ? (string)jObj["stylesheet"] : default;
				this.langAtr = jObj.ContainsKey("lang") ? (string)jObj["lang"] : default;
				outputFolder = jObj.ContainsKey("output") ? (string)jObj["output"] : default;
				}
			else if (inputValue != null)
				{
				input = inputValue;
				this.cssUrl = this._options.Stylesheet ?? default;
				this.langAtr = this._options.LangAttr ?? default;
				outputFolder = this._options.OutputPath ?? default;
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

			if (File.Exists(input))
				{
				this.GenerateFile(input, outputFolder);
				}
			else if (Directory.Exists(input))
				{
				this.GenerateFolder(input, outputFolder);
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
