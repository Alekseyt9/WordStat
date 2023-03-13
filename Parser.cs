
using System.Text;

namespace WordStat
{
    internal class Parser
    {
        private const int s_BufLen = 100;
        private static readonly HashSet<char> s_CharSet = new() { '\'', '’' };

        private int _queueLen;
        private StreamReader _reader;
        private int _bufIndex;
        private char[] _buffer = new char[s_BufLen];
        private int _bufLen;

        private readonly Queue<string> _queue = new Queue<string>();

        public Parser(int qCount)
        {
            _queueLen = qCount;
        }

        public void OpenFile(string fileName)
        {
            _reader = new StreamReader(fileName);
        }

        private bool NextExpression(out string str)
        {
            string word;
            str = null;

            if (_queue.Count < _queueLen)
            {
                while (_queue.Count < _queueLen)
                {
                    if (NextWord(out word))
                    {
                        _queue.Enqueue(word);
                    }
                    else
                    {
                        return false;
                    }
                }

                str = string.Join(" ", _queue.ToArray());
                return true;
            }

            if (NextWord(out word))
            {
                _queue.Dequeue();
                _queue.Enqueue(word);
                str = string.Join(" ", _queue.ToArray());
                return true;
            }

            return false;
        }

        private bool NextWord(out string str)
        {
            char c;
            var sb = new StringBuilder(50);
            bool inWord = false;

            while (NextChar(out c))
            {
                if (char.IsLetter(c) || s_CharSet.Contains(c))
                {
                    sb.Append(c);
                    inWord = true;
                }
                else
                {
                    if (inWord)
                        break;
                }
            }

            str = sb.ToString().ToLowerInvariant();
            return sb.Length > 0;
        }

        private bool NextChar(out char c)
        {
            if (_bufIndex > _bufLen -1)
            {
                if (NextBuffer(ref _buffer))
                {
                    _bufIndex = 0;
                }
                else
                {
                    c = '-';
                    return false;
                }
            }

            c = _buffer[_bufIndex++];
            return true;
        }

        private bool NextBuffer(ref char[] buffer)
        {
            _bufLen = _reader.Read(buffer, 0, s_BufLen);
            return _bufLen > 0;
        }

        public bool Next(out string token)
        {
            return NextExpression(out token);
        }

        public void Close()
        {
            _reader.Close();
        }
        
    }
}
