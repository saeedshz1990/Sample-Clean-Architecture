namespace Sample_Clean_Architecture.Web.Models
{
    public class MessageViewModel
    {
        public string Message { get; set; }
        public string Color { get; set; }
        public MessageViewModel()
        {
            Message = string.Empty;
            Color = "black";
        }
    }
}
