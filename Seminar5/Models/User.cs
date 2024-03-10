
namespace Seminar5.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Message> SendedMessage { get; set; }
        public virtual ICollection<Message> RecievedMessage { get; set; }

    }
}
