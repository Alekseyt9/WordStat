
using Ganss.Text;

namespace WordStat
{
    internal class Processor
    {
        private readonly Dictionary<string, int> _map = new();
        //private readonly SortedDictionary<string, int> _map = new();

        public void Perform(Parser parser, List<string> stopList)
        {
            string word;
            var ac = new AhoCorasick(stopList);
            
            while (parser.Next(out word))
            {
                var results = ac.Search(word);

                if (results.Any())
                {
                    continue;
                }

                if (!_map.ContainsKey(word))
                {
                    _map.Add(word, 0);
                }

                _map[word]++;

                if (_map.Count > 10000000)
                {
                    break;
                }

            }
        }

        public void WriteResult(string resFile, int resCount)
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
