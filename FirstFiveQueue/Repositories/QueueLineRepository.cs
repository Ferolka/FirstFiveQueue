using FirstFiveQueue.Data;
using FirstFiveQueue.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFiveQueue.Repositories
{
    public class QueueLineRepository : IQueueLineRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<QueueLineRepository> _logger;
        public QueueLineRepository(AppDbContext context, ILogger<QueueLineRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<QueueLine> ClientLine()
        {
            return _context.QueueLines.Where(q=>q.ClientStatus==ClientStatus.InQueue).ToList();
        }
        public async Task<Result> AddToLine(QueueLine queueLine)
        {
            try
            {
                queueLine.CheckInTime = DateTime.Now;
                if (ClientInService() == null)
                {
                    queueLine.ClientStatus = ClientStatus.InService;
                }
                await _context.AddAsync(queueLine);
                await _context.SaveChangesAsync();
                return new Result() { Success=true, QueueLine=queueLine};
            }
            catch(Exception ex)
            {
                _logger.LogError("AddToLine error {0}", ex);
                return new Result() { Success = false, Error=ex.Message };
            }
        }

        public async Task<Result> NextClient(QueueLine queueLine)
        {
            try
            {
                if (queueLine.Id != 0)
                {
                    var queueLineDb = _context.QueueLines.FirstOrDefault(q => q.Id == queueLine.Id);
                    if (queueLineDb == null)
                    {
                        return new Result() { Success = false, Error = "Not found" }; ;
                    }
                    queueLineDb.ClientStatus = ClientStatus.Closed;
                    _context.Update(queueLineDb);
                }
                var nextClient = NextClientInService();
                if(nextClient!= null)
                {
                    nextClient.ClientStatus = ClientStatus.InService;
                    _context.Update(nextClient);
                }
                await _context.SaveChangesAsync();
                return new Result() { Success = true, QueueLine = nextClient };
            }
            catch (Exception ex)
            {
                _logger.LogError("NextClient error {0}", ex);
                return new Result() { Success = false, Error = ex.Message };
            }
        }
        private QueueLine NextClientInService()
        {
            return _context.QueueLines.Where(q => q.ClientStatus==ClientStatus.InQueue).OrderBy(q => q.Id).FirstOrDefault();
        }
        public QueueLine ClientInService()
        {
            return _context.QueueLines.Where(q => q.ClientStatus == ClientStatus.InService).OrderBy(q => q.Id).FirstOrDefault();
        }
    }
}
