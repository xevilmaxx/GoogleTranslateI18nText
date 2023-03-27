using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.FileSystemGlobbing;
using Newtonsoft.Json;
using System.IO;

namespace GoogleTranslateI18nText
{
    public class I18ntextReader
    {

        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public List<string> GetMatchingFiles(string rootPath, string SrcLang)
        {
            var fileNames = new List<string>();

            try
            {
                // Create a matcher object
                var matcher = new Matcher();
                matcher.AddInclude($"**/*{SrcLang}.json");

                // Match the files against the pattern recursively
                var result = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(rootPath)));

                // Get the file names from the result
                foreach (var file in result.Files)
                {
                    fileNames.Add(new FileInfo(Path.Combine(rootPath, file.Path)).FullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return fileNames;
        }

        public Dictionary<string, string> ReadFile(string FilePath)
        {
            try
            {

                var fileContent = File.ReadAllText(FilePath);
                
                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileContent);

                return result;

            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        public bool GenerateTranslatedFile(string FilePath, string SrcLang, string DstLang, Dictionary<string, string> TranslatedData)
        {
            try
            {

                var adjustedFilePath = FilePath.Replace($"{SrcLang}.json", $"{DstLang}.json");

                var newFileContent = JsonConvert.SerializeObject(TranslatedData, Formatting.Indented);

                File.WriteAllText(adjustedFilePath, newFileContent);

                return true;

            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }

    }
}
