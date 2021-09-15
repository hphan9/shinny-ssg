# shinny-ssg  - CLI Tool to generate HTML File
## Features
* generate a .html file .txt file with title
* accept folder to generate .html files in the new folder with the same structure of origional folder
* give user option to choose destination folder and stylesheet 

## How to use

Option	Function
 
| OPTION             | Function                            | 
| ------------------ |:-----------------------------------:| 
| -v --version       | 	Name and version                   | 
| -h --help          |  Information                        |   
| -s --stylesheet    | <'link-to-css-stylesheet'>	         | 
| -i --input         | specifies an input file or folder   | 
| -o --output        | specifies destination folder        | 

### Built with
* .NET CORE 3.1
* [Command Line Utils](https://github.com/natemcmaster/CommandLineUtils)

### How to use 
> shinny -h 
> 
> shinny -i <filePath> 
>
> shinny -i <path> -o <folder apth> -s <stylesheet link>
  
