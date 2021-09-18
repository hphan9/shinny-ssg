# shinny-ssg Version 0.1 - CLI Tool to generate HTML File

## Features
* generate a .html file from a .txt file with a title
* accept folder as input path to generate .html files in the destionation folder with the same structure of original folder
* give user the options to specify destination folder and/or stylesheet url link

## How to use
* Download or clone the shinny-ssg folder to your local machine
* Use command prompt to navigate to the folder netcoreapp3.1 in the shinny-ssg folder ( path `shinny-ssg\bin\Debug\netcoreapp3.1`)
* In the command prompt run `shinny-ssg.exe -h` to see the options
* The file will be generated in the `shinny-ssg\bin\Debug\netcoreapp3.1\dist` folder by default or in the destination folder if specified by user
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

### Sample HTML file 
https://hphan9.github.io/shinny-ssg.html

### License
MIT 

### Author 
Emily Phan
  
