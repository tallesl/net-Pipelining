namespace PipeliningLibrary.TestApplication
{
    using PipeliningLibrary;
    using Properties;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class GenerateHtmlResult : IPipe
    {
        public object Run(dynamic input, Action<string> notifyProgress)
        {
            // GitHub user
            string user = input.User;

            // found issue by repository name
            IDictionary<string, IEnumerable<dynamic>> issues = input.Issues;

            // building the page header
            var html = new StringBuilder()
                .Append("<style>")
                .Append(Resources.Style)
                .Append("</style>")
                .Append("<h1>issues for <a href=\"https://github.com/")
                .Append(user)
                .Append("\">")
                .Append(user)
                .Append("</a></h1>")
                .Append("<p>")
                .Append(issues.Select(kvp => kvp.Value.Count()).Sum())
                .Append(" open issues where found on ")
                .Append(DateTime.Now)
                .Append("</p>");

            foreach (var kvp in issues)
            {
                // building repository header
                html.Append("<h2><a href=\"http://github.com/tallesl/")
                    .Append(kvp.Key)
                    .Append("\">")
                    .Append(kvp.Key)
                    .Append("</a></h2>")
                    .Append("<ul>");

                foreach (var issue in  kvp.Value)
                {
                    string url = issue.html_url;
                    int number = issue.number;
                    string title = issue.title;

                    // building issue item
                    html.Append("<li><a href=\"")
                        .Append(url)
                        .Append("\">#")
                        .Append(number)
                        .Append("</a>&nbsp;")
                        .Append(title)
                        .Append("</li>");
                }

                html.Append("</ul>");
            }

            // writing it all out
            File.WriteAllText("index.html", html.ToString());

            // here, take it
            return input;
        }
    }
}
