
using System.Xml.Serialization;

namespace HerrOber2.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string displayName, string emailAddress)
        {
            DisplayName = displayName;
            EmailAddress = emailAddress;
        }

        public string DisplayName { get; set; }

        public string EmailAddress { get; set; }

        public string ImageUrl { get; set; }

        [XmlIgnoreAttribute]
        public double Balance { get; private set; }

        internal double CalculateBalance()
        {
            Balance = 0.0;
            foreach(Order order in DataModel.Instance.Orders)
            {
                if(order.UserEmail.Equals(EmailAddress))
                {
                    if (order.OrderStatus == OrderStatus.Delivered)
                    {
                        Balance += (order.Price + order.ExtrasPrice);
                    }
                }
            }
            // TODO: Add bookings table to Balance ....

            return Balance;
        }
    }
}
