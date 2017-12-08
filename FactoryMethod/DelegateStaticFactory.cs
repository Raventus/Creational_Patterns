using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    static class ImporterFactory
    {
        private static readonly Dictionary<string, Func<Importer>> _map = new Dictionary<string, Func<Importer>>();

        static ImporterFactory()
        {
            _map[".json"] = ()=> new JsonImporter();
            _map[".xls"] = ()=> new XlsImporter();
            _map[".xslx"] = () => new XslxImporter(); 
        }

        public static Importer Create(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var creator = GetCreator(extension);
            if (creator == null)
            {
                throw new UnsupportedImporterTypeException(extension);
            }
            return creator;
        }
        
        private static Func<Importer> GetCreator (string extension)
        {
            Func<Importer> creator;
            _map.TryGetValue(extension, out creator);
            return creator;
        }
    }

    // 2/ Обобщенные фабрики (вызов постобработки после создания объекта)
    public abstract class Product
    {
        protected internal abstract void PostConstruction();
    }

    public class ConcreteProduct : Product
    {
        // Внутренний конструктор не позволяет клиентам иерархии создавать объекты напрямую
        internal ConcreteProduct() { }

        protected internal override void PostConstruction()
        {
            Console.WriteLine("ConcreteProduct: post construction");
        }
    }

    // единственный законный способ создания объектов семейства Product
    public static class ProductFacrtory
    {
        public static T Create<T> () where T:Product, new()
        {
            try
            {
                var t = new T();
                // Вызываем постобработку
                t.PostConstruction();
                return t;
            }
            catch (TargetInvocationException e)
            {
                var edi = ExceptionDispatchInfo.Capture(e.InvalidException);
                edi.Throw();
                return default(T); // недостежима, но компилятор не знает об этом
            }
        }
    }
}
