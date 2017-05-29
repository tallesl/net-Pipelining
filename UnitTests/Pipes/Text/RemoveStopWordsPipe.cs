namespace PipeliningLibrary.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;

    public class RemoveStopWordsPipe : IPipe
    {
        private readonly string[] _stopWords;

        public RemoveStopWordsPipe(string[] stopWords)
        {
            _stopWords = stopWords;
        }

        public object Run(dynamic input)
        {
            IEnumerable<string> words = input;
            return words.Where(w => !_stopWords.Contains(w.ToLowerInvariant()));
        }
    }
}
