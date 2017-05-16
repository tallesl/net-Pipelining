namespace PipeliningLibrary.TestApplication
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            // initializing the pipeline
            Pipeline.Expando();
            Pipeline.Register("open_issues")
                .Pipe<GetUserRepositories>()
                .Pipe<FilterOpenIssues>()
                .Pipe<GetIssuesDetails>()
                .Pipe<GenerateHtmlResult>();

            // asking the GitHub user
            Console.Write("GitHub user: ");
            var user = Console.ReadLine();

            // running the pipeline
            var task = Pipeline.Run(

                // id of the pipeline to run
                "open_issues",

                // input of this run
                new
                {
                    User = user,
                    PerPage = 10,
                },

                // an action to handle events
                e =>
                {
                    if (e is PipeStarted)
                        Console.WriteLine("\"{0}\" started.", e.Pipe.Name);

                    if (e is PipeEnded)
                        Console.WriteLine("\"{0}\" ended.{1}", e.Pipe.Name, Environment.NewLine);

                    if (e is PipeException)
                        Console.WriteLine("An exception just happened: {0}", ((PipeException)e).Exception);

                    if (e is PipeMessage)
                        Console.WriteLine(((PipeMessage)e).Message);
                }
            );

            // the pipeline result
            var result = task.Result;

            // saving a "result.json" with the pipeline result
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText("result.json", json);

            // if the pipeline was successfull we open the resulted file
            if (result.Success)
                Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "index.html"));

            // else we open the result file with the error
            else
                Process.Start("notepad", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.json"));
        }
    }
}