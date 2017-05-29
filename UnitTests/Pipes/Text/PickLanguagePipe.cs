namespace PipeliningLibrary.UnitTests
{
    using System;

    public class PickLanguagePipe : IBranchPipe
    {
        public BranchOutput Run(dynamic input)
        {
            string text = input.Text;
            string language = input.Language;

            switch (language.ToLowerInvariant())
            {
                case "english":
                    return new BranchOutput("extract_keywords_en", text);

                case "spanish":
                    return new BranchOutput("extract_keywords_es", text);

                case "portuguese":
                    return new BranchOutput("extract_keywords_pt", text);

                case "exception":
                    return new BranchOutput("extract_keywords_ex", text);

                default:
                    throw new InvalidOperationException(string.Format("Unknown language: \"{0}\".", language));
            }
        }
    }
}
