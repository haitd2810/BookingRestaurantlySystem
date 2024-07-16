namespace booking.Services
{
    public interface IUserManage
    {
        string messageConfirm(string name, string email, string phone, string date, string time, string tableNumber, string message);
        public void sendMailConfirm(string from, string password, string to, string body, string subject);
        string messageFeedback(string name);
        
    }
}
