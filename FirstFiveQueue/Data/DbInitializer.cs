using FirstFiveQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFiveQueue.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context, IServiceProvider service)
        {
            context.Database.EnsureCreated();
            if (context.QueueLines.Any())
            {
                return;
            }
            CreateQueueLines(context);
            return; 
        }
        private static void CreateQueueLines(AppDbContext context)
        {
        var q1 = new QueueLine { FullName = "Ivan Ivanovich",ClientStatus=ClientStatus.Closed };
        var q2 = new QueueLine { FullName = "Petro Petrovich", ClientStatus = ClientStatus.Closed };
        var q3 = new QueueLine { FullName = "Semen Semenovich", ClientStatus = ClientStatus.Closed };
        var queues = new List<QueueLine>() { q1, q2, q3 };
        try
            {   
                context.AddRange(queues);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
