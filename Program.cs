
using System.Globalization;

namespace WordStat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var processor = new Processor();

            var path = @"d:\eng_corp\subtitles\";
            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories).ToList();
            //var files = new List<string>() { @"d:\eng_corp\reddit.txt" };

            var stopList = new List<string>()
            {
                "share user", "avatar level", "user avatar", "reply",
                "hr ", "share more", "ago ", "day ago", "top karma",
                "golden showers", "share save", "best search", "comments user",
                "comment as", "alekseyt", "sort by", "comment markdown",
                "sort by best", "more replies", "comment deleted",
                "newspapernelson", "prolific commenter", "min ago",
                "hz monitor", "aw dw", "gb ddr", "inch wqhd", 
                "inch wqhd", "dw new", "gold certified", "deleted by user",
                "focus st", "comment removed", "here comment",
                "replace this text", "sir excuseme", "removed by moderator",
                "view discussions", "questions or concerns",
                "automatically please", "this action was", "a bot",
                "automoderator", "discussions in other", "poster hr",
                "achievement hr", "twitter video", "de mb", "audi a", "mb glc",
                "link info", "https www", "info feedback", "share continue", "community user",
                "youtube com", "in other communities", "top poster", "shall be free", 
                " op ", "op avid", "wolf op", " atx ", "no no ", "wikipedia org",
                "modular atx", "titan bot", "avid voter", "award share",
                "markdown mode", "share level", "moderator", "cake day",
                "comments view", "rammskie op", "year club", "by user",
                "youtu be", "time top", "communities user", "collateralcoyote op", "rauns op",
                "wucky", "oh reddit", "now now", "fucky", "detonation op", "reddit", "mad titan",
                "lt ref", " web", " lt ", " google ", " cite ",
                " status ", "http", " archive ", "quot", " xml ",
                " pages ", "wiki", "format", "google", " gt ", " align ",
                "revision", " id ", " br ", "timestamp", "publisher", " ns ", "date",
                "xml", "access", "preserve", "www", "perseus", "bbc", "amp",
                "hopper", "dmy", "isbn", "links", "pg pa", "mdy", "mode",
                "suu", "shell", "language en", "page", "see also",
                "abbr", "doi", "awb", "oclc", "fix", "c c", "'''", "journal",
                "div col", "username", "minor", "book", "ref name",
                "reference", "telegraph", "name list", "transliteration", "pmid",
                "record", "category", "co uk", "player", "issn", "gt ",
                "jpg", " com ", " php ", " net ", " n a", "alsj", "greeklit",
                "align center", "content", "d d ", " b d", " d d", "sfnm", "library",
                "ncbi", "p p ", " p p", "math", " sub ", "url", "status",
                "news", "parentid", "s cid", " code", "lt ", "archive", "math",
                "cite news", "i i ", "' '", "d f r", "x x ", "esq", "i i i", "a b c",
                "amp c", "w w w", "m d f", "n n n", "f r s", "a a a", "sync",
                "class", "meow", "font", "la la"
            };

            var i = 0;
            foreach (var file in files)
            {
                var proc = Math.Floor(100 * i / (float)files.Count);
                Console.Write($@"{proc.ToString(CultureInfo.InvariantCulture),3}%");
                Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop);

                var parser = new Parser(3);
                parser.OpenFile(file);
                processor.Perform(parser, stopList);
                parser.Close();
                i++;
            }

            processor.WriteResult("sub_res_3.txt", 1000);
            //Console.ReadLine();
        }

    }
}