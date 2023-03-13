

namespace WordStat
{
    internal class Processor
    {
        private readonly Parser _parser;
        private readonly Dictionary<string, int> _map = new();

        public Processor(Parser parser)
        {
            _parser = parser;
        }

        public void Do(string resFile, int resCount)
        {
            string word;

            while (_parser.Next(out word))
            {
                if (!_map.ContainsKey(word))
                    _map.Add(word, 0);

                _map[word]++;
            }

            var list = new List<ExprItem>();
            foreach (var pair in _map)
            {
                list.Add(new ExprItem()
                {
                    Text = pair.Key,
                    Count = pair.Value
                });
            }

            list.Sort((c1, c2) => c1.Count.CompareTo(c2.Count));
            list.Reverse();

            using var writer = new StreamWriter(resFile);
            for (var i = 0; i < resCount; i++)
            {
                writer.WriteLine($"{list[i].Count}: {list[i].Text}");
            }

        }

    }
}
