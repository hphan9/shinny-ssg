// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Shinny_ssg
	{
	using System;
	using System.IO;
	using McMaster.Extensions.CommandLineUtils;
	using Newtonsoft.Json.Linq;
	using shinny_ssg;
	public class Program
		{

		private static int Main(string[] args)
			{
			try
				{
				// initialize new CLI application
				using var app = new CommandLineApplication<CommandLineOptions>();
				app.HelpOption();
				app.VersionOption("-v|--version", "0.1", "Shinny SSG 0.1");
				app.Conventions.UseDefaultConventions();
				app.OnExecute(() =>
				{
					var generator = new Generator(app.Model);
					generator.Run();
				});
				return app.Execute(args);
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
			}
		}
	}
