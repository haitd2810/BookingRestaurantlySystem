using booking.Models;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
namespace booking.Services
{
    public class UserManage : IUserManage
    {
        private readonly bookingDBContext context = new bookingDBContext();
        public string messageConfirm(string name, string email, string phone, string date, string time, string tableNumber, string msg)
        {
            string body = "<!DOCTYPE html>" +
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
                "<h1>Restaurantly</h1>\r\n" +
                "</div>\r\n" +
                "<div class=\"content\">\r\n" +
                "<h2>Table Booking Confirmation</h2>\r\n" +
                "<p>Dear "+ name +",</p>\r\n" +
                "<p>Thank you for choosing our restaurant. Your table booking has been successfully confirmed.</p>\r\n" +
                "<p><strong>Booking Details:</strong></p>\r\n" +
                "<ul>\r\n" +
                "<li><strong>Booking Number:</strong> "+ phone +"</li>\r\n" +
                "<li><strong>Date:</strong> " + date +"</li>\r\n" +
                "<li><strong>Time:</strong> "+ time +"</li>\r\n" +
                "<li><strong>Table Number:</strong> "+ tableNumber +"</li>\r\n" +
                "<li><strong>Your Message:</strong> " + msg + "</li>\r\n" +
                "</ul>\r\n" +
                "<p>We look forward to serving you and making your dining experience memorable. If you have any questions or need to make any changes to your booking, please do not hesitate to contact us.</p>\r\n" +
                "<p>Best regards,</p>\r\n" +
                "<p>Restaurantly Hari</p>\r\n" +
                "<a href=\"https://localhost:7148/\">Visit Our Website</a>\r\n" +
                "</div>\r\n" +
                "<div class=\"footer\">\r\n" +
                "<p>&copy; 2024 [Restaurant Name]. All rights reserved.</p>\r\n" +
                "<p>[Restaurant Address] | [Restaurant Phone Number] | [Restaurant Email]</p>\r\n" +
                "</div>\r\n" +
                "</div>\r\n" +
                "</body>\r\n" +
                "</html>";

            return body;
        }

        public string messageFeedback(string name)
        {
            string body = "<!DOCTYPE html>" +
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
                "<h1>Restaurantly</h1>\r\n" +
                "</div>\r\n" +
                "<div class=\"content\">\r\n" +
                "<h2>Thank you</h2>\r\n" +
                "<p>Dear " + name + ",</p>\r\n" +
                "<p>Thanks for your feedback. We will check your feedback and reply to you as soon as possible.</p>\r\n" +
                "<p><strong>Once again, Thank you very much.</strong></p>\r\n" +
                "<p>Have a nice day.</p>\r\n" +
                "<p>Best regards,</p>\r\n" +
                "<p>Restaurantly Hari</p>\r\n" +
                "<a href=\"https://localhost:7148/\">Visit Our Website</a>\r\n" +
                "</div>\r\n" +
                "<div class=\"footer\">\r\n" +
                "<p>&copy; 2024 [Restaurant Name]. All rights reserved.</p>\r\n" +
                "<p>[Restaurant Address] | [Restaurant Phone Number] | [Restaurant Email]</p>\r\n" +
                "</div>\r\n" +
                "</div>\r\n" +
                "</body>\r\n" +
                "</html>";

            return body;
        }

        public void sendMailConfirm(string from, string password, string to,string body, string subject)
        {
            string fromMail = from;
            string fromPass = password;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(to));
            message.Body = body;
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
