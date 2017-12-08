using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    public class LogReader
    {
        private readonly Func<Stream> _streamFactory;

        private LogReader (Func<Stream> streamFactory)
        {
            _streamFactory = streamFactory;
        }

        public static LogReader FromFile (string fileName)
        {
            Func<Stream> factory = () => new FileStream(fileName, FileMode.Open);
            return new LogReader(factory);
        }

        public static LogReader FromStream (Stream stream)
        {
            Func<Stream> factory = () => stream;
            return new LogReader(factory);
        }

        public IEnumerable<LogEntry> Read()
        {
            using (var stream = OpenLogSource())
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return LogEntryParser.Parse(line);
                }
            }
        }

        private Stream OpenLogSource ()
        {
            return _streamFactory();
        }
        
    }
}
