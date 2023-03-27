using Newtonsoft.Json.Linq;
using System.Net;

namespace GoogleTranslateI18nText
{
    public class GoogleTranslate
    {

        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private WebClient HttpClient = new WebClient();
        private string GoogleEndpoint = "https://translate.googleapis.com/translate_a/single?";
        private string DataPlaceholder = "client=gtx&sl={0}&tl={1}&dt=t&q={2}";

        public string Translate(string Text, string SrcLang, string DstLang)
        {
            try
            {

                var result = HttpClient.DownloadString(GoogleEndpoint + string.Format(DataPlaceholder, SrcLang, DstLang, Text));

                var json = JArray.Parse(result);

                var translatedRes = json.First.First.First.Value<string>();

                return translatedRes;

            }
            catch(Exception ex)
            {
                log.Error(ex);
                return Text;
            }
        }

    }
}
