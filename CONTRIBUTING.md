# shinny-ssg Version 0.1 - CLI Tool to generate HTML File

## Prerequisites 
* .NetCore 3.1

## Setup
* Download or clone source code
* Cd to the solution repository
* Install dotnet-format with command: dotnet tool install -g dotnet-format
* Build project with command: dotnet build 
* Format code: Dotnet-format will autoformat the source code at the build time or with command: dotnet-format -a warn ./shinny-ssg.sln

## Editor/IDE Integration
 Import the file ShinnySSG-2021-11-06.vssettings to Visual Studio.

## Testing
* Test genereated files and folders: 
-> change the value of folder path variable in file ./shinny-ssgTests.src.GeneratorTests.cs with the value in your local machine
-> Using Test Explores in Visual Studio to test the project 
