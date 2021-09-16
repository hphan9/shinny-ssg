using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace shinny_ssg
{

    class Page
    {
        private string head;
        private string title;
        private string body;
        private string cssString;
        public Page() { }
        public Page(string text)
        {
            var paraps = Regex.Split(text, "\r?\n\r?\n");
            //first line is title
            title = paraps[0];
            cssString = String.IsNullOrEmpty(Globals.cssUrl)
                       ? "<style type ='text/css'> body { display: block;max-width: 800px; margin: 20px auto; padding: 0 10px; word-wrap: break-word  }</style >"
                       : $"<link rel =\"stylesheet\"href =\"{Globals.cssUrl}\" >";

            head = $"<head>" +
                $"<meta charset = \"utf-8\" >" +
                 cssString +
                $"<title >{title} </title >" +
                $"<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\">" +
                $"</head>";
            body += $"<h1>{title}</h1>";
            for (var i = 1; i < paraps.Length; i++)
            {
                body += $"<p>{paraps[i]}</p>";
            }
        }
        public string getTitle()
        {
            return this.title;
        }
        public string getPage()
        {
            //Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten
            return $"<html> <head>{head}</head> <body>{body}</body> </html>";
        }
    }
}
