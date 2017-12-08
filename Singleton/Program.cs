using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    /// <summary>
    /// Гарантирует что у класса есть только один экземпляр, и предоставляет глобальную точку доступа к нему.
    /// Эмулирует глобальные переменные в объектно-ориентированных языках программирования (доступ к рессурсу, который необходим разным частям приложения)
    /// 1. В многопоточной среде должна обеспечиваться возможность доступа к синглтону (потокобезопасность)
    /// 2. Обеспечение ленивости (создание перед первым использованием, а не при объявлении) создания синглтона
    /// Не рекомендуется использовать синглтоны с изменяемым состоянием
    /// Невозможность юнит тестирования классов, которые используют синглтон
    /// При отсутствии состояния и наличии небольшого числа операций статические методы являются более подходящим решением 
    
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }


        public class Singleton1
        {
            private static Singleton1 instance;
            public static Singleton1 Instance
            {
                get { return instance ?? (instance = new Singleton1()); }
            }
            protected Singleton1() { }
        }

        public class Singleton2
        {
            private static readonly Singleton2 instance = new Singleton2();

            public static Singleton2 Instance
            {
                get { return instance; }
            }

            /// защищённый конструктор нужен, чтобы предотвратить создание экземпляра класса Singleton
            protected Singleton2() { }
        }

        public class Singleton3
        {
            /// защищённый конструктор нужен, чтобы предотвратить создание экземпляра класса Singleton
            protected Singleton3() { }

            private sealed class SingletonCreator
            {
                private static readonly Singleton3 instance = new Singleton3();
                public static Singleton3 Instance { get { return instance; } }
            }

            public static Singleton3 Instance
            {
                get { return SingletonCreator.Instance; }
            }

        }

        public class Singleton<T> where T : class
        {
            /// Защищённый конструктор необходим для того, чтобы предотвратить создание экземпляра класса Singleton. 
            /// Он будет вызван из закрытого конструктора наследственного класса.
            protected Singleton() { }

            /// Фабрика используется для отложенной инициализации экземпляра класса
            private sealed class SingletonCreator<S> where S : class
            {
                //Используется Reflection для создания экземпляра класса без публичного конструктора
                private static readonly S instance = (S)typeof(S).GetConstructor(
                            BindingFlags.Instance | BindingFlags.NonPublic,
                            null,
                            new Type[0],
                            new ParameterModifier[0]).Invoke(null);

                public static S CreatorInstance
                {
                    get { return instance; }
                }
            }

            public static T Instance
            {
                get { return SingletonCreator<T>.CreatorInstance; }
            }

        }

        /// Использование Singleton
        public class TestClass : Singleton<TestClass>
        {
            /// Вызовет защищённый конструктор класса Singleton
            private TestClass() { }

            public string TestProc()
            {
                return "Hello World";
            }
        }

    }

    // 1. Ленивая реализация (.NET4.0)
    public sealed class LazySingleton
    {
        private static readonly Lazy<LazySingleton> _instance = new Lazy<LazySingleton>(() => new LazySingleton());
        LazySingleton()
        {
        }

        public static LazySingleton Instance
        {
            get => _instance.Value;
        }
    }
    // 2. Двойная проверка (сложнее первой)
    public sealed class DoubleCheckedLock
    {
        // поле должно быть volatile (тем самым прорядок создания объекта не поменяется 1. выделение 
        // памяти в куче, 2. конструирование объекта по указанному адресу (вызов конструктора) 3. Инициализация поля _instance)
        // не участвует в оптимизации компилятора, предполагающей доступ для одного потока. Это гарантирует, что в любой момент времени в поле будет содержаться актуальное значение.
        private static volatile DoubleCheckedLock _instance;
        private static readonly object _syncRoot = new object();
        DoubleCheckedLock()
        { }

        public static DoubleCheckedLock Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new DoubleCheckedLock();
                        }
                    }
                }
                return _instance;
            }
        }
    }
    // 3. Инициализатор статического поля
    public sealed class FieldInitSingleton
    {
        // Можно перенестии инициализацию синглтона прямо в статический конструктор

        private static readonly FieldInitSingleton _instance = new FieldInitSingleton();
        FieldInitSingleton()
        {
        }
        // Добавляение явного статического конструктора приказывает компилятору не помечать тип атрибутом beforefiledinit

        static FieldInitSingleton()
        {
        }

        public static FieldInitSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

    }

    public sealed class LazyFieldInitSingleton
    {
        private LazyFieldInitSingleton()
        {
        }

        public static LazyFieldInitSingleton Instance
        {
            get
            {
                return SingletonHolder._instance;
            }
        }
        public static class SingletonHolder
        {
            public static readonly LazyFieldInitSingleton _instance = new LazyFieldInitSingleton();
            // Пустой статический конструктор уже не нужен, если мы будем обращаться к полю _instance лишь из свойства Instance класса LazyFieldSingleton
        }
    }

 
}
