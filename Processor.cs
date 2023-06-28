

namespace WordStat
{
    internal class Processor
    {
        private readonly Dictionary<string, int> _map = new();
        //private readonly SortedDictionary<string, int> _map = new();

        public void Perform(Parser parser)
        {
            string word;

            while (parser.Next(out word))
            {
                if (!_map.ContainsKey(word))
                {
                    _map.Add(word, 0);
                }

                _map[word]++;
            }
        }

        public void WrireResult(string resFile, int resCount)
        {
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
