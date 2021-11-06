// <copyright file="Page.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Shinny_ssg
    {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Page
        {
        private string head;
        private string title;
        private string body;
        private string cssString;
        private string lang;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
            {
            }

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        /// <param name="text">the source text.</param>
        /// <param name="cssUrl"> Css url.</param>
        /// <param name="langAtr"> language attribute.</param>
        public Page(string text, string cssUrl, string langAtr)
            {
            var paraps = Regex.Split(text, "\r?\n\r?\n");

            // first line is _title
            this.title = paraps[0];
            this.cssString = $"<link rel =\"stylesheet\"href =\"{cssUrl}\" >";
            this.lang = $"lang= \"{langAtr}\"";
            this.head = $"<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>" +
                 this.cssString +
                $"<title >{this.title} </title >" +
                $"<meta name=\"description\" content=\"Page is generated with ShinnySSG tool \"/>" +
               $"<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\">" +
               $"<meta property=\"og: title\" content=\"{this.title}\" />" +
               $"<meta property = \"og:image\" content = \"https://pixabay.com/images/id-761653/\" />";

            this.body += $"<h1>{this.title}</h1>";

            for (var i = 1; i < paraps.Length; i++)
                {
                var temp = Regex.Replace(paraps[i], "\r?\n", " ");
                temp = this.ParseMarkdownLine(temp);
                this.body += $"<p>{temp}</p>";
                }
            }

        public string GetPage()
            {
            // Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten
            return $"<!DOCTYPE html> <html {this.lang}> <head> {this.head} </head> <body>{this.body}</body> </html>";
            }

        // Parse the markdown line into html
        public string ParseMarkdownLine(string line)
            {
            // If aiming for performance,
            // ...use named groups and try to match italic, underline, etc.
            // This matches anything between double asterisks
            string boldPattern = @"\*\*(.*?)\*\*";
            string boldReplacement = "<strong>$1</strong>";
            return Regex.Replace(line, boldPattern, boldReplacement);
            }
        }
    }
