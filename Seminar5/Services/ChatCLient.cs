
using Seminar5.Models;

namespace Seminar5.Services
{
    public class ChatClient
    {
        public void ListUnreadMessages(string userName)
        {
            using var context = new ChatContext();
            var user = context.Users.FirstOrDefault(u => u.Name == userName);

            if (user != null)
            {
                var unreadMessages = context.Messages.Where(m => m.ConsumerId == user.Id && !m.isRecieved).ToList();

                if (unreadMessages.Any())
                {
                    Console.WriteLine("Unread Messages:");
                    foreach (var message in unreadMessages)
                    {
                        Console.WriteLine($"From: {message.Author?.Name}, Content: {message.Content}");
                    }
                }
                else
                {
                    Console.WriteLine("No unread messages.");
                }
            }
            else
            {
                Console.WriteLine($"User '{userName}' not found.");
            }
        }
    }
}