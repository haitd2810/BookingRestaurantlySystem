namespace booking.Services
{
    public interface IUserManage
    {
        public Boolean isValidStaff(string username, string password);

        public void sendMailConfirm(string name, string email, string phone, string date, string time, string tableNumber, string message);
    }
}
