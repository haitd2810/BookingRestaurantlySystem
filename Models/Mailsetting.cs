using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Mailsetting
    {
        public int Id { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }
        public byte[]? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        private static readonly object instaceLock = new object();
        private static Mailsetting instance = null;

        public static Mailsetting Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new Mailsetting();
                    return instance;
                }
            }
        }

        public Mailsetting getMailSetting()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Mailsettings.FirstOrDefault() ?? (new Mailsetting());
        }
    }
}
