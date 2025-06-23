using System;
using System.Reflection.Metadata.Ecma335;

namespace CodingPrinciples
{
    //Signle Responsibility Principle(SRP)
    // class should have one, and only one, reason to change

    //т.е. каждый класс отвечает только за одну задачу.

    #region BAD EXAMPLE
    class Report
    {
        public string? Text { get; set; }

        public void Print() { }
        public void SendToEmail() { }
        public void SaveToFile() { }
    }
    #endregion
    #region GOOD EXAMPLE
    class Report_
    {
        public string? Text { get; set; }
    }
    class PrintReport
    {
        public void Print(Report_ report) { }
    }
    class SaveReport
    {
        public void SaveToFile(Report_ report, string filepath) { }
    }


    #endregion


    //O - Open/Closed Principle(OCP)
    //software entities (classes,modules,functions,etc) should be open for extension,
    //but closed for modification

    //т.е. добавлять функциональность, не трогая то, что реализовано


    //L - Liskov Substitution Principle(LSP)
    //подкласс должен вести себя как родительский класс, не ломая его логику

    #region BAD SOLUTION
    class Rectangle
    {
        protected double _x;
        protected double _y;
        protected double _width;
        protected double _height;

        public Rectangle()
        {
            _x = _y = 0;
            _width = _width = 0;
        }
        public Rectangle(double width, double height)
        {
            _width = width;
            _height = height;
            _x = 0;
            _y = 0;
        }
        public Rectangle(double x, double y, double width, double height) : this(x, y)
        {
            _width = width;
            _height = height;
        }

        public double getX() => _x;
        public double getY() => _y;
    }

    class Square : Rectangle { } // square doesn't have width and height altogether. 

    #endregion
    #region GOOD SOLUTION

    abstract class Figure
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    class Rectangle_ : Figure { }
    class Square_ : Figure { }
    #endregion



    //I - Interface Segregation Principle(ISP)
    //many client-specific interfaces are better than one general-purpose interface


    //D-Dependency Inversion Principle(DIP)
    //Depend on abstractions, not on concretions

    //1.Модули верхних уровней должны зависеть от модулей нижних уровней, но оба типа модулей должны зависеть от абстракции

    //2.Абстракции не должны зависеть от деталей. Детали должны зависеть от абстракций

    #region BAD SOLUTION
    class ConsoleLogger
    {
        public void log(string[] args) { }
    }

    class OrderService //this class is depended on ConsoleLogger
    {
        private readonly ConsoleLogger _logger = new ConsoleLogger();
        private void ProccessOrder(int orderid) { }
    }
    #endregion
    #region GOOD SOLUTION
    interface ILogger
    {
        void Log(string mes);
    }

    class Logger : ILogger
    {
        public void Log(string mes)
        {
            Console.WriteLine(mes);
        }
    }

    class Order
    {
        private readonly ILogger _logger = new Logger(); //now Order isn't depended on any exact class
        public void ProcessOrder(int ordered)
        {
            _logger.Log($"Order ID:{ordered} processed!");
        }
    } 
    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            string logFileName = $"Logs_{DateTime.Now.DayOfWeek}.txt";

        }
    }
}
