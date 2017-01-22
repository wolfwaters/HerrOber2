using System;

namespace HerrOber2.Models
{
    public class Booking
    {
        public string ActorId { get; set; }

        public double Amount { get; set; }

        public BookingType BookingType { get; set; }

        // Restaurant name or id of user
        public string Recipient { get; set; }

        public DateTime BookingDate { get; set; }
    }

    public enum BookingType
    {
        Restaurant,
        InternalTransfer,       
    }
}
