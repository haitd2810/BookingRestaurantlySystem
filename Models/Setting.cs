using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Setting
    {
        public int Id { get; set; }
        public string? Location { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public string? DayWork { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? LinkedIn { get; set; }
    }
}
