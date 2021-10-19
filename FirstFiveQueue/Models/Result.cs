﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFiveQueue.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public QueueLine QueueLine { get; set; }
    }
}
