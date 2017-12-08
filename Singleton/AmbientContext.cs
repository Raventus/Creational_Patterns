using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    // AmbientContext/ От Singleton можно наследоваться и в начале работы приложения можно выбрать подходящий экземпляр
    public interface ILogger
    {
        void Write();
    }

    internal class DefaultLogger : ILogger
    {
        public void Write()
        {
            throw new NotImplementedException();
        }
    }

    public class GlobalLogger
    {
        private static ILogger _logger = new DefaultLogger();
        // Классы этой сборки (или друзья) смогут задать нужный экземпляр логгера

        public static ILogger Logger
        {
            get
            {
                return _logger;
            }
            set
            {
                _logger = value;
            }
        }
    }
}
