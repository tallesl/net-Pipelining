namespace PipeliningLibrary.TestApplication
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;

    public static class Http
    {
        public static IEnumerable<dynamic> Get(string url, params object[] args)
        {
            using (var webClient = new WebClient())
            {
                // GitHub API forces you to set an user agent
                // https://developer.github.com/v3/#user-agent-required
                webClient.Headers.Add("User-Agent", "Donald Duck");

                var _url = string.Format(url, args);
                var json = webClient.DownloadString(_url);

                return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(json);
            }
        }
    }
}
