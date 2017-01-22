using System.Xml.Serialization;

namespace HerrOber2.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string email)
        {
            Id = DataModel.NewGuid();
            Name = name;
            Email = email;
            ImageUrl = name + ".jpg";
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        [XmlIgnoreAttribute]
        public double Balance { get; private set; }

        internal double CalculateBalance()
        {
            Balance = 0.0;

            // Subtract ordered items values
            foreach(Order order in DataModel.Instance.Orders)
            {
                if(order.UserId.Equals(Id))
                {
                    if (order.OrderStatus == OrderStatus.Delivered)
                    {
                        Balance -= order.Price;
                    }
                }
            }

            // Add bookings table to Balance ....
            foreach(Booking booking in DataModel.Instance.Bookings)
            {
                if (booking.ActorId.Equals(Id))
                {
                    Balance += booking.Amount;
                }
                else if(booking.BookingType == BookingType.InternalTransfer
                    && booking.Recipient.Equals(Id))
                {
                    Balance -= booking.Amount;
                }
            }

            return Balance;
        }
    }
}
