using System.Net;

namespace GoogleTranslateI18nText
{
    internal class Program
    {

        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {

            log.Debug("Welcome to i18ntext rough translator!");


            string srcLang = "en";
            string dstLang = "ja";
            string i18nTextFilesDir = "./../../../i18ntext/";


            var translator = new GoogleTranslate();

            //string[] files = Directory.GetFiles("./../../../i18ntext/", "*.json", SearchOption.AllDirectories);
            var files = new I18ntextReader().GetMatchingFiles(i18nTextFilesDir, srcLang);

            foreach (var file in files)
            {

                var keyValues = new I18ntextReader().ReadFile(file);
                var existentKeyValues = new I18ntextReader().ReadTranslatedFile(file, srcLang, dstLang);

                foreach (var keyValue in keyValues)
                {

                    if (existentKeyValues != null && existentKeyValues.ContainsKey(keyValue.Key))
                    {
                        log.Debug("Will use existent translation");
                        log.Debug($"[{keyValue.Key}] -> {existentKeyValues[keyValue.Key]}");

                        //avoid translating manually edited records
                        keyValues[keyValue.Key] = existentKeyValues[keyValue.Key];

                        continue;
                    }

                    var translated = translator.Translate(keyValue.Value, srcLang, dstLang);

                    log.Debug($"[{keyValue.Key}] -> {keyValues[keyValue.Key]} -> {translated}");

                    //alter key again with trnsalted text now
                    keyValues[keyValue.Key] = translated;
                    
                }

                new I18ntextReader().GenerateTranslatedFile(file, srcLang, dstLang, keyValues);

            }


            //for (int i = 0; i < 100; i++)
            //{
            //    var content = translator.Translate("arrivederci", "it", "en");
            //    Console.WriteLine(content);
            //}

            log.Info("-----------------------------");
            log.Info("DONE");
            log.Info("-----------------------------");

            Console.ReadLine();

        }
    }
}