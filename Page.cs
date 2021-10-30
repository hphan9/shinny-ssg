using System.Globalization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace shinny_ssg
{

    class Page
    {
        private string _head;
        private string _title;
        private string _body;
        private string _cssString;
        private string _lang;
        public Page() { }
        public Page(string text, string cssUrl, string langAtr)
        {
            var paraps = Regex.Split(text, "\r?\n\r?\n");
            //first line is _title
            _title = paraps[0];
            _cssString = $"<link rel =\"stylesheet\"href =\"{cssUrl}\" >";
            _lang = $"lang= \"{langAtr}\"";
            _head = $"<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>" +
                 _cssString +
                $"<title >{_title} </title >" +
                $"<meta name=\"description\" content=\"Page is generated with ShinnySSG tool \"/>" +
               $"<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\">" +
               $"<meta property=\"og: title\" content=\"{_title}\" />" +
               $"<meta property = \"og:image\" content = \"https://pixabay.com/images/id-761653/\" />";

            _body += $"<h1>{_title}</h1>";
            for (var i = 1; i < paraps.Length; i++)
            {
                var temp = Regex.Replace(paraps[i], "\r?\n", " ");
                temp = this.ParseMarkdownLine(temp);
                _body += $"<p>{temp}</p>";
            }
        }

        public string GetPage()
        {
            //Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten
            return $"<!DOCTYPE html> <html {_lang}> <head> {_head} </head> <body>{_body}</body> </html>";
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
