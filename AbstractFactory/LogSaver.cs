using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class LogSaver
    {
        // Фабрика фабрик
        private readonly DbProviderFactory _factory;
        public LogSaver(DbProviderFactory factory)
        {
            _factory = factory;
        }
        public void Save(IEnumerable<LogEntry> logEntries)
        {
            using (var connection = _factory.CreateConnection())
            {
                SetConnectionString(connection);
                using (var command = _factory.CreateCommand())
                {
                    SetCommandArguments(logEntries);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SetCommandArguments(IEnumerable<LogEntry> logEntries)
        {
            throw new NotImplementedException();
        }

        private void SetConnectionString(DbConnection connection)
        {
            throw new NotImplementedException();
        }
    }

    public class LogEntry
    {
    }
}
