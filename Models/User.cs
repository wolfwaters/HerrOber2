
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
    }
}
