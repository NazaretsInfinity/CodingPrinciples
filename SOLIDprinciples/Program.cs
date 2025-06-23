using System.Xml.Linq;

namespace SolidTask
{
    public class Order
    {
        public int Id { get; set; }
        public required string CustomerEmail { get; set; }
        public bool IsValid { get; set; }
    }
    
    // Interfaces -------------
    public interface ISaveBase
    {
        void Save(Order order);
    }

    public interface ISendMessage
    { 
        void SendEmail(string email,string message);
    }
   // --------------------------
    public class SqlDatabase : ISaveBase
    {
        public void Save(Order order)
        {
            Console.WriteLine($"Saving order #{order.Id} to SQL database...");
        }
    }
    public class SmtpEmailSender : ISendMessage 
    {
        public void SendEmail(string email, string message)
        {
            Console.WriteLine($"Sending email to {email}: {message}");
        }
    }
  

    interface IProcessOrder 
    {
        bool ProcessOrder(Order order);
    }

    public class OrderService : IProcessOrder
    {
        private readonly ISaveBase _database;
        private readonly ISendMessage _emailSender;

        public OrderService(ISaveBase database, ISendMessage emailSender)
        {
            _database = database;
            _emailSender = emailSender;
        }

        public bool ProcessOrder(Order order)
        {
            if (!order.IsValid)
                return false;
            _database.Save(order);
            _emailSender.SendEmail(order.CustomerEmail, "Order confirmed!");
            return true;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Random random = new();

            ISaveBase database = new SqlDatabase();
            ISendMessage sender = new SmtpEmailSender();
            IProcessOrder orderService = new OrderService(database, sender);
            for (int i = 0; i < 8; i++)
            {
                Order order = new()
                {
                    Id = random.Next(),
                    CustomerEmail = $"user{random.Next()}@example.ru",
                    IsValid = true
                };
                orderService.ProcessOrder(order);
            }
        }
    }
}
