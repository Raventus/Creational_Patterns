using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Строитель отделяет конструирование сложного объекта от его представления, так, что в результате одного 
/// и того же процесса конструирования могут получаться разные представления.
/// СОздаваемому объекту нужно передать множество аргументов, часть из которых нужна одним клиентам, а часть другим. Фабричный метод 
/// с десятью аргументами, 9 из которых не используется не удачное решение
/// </summary>
namespace Builder
{

    class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();

            Builder b1 = new ConcreteBuilder1();
            Builder b2 = new ConcreteBuilder2();

            // Construct two products
            director.Construct(b1);
            Product p1 = b1.GetResult();
            p1.Show();

            director.Construct(b2);
            Product p2 = b2.GetResult();
            p2.Show();

            // Wait for user
            Console.Read();
        }
    }

    class Director
    {
        // Builder uses a complex series of steps
        public void Construct(Builder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }

    abstract class Builder
    {
        public virtual void BuildPartA() { }
        public virtual void BuildPartB() { }
        public abstract Product GetResult();
    }

    class ConcreteBuilder1 : Builder
    {
        private readonly Product product = new Product();

        public override void BuildPartA()
        {
            product.Add("PartA");
        }

        public override void BuildPartB()
        {
            product.Add("PartB");
        }

        public override Product GetResult()
        {
            return product;
        }
    }

    class ConcreteBuilder2 : Builder
    {
        private readonly Product product = new Product();

        public override void BuildPartA()
        {
            product.Add("PartX");
        }

        public override void BuildPartB()
        {
            product.Add("PartY");
        }

        public override Product GetResult()
        {
            return product;
        }
    }

    class Product
    {
        private readonly List<string> parts = new List<string>();

        public void Add(string part)
        {
            parts.Add(part);
        }

        public void Show()
        {
            Console.WriteLine("\nProduct Parts -------");
            foreach (string part in parts)
                Console.WriteLine(part);
        }
    }
}
