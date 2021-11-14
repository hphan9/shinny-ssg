using System;
using System.Collections.Generic;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace shinny_ssg
	{

	public class CommandLineOptions
		{
		[Option("-v|--version", Description = "Shinny SSG 0.1")]
		public string Version { get; internal set; }

		[Option("-c|--config", "Configuration JSON File", CommandOptionType.SingleValue)]
		public string ConfigFile { get; internal set; }

		[Option("-i|--input", "Input file/folder to convert source file to HTML", CommandOptionType.SingleValue)]
		public string InputPath { get; internal set; }

		[Option("-o|--output", "Output folder for converted file/files", CommandOptionType.SingleValue)]
		public string OutputPath { get; internal set; }

		[Option("--stylesheet| -s", "Style Sheet for the converted HTML file", CommandOptionType.SingleValue)]
		public string Stylesheet { get; internal set; }

		[Option("-l|--lang", "Language Attribute of the converted HTML file", CommandOptionType.SingleValue)]
		public string LangAttr { get; internal set; }

		}
	}
