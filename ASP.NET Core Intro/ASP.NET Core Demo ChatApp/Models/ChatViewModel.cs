namespace ASP.NET_Core_Demo_ChatApp.Models
{
    public class ChatViewModel
    {
        public MessageViewModel CurrentMessage { get; set; }
        public List<MessageViewModel> Messages { get; set; }
    }
}
