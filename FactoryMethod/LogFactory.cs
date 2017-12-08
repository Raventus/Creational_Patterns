using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public static class LogEntryParser
    {
        public static LogEntry Parser(string data)
        {
            // Анализирует содержание дата и создает нужный экземпляр Эксептион или симпл лог ентри
            throw new NotImplementedException();
        }

        internal static LogEntry Parse(string line) // в зависимости от параметра возвращает нужный объект LogEntry
        {
            throw new NotImplementedException();
        }
    }

    public class LogEntry
    {
    }

    public abstract class LogReaderBase
    {
        public IEnumerable<LogEntry> Read()
        {
            using (var stream = OpenLogSource()) // В зависимости от типа LogEntry возвращает нужный стрим
            {
                using (var reader = new StreamReader(stream))
                {
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return LogEntryParser.Parse(line);
                    }
                }
            }
        }

        protected abstract Stream OpenLogSource();
    }
}
