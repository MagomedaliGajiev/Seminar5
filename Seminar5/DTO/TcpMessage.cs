using System.Text.Json;

namespace Seminar5.DTO
{
    public class TCPMessage
    {
        public  int? Id {  get; set; }
        public string? SenderName {  get; set; }
        public string? ConsumerName { get; set; }
        public string Text { get; set; }
        public Command Status { get; set; }

        public static TCPMessage? JsonToMessage(string jsonData) => JsonSerializer.Deserialize<TCPMessage>(jsonData);

        public string MessageToJson() => JsonSerializer.Serialize(this);
    }

    public enum Command
    {
        Registered,
        Confirmed,
        Messaged

    }
}
