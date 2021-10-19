using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstFiveQueue.Models
{
    [Flags]
    public enum ClientStatus
    {
        [Display(Name = "InQueue")]
        InQueue = 0,
        [Display(Name = "InService")]
        InService = 1,
        [Display(Name = "Closed")]
        Closed = 2
    }
    public class QueueLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public ClientStatus ClientStatus { get; set; }
       // public DateTime CheckInTime { get; set; }
        private DateTime _checkintime;
        public DateTime CheckInTime
        {
            get => _checkintime;
            set {
                DateFormat = value.ToString("HH:mm");
                _checkintime = value;
            }
        }
        [NotMapped]
        public string DateFormat { get; set; }
    }
}
