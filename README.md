# shinny-ssg  - CLI Tool to generate HTML File
## Features
* generate a .html file from a .txt file with a title
* accept folder to generate .html files in the new folder with the same structure of original folder
* give user the options to spectify destination folder and/or stylesheet url link

## How to use
* Download or clone the shinny-ssg folder to your local machine
* Use command prompt to use the tool by these command:
* Navigate to the folder netcoreapp3.1 in the shinny-ssg folder ( path `shinny-ssg\bin\Debug\netcoreapp3.1`)
* In the command prompt run `shinny-ssg.exe -h` to see the option
* The file will be generated in the `shinny-ssg\bin\Debug\netcoreapp3.1\dist` folder by default or in the destination folder if user specified
### Command
> shinny-ssg.exe -h 
> 
> shinny-ssg.exe -i `<filePath>` 
>
> shinny-ssg.exe -i `<path>` -o `<folder path>` -s `<stylesheet link>`

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

### Author 
Emily Phan
  
