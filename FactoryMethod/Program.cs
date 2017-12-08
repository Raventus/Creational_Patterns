using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodMaon
{
    //Определяет интерфейс для создания объекта, но оставляет подклассам решение о том, какой класс инстанцировать. Фабричный метод позволяет классу 
    /// <summary>
    /// делегировать инстанцирование подклассам
    /// Уель любой фабрики - оградить клиентов от подробностей создания экземпляров класса или иерархии классов
    /// Классический фабричный метод является частным случаем Шаблонного метода переменный шаг которого отвечает за создание нужного объекта
    /// Отличие статического фабричного метода от классики в том, что тип создаваемого фабрикой объекта определяется не типом наследника, а аргументами, прередаваемые методы Create
    /// Полиморфный фабричный метод определяет интерфейс фабрики, а за создание конкретного экземпляра продукта отвечает конкретная фабрика
    /// Фабричный метод позволяет модифицироватьт иерархию наследования не затрагивая при этом существующих клиентов
    /// Пример метод FromSeconds структуры TimeSpan
    /// Фабричный метод может быть асинхронным. Когда сложность или время исполнения конструктора переходит определенную черту используют фабричный метод.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Creator[] creators = { new ConcreteCreatorA(), new ConcreteCreatorB() };
            foreach (Creator creator in creators)
            {
                Product product = creator.FactoryMethod();
                Console.WriteLine("Created {0}", product.GetType());
            }
            Console.Read();
        }

    }

    abstract class Product
    {
        public abstract string GetType();
    }

    class ConcreteProductA : Product
    {
        public override string GetType() { return "ConcreteProductA"; }
    }

    class ConcreteProductB : Product
    {
        public override string GetType() { return "ConcreteProductB"; }
    }

    abstract class Creator
    {
        public abstract Product FactoryMethod();
    }

    class ConcreteCreatorA : Creator
    {
        public override Product FactoryMethod() { return new ConcreteProductA(); }
    }

    class ConcreteCreatorB : Creator
    {
        public override Product FactoryMethod() { return new ConcreteProductB(); }
    }
}
