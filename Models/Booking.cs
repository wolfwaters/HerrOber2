using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerrOber2.Models
{
    public class Booking
    {
        public string ActorEmail { get; set; }

        public double Amount { get; set; }

        public BookingType BookingType { get; set; }

        // Restaurant name or email of user
        public string Recipient { get; set; }

        public DateTime BookingDate { get; set; }
    }

    public enum BookingType
    {
        Restaurant,
        InternalTransfer,       
    }
}
