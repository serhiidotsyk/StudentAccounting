namespace BLL.Models.Email
{
    public class EmailModel
    {
        public string SenderAdress { get; set; }
        public string ReceiverAdress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
