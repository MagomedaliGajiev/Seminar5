
namespace Seminar5.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int? AutorId { get; set; }
        public int? ConsumerId { get; set; }
        public string Content { get; set; }
        public bool isRecieved { get; set; }
        public virtual User? Author { get; set; }
        public virtual User? Consumer { get; set; }

    }
}
