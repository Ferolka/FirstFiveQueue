using FirstFiveQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFiveQueue.Repositories
{
    public interface IQueueLineRepository
    {
        List<QueueLine> ClientLine();
        Task<Result> AddToLine(QueueLine queueLine);
        Task<Result> NextClient(QueueLine queueLine);
        QueueLine ClientInService();
    }
}
