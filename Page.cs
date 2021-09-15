using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace shinny_ssg
{

    
    class Page
    {
        private string head ;
        private string title;
        private string body;
        private string cssString;
        public Page( string text, string css)
        {
            var paraps = Regex.Split(text, "\r?\n\r?\n");
            //first line is title
            title = paraps[0];
            cssString = String.IsNullOrEmpty(css) ? "" : $"<link rel =\"stylesheet\"href =\"{css}\" >";
            Console.WriteLine(css);
            head= $"<head>"+
                $"<meta charset = \"utf-8\" >" +
                 cssString +
                 $"<title >{title} </title >" +
                $"<meta name = \"viewport\" content = \"width=device-width, initial-scale=1\">" +
                $"</head>";
            body += $"<h1>{title}</h1>";
            for(var i=1; i<paraps.Length; i++)
            {
                body += $"<p>{paraps[i]}</p>";
            }


        }
        public void setCssFile(string cssFile)
        {

        }
        public string getHead()
        {
            return head;
        }
        public string getTitle()
        {
            return title;
        }
        public string getBody()
        {
            return body;
        }
        public string getPage()
        {
            //Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten
            return $"<html> <head>{head}</head> <body>{body}</body> </html>";
        }
    }
}
