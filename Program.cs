namespace WordStat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(4);
            parser.OpenFile("fra_news_2022_1M-sentences.txt");
            var processor = new Processor(parser);
            processor.Do("fra_1m_4.txt", 500);
            parser.Close();

            Console.ReadLine();
        }

    }
}