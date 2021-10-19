using FirstFiveQueue.Models;
using FirstFiveQueue.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFiveQueue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueLineController : Controller
    {
        private readonly ILogger<QueueLineController> _logger;
        private readonly IQueueLineRepository _queueLineRepository;

        public QueueLineController(ILogger<QueueLineController> logger, IQueueLineRepository queueLineRepository)
        {
            _logger = logger;
            _queueLineRepository = queueLineRepository;
        }
        [HttpGet]
        [Route("clientline")]
        public JsonResult ClientLine()
        {
            var query = _queueLineRepository.ClientLine();
            return Json(query.ToList());

        }
        [HttpGet]
        [Route("inservice")]
        public JsonResult InService()
        {
            var query = _queueLineRepository.ClientInService();
            return Json(query);

        }
        [HttpPost]
        [Route("addclientline")]
        public async Task<JsonResult> AddClientLine(QueueLine queueLine)
        {
            var res = await _queueLineRepository.AddToLine(queueLine);
            return Json(res);

        }
        [HttpPost]
        [Route("nextClient")]
        public async Task<JsonResult> NextClient(QueueLine queueLine)
        {
            var res = await _queueLineRepository.NextClient(queueLine);
            return Json(res);

        }
    }
}
