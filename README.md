# shinny-ssg Version 0.1 - CLI Tool to generate HTML File

## Features
* generate .html files from .txt files and .md files
* accept folder as input path to generate .html files in the destionation folder which will has the same structure as the original folder
* give user the options to specify destination folder, language attribute for HTML files and/or stylesheet url link 

## How to use

### Using source code
* Download or clone the shinny-ssg folder to your local machine
* Use command prompt to navigate to the folder netcoreapp3.1 in the shinny-ssg folder ( path `shinny-ssg\bin\Debug\netcoreapp3.1`)
* In the command prompt run `shinny-ssg.exe -h` to see the options
* The file will be generated in the `shinny-ssg\bin\Debug\netcoreapp3.1\dist` folder by default or in the destination folder if specified by user
* If there is no error, the app exits with code 0, otherwhise it exits with code -1

### Using Nuget package
#### global
> dotnet tool install --global shinny_ssg --version 1.0.1
>
#### local
> dotnet new tool-manifest # if you are setting up this repo
> dotnet tool install --local shinny_ssg --version 1.0.1

### Command
> shinny-ssg -h 
> 
> shinny-ssg -i `<filePath>` 
>
> shinny-ssg -i `<path>` -o `<folder path>` -s `<stylesheet link>`

Option	Function
 

| OPTION             | Function                            | 
| ------------------ |:-----------------------------------:| 
| -v --version       | 	Name and version                   | 
| -h --help          |  Information                        |   
| -s --stylesheet    | <'link-to-css-stylesheet'>	       | 
| -i --input         | specifies an input file or folder   | 
| -o --output        | specifies destination folder        | 
| -l --lang          | specifies language atrribute        | 
| -c --config        | accept a file path to a JSON config file |


### Built with
* .NET CORE 3.1
* [Command Line Utils](https://github.com/natemcmaster/CommandLineUtils)

### Sample HTML file 
https://hphan9.github.io/shinny-ssg.html

### License
MIT 

### Author 
Emily Phan 
  
