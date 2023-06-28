

using System.Globalization;

namespace WordStat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var processor = new Processor();

            var path = @"d:\eng_corp\1-billion-word-language-modeling-benchmark-r13output\";
            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories).ToList();

            var i = 0;
            foreach (var file in files)
            {
                var proc = Math.Floor(100 * i / (float)files.Count);
                Console.Write($@"{proc.ToString(CultureInfo.InvariantCulture),3}%");
                Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop);

                var parser = new Parser(4);
                parser.OpenFile(file);
                processor.Perform(parser);
                parser.Close();
                i++;
            }

            processor.WrireResult("1-billion-word_res_4.txt", 500);
            //Console.ReadLine();
        }

    }
}