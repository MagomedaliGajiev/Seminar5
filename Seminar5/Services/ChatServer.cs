using Seminar5.DTO;
using Seminar5.Models;
using System.Net;
using System.Net.Sockets;


namespace Seminar5.Services
{
    public class ChatServer
    {
        TcpListener? _listener;

        public ChatServer(IPEndPoint? endPoint)
        {
            if (endPoint != null)
            {
                _listener = new TcpListener(endPoint);
            }
        }

        public void Run(object? state, bool timeOut)
        {
            TcpClient? tcpClient = _listener?.AcceptTcpClient();


        }

        public async Task ProcessClient(TcpClient client)
        {
            using var reader = new StreamReader(client.GetStream());
            var jsonData = await reader.ReadToEndAsync();

            var message = TCPMessage.JsonToMessage(jsonData);

            switch (message.Status)
            {
                case Command.Registered:
                    RegisterClient(message.SenderName);
                    break;
                case Command.Confirmed:
                    Confirmed(message.Id);
                    break;
                case Command.Messaged:
                    Messaged(message);
                    break;
            }
        }

        private void RegisterClient(string name)
        {
            using var context = new ChatContext();
            context.Users.Add(new User { Name = name });
            context.SaveChanges();
        }

        private void Confirmed(int? id)
        {
            using var context = new ChatContext();
            var message = context.Messages.FirstOrDefault(m => m.Id == id);
            if (message != null)
            {
                message.isRecieved = true;
            }
            context.SaveChanges();
        }

        public void Messaged(TCPMessage message)
        {
            using var context = new ChatContext();
            var sender = context.Users.FirstOrDefault(u => u.Name == message.SenderName);
            var consumer = context.Users.FirstOrDefault(u => u.Name == message.ConsumerName);

            if (sender != null && consumer != null)
            {
                var newMessage = new Message
                {
                    AutorId = sender.Id,
                    ConsumerId = consumer.Id,
                    Content = message.Text,
                    isRecieved = false
                };

                context.Messages.Add(newMessage);
                context.SaveChanges();

                Console.WriteLine($"Message from {message.SenderName} to {message.ConsumerName} saved successfully.");
            }
            else
            {
                Console.WriteLine("Sender or consumer not found. Message not saved.");
            }
        }
    }

}
