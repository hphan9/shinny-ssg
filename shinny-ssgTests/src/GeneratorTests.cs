using Shinny_ssg;
using Xunit;
using shinny_ssg;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
namespace Shinny_ssg.Tests
{
    public class GeneratorTests
    {
        [Fact()]
        public void GeneratorTest_ConfigOption_InputFolderNotNested()
        {
            //Arrange
            var options = new CommandLineOptions();
            SetUpOptions(options, "ConfigFile", @"C:\Users\khoit\Desktop\OSD600\shinny-ssg\shinny-ssgTests\testConfig.json");
            var expectedOutputFolder = new DirectoryInfo(@"C:/Users/khoit/Desktop/OSD600/dist");
            var expectedInputFolder = new DirectoryInfo(@"C:/Users/khoit/Desktop/OSD600/Sherlock-Holmes-Selected-Stories");
            var generator = new Generator(options);
            //Action
            generator.Run();
            var inputFiles = expectedInputFolder.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(file => file.Name.EndsWith(".txt") || file.Name.EndsWith(".md"));
            var outputFiles = expectedOutputFolder.EnumerateFiles("*.*", SearchOption.AllDirectories);
            //Assert
            Assert.True(expectedOutputFolder.Exists, "OutputFolder must exist");
            Assert.Equal<int>(inputFiles.Count(), outputFiles.Count());
            foreach (FileInfo f in inputFiles)
            {
                var nameWithoutExtension = Path.GetFileNameWithoutExtension(f.FullName);
                var result = outputFiles.Where(outputFile => Path.GetFileNameWithoutExtension(outputFile.FullName) == nameWithoutExtension).Select(outputFile => outputFile);
                Assert.True(result?.First().Extension == ".html", "OutputFile must be Html");
            }
        }

        [Fact()]
        public void GeneratorTest_InputOption_InputFolderNotNested()
        {
            //Arrange
            var options = new CommandLineOptions();
            SetUpOptions(options, "InputPath", @"C:/Users/khoit/Desktop/OSD600/Sherlock-Holmes-Selected-Stories");
            var expectedOutputFolder = new DirectoryInfo(@"C:/Users/khoit/Desktop/OSD600/shinny-ssg/shinny-ssg/bin/Debug/netcoreapp3.1/dist");
            var expectedInputFolder = new DirectoryInfo(@"C:/Users/khoit/Desktop/OSD600/Sherlock-Holmes-Selected-Stories");
            var generator = new Generator(options);
            //Action
            generator.Run();
            var inputFiles = expectedInputFolder.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(file => file.Name.EndsWith(".txt") || file.Name.EndsWith(".md"));
            var outputFiles = expectedOutputFolder.EnumerateFiles("*.html*", SearchOption.AllDirectories);
            //Assert
            Assert.True(expectedOutputFolder.Exists, "OutputFolder must exist");
            Assert.Equal<int>(inputFiles.Count(), outputFiles.Count());
            foreach (FileInfo f in inputFiles)
            {
                var nameWithoutExtension = Path.GetFileNameWithoutExtension(f.FullName);
                var result = outputFiles.Where(outputFile => Path.GetFileNameWithoutExtension(outputFile.FullName) == nameWithoutExtension).Select(outputFile => outputFile);
                Assert.True(result?.First().Extension == ".html", "OutputFile must be Html");
            }
        }

        [Fact()]
        public void GeneratorTest_InvalidInputOption()
        {
            //Arrange
            var options = new CommandLineOptions();
            SetUpOptions(options, "InputPath", String.Empty);
            var generator = new Generator(options);

            //Action
            var ex = Assert.Throws<Exception>(() => generator.Run());

            //Assert
            Assert.Equal("Either Input or Config File must have a valid value ", ex.Message);
        }



        [Fact()]
        public void GetPageTest()
        {
            var text = @"Silver Blaze


I am afraid, Watson, that I shall have to go,” said Holmes, as we
sat down together to our breakfast one morning.";
            var cssUrl = @"https://cdn.jsdelivr.net/npm/water.css@2/out/water.css";
            var langAtr = "en-CA";
            var page = new Page(text, cssUrl, langAtr);
            var expectedResult = "<!DOCTYPE html> <html lang= \"en-CA\"> <head> <meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/><link rel =\"stylesheet\"href =\"https://cdn.jsdelivr.net/npm/water.css@2/out/water.css\" ><title >Silver Blaze </title ><meta name=\"description\" content=\"Page is generated with ShinnySSG tool \"/><meta name = \"viewport\" content = \"width=device-width, initial-scale=1\"><meta property=\"og: title\" content=\"Silver Blaze\" /><meta property = \"og:image\" content = \"https://pixabay.com/images/id-761653/\" /> </head> <body><h1>Silver Blaze</h1><p> I am afraid, Watson, that I shall have to go,” said Holmes, as we sat down together to our breakfast one morning.</p></body> </html>";
            //Action
            var result = page.GetPage();
            //Assert
            Assert.Equal(expectedResult, result);
        }


        private void SetUpOptions(CommandLineOptions options, string propertyName, object value)
        {
            var type = typeof(CommandLineOptions);
            var property = type.GetProperty(propertyName);
            property.SetValue(options, value);
        }
    }
}