// <copyright file="FileText.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Shinny_ssg
	{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Text.RegularExpressions;

	public class FileText
		{
		private string sourcePath;
		private string folder;
		private string name;
		private Page page;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileText"/> class.
		/// </summary>
		public FileText()
			{
			}

		public bool CreateFile(string source, string folder, string cssUrl, string langAtr)
			{
			this.sourcePath = source;
			if (Path.GetExtension(this.sourcePath) != ".txt" && Path.GetExtension(this.sourcePath) != ".md")
				{
				return false;
				}

			this.folder = folder;
			this.name = Path.GetFileNameWithoutExtension(this.sourcePath);
			string text = null;
			try
				{
				text = File.ReadAllText(source);
				}
			catch (UnauthorizedAccessException)
				{
				Console.Error.WriteLine($"The file or directory cannot be authorized. There is no read permission for {this.name} file or directory. ");
				return false;
				}
			catch (Exception ex)
				{
				Console.Error.WriteLine($"Can not read file {this.name} from {source}");
				Console.WriteLine(ex.Message);
				return false;
				}

			this.page = new Page(text, cssUrl, langAtr);
			return true;
			}

		public bool SaveFile()
			{
			var newPath = Path.Combine(this.folder, $"{this.name}.html");
			try
				{
				File.WriteAllText(newPath, this.page.GetPage());
				}
			catch (Exception)
				{
				Console.Error.WriteLine($"Can not write file {this.name} to {newPath}");
				return false;
				}

			return true;
			}
		}
	}
