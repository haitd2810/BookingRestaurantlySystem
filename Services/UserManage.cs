using booking.Models;
using System.Net;
using System.Net.Mail;
namespace booking.Services
{
    public class UserManage : IUserManage
    {
        private readonly bookingDBContext context = new bookingDBContext();
        public bool isValidStaff(string username, string password)
        {
            Staff staff = context.Staff.Where(staff => staff.Username == username && staff.Password == password).FirstOrDefault();
            return staff != null;
        }

        public void sendMailConfirm(string name, string email, string phone, string date, string time, string tableNumber, string msg)
        {
            string fromMail = "haitd28102003@gmail.com";
            string fromPass = "ufgnjoyhaobumcwm";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Confirmation for booking";
            message.To.Add(new MailAddress(email));
            message.Body = "" +
                "<!DOCTYPE html>" +
                "\r\n<html>\r\n" +
                "<head>\r\n" +
                "<style>\r\n" +
                "body {\r\n" +
                "font-family: Arial, sans-serif;\r\n" +
                "margin: 0;\r\n" +
                "padding: 0;\r\n" +
                "background-color: #f6f6f6;\r\n" +
                "}\r\n" +
                ".container {\r\n" +
                "width: 100%;\r\n" +
                "padding: 20px;\r\n" +
                "background-color: #ffffff;\r\n" +
                "box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n" +
                "margin: 20px auto;\r\n" +
                "max-width: 600px;\r\n" +
                "}\r\n" +
                ".header {\r\n" +
                "background-color: #333333;\r\n" +
                "color: #ffffff;\r\n" +
                "padding: 10px 20px;\r\n" +
                "text-align: center;\r\n" +
                "}\r\n" +
                ".content {\r\n" +
                "padding: 20px;\r\n" +
                "}\r\n" +
                ".content h2 {\r\n" +
                "color: #333333;\r\n" +
                "}\r\n" +
                ".content p {\r\n" +
                "line-height: 1.6;\r\n" +
                "color: #666666;\r\n" +
                "}\r\n" +
                ".content a {\r\n" +
                "display: inline-block;\r\n" +
                "padding: 10px 20px;\r\n" +
                "color: #ffffff;\r\n" +
                "background-color: #007bff;\r\n" +
                "text-decoration: none;\r\n" +
                "border-radius: 5px;\r\n" +
                "}\r\n" +
                ".footer {\r\n" +
                "background-color: #f1f1f1;\r\n" +
                "padding: 10px 20px;\r\n" +
                "text-align: center;\r\n" +
                "font-size: 12px;\r\n" +
                "color: #666666;\r\n" +
                "}\r\n" +
                "</style>\r\n" +
                "</head>\r\n" +
                "<body>\r\n" +
                "<div class=\"container\">\r\n" +
                "<div class=\"header\">\r\n" +
                "<h1>Restaurant Name</h1>\r\n" +
                "</div>\r\n" +
                "<div class=\"content\">\r\n" +
                "<h2>Table Booking Confirmation</h2>\r\n" +
                "<p>Dear [Customer Name],</p>\r\n" +
                "<p>Thank you for choosing our restaurant. Your table booking has been successfully confirmed.</p>\r\n" +
                "<p><strong>Booking Details:</strong></p>\r\n" +
                "<ul>\r\n" +
                "<li><strong>Booking Number:</strong> [Booking Number]</li>\r\n" +
                "<li><strong>Date:</strong> [Date]</li>\r\n" +
                "<li><strong>Time:</strong> [Time]</li>\r\n" +
                "<li><strong>Number of Guests:</strong> [Number of Guests]</li>\r\n" +
                "</ul>\r\n" +
                "<p>We look forward to serving you and making your dining experience memorable. If you have any questions or need to make any changes to your booking, please do not hesitate to contact us.</p>\r\n" +
                "<p>Best regards,</p>\r\n" +
                "<p>[Restaurant Name]</p>\r\n" +
                "<a href=\"[Restaurant Website URL]\">Visit Our Website</a>\r\n" +
                "</div>\r\n" +
                "<div class=\"footer\">\r\n" +
                "<p>&copy; 2024 [Restaurant Name]. All rights reserved.</p>\r\n" +
                "<p>[Restaurant Address] | [Restaurant Phone Number] | [Restaurant Email]</p>\r\n" +
                "</div>\r\n" +
                "</div>\r\n" +
                "</body>\r\n" +
                "</html>";
            message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPass),
                EnableSsl = true
            };
            smtpClient.Send(message);
        }
    }
}
