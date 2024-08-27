using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace booking.DAO
{
    public class OrderHistoryDAO
    {
        private readonly bookingDBContext context = new bookingDBContext();
        private readonly IOrderService order_service = new OrderService();
        private static readonly object instaceLock = new object();
        private static OrderHistoryDAO instance = null;

        public static OrderHistoryDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new OrderHistoryDAO();
                    return instance;
                }
            }
        }

        public Boolean AddOderHistory(Orderhistory oderH)
        {
            context.Orderhistories.Add(oderH);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public Boolean UpdateOrderHistory(Orderhistory orderH)
        {
            context.Orderhistories.Update(orderH);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public Orderhistory FindbyID(int id)
        {
            return context.Orderhistories.Where(oh => oh.Id == id)
                          .Include(oh => oh.Ordertables)
                          .FirstOrDefault() ?? new Orderhistory();
        }

        public Boolean DeleteByID(int id)
        {
            Orderhistory od_history = FindbyID(id);
            if (od_history != null)
            {
                context.Remove(od_history);
                context.SaveChanges();
            }
            return true;
        }

        public float GetTotal(Orderhistory order_history)
        {
            if (order_history != null)
            {
                float total_price = 0;
                foreach (var od in order_history.Ordertables)
                {
                    total_price += float.Parse(s: od.Price.ToString() ?? "0");
                }
                return total_price;
            }
            return 0;
        }

        public List<Orderhistory> GetAll()
        {
            return context.Orderhistories
                          .Where(od => od.Status[0] == 1 && od.Payed[0] == 1)
                          .Include(od => od.Ordertables)
                          .ToList();
        }
        
        public bool DeleteByZeroMeal(List<Ordertable> order_list, Ordertable order, int odHistory, int tableID)
        {
            ITableService table_service = new TableService();
            if (order_service.checkOrderMeal(order_list))
            {
                order_service.DeleteOrderTable(order);
                DeleteByID(odHistory);

                //update status table
                table_service.MarkTableAsOrdered(tableID, false);
                return true;
            }
            else { order_service.DeleteOrderTable(order); return true; }
        }

        public int GetIDOrderHistory(int ExistID, int newID)
        {
            if (newID == -1 && ExistID == -1)
            {
                Orderhistory od_history = SetDefault();
                AddOderHistory(od_history);
                return od_history.Id;
            }
            else if (newID == -1 && ExistID != -1) return ExistID;
            else return newID;
        }

        public List<Total_Statistics> GetTotalStatistic(List<Orderhistory> order_his_list, DateTime? start, DateTime? end)
        {
            int count = 0;
            List<Total_Statistics> total_List = new List<Total_Statistics>();
            if (order_his_list == null || order_his_list.Count == 0) GetCompletebyDate(total_List, start, end);
            foreach (Orderhistory ord in order_his_list)
            {
                if (ReturnIndexDuplicate(total_List, DateTime.ParseExact(ord.getDate(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) != -1)
                {
                    int index = ReturnIndexDuplicate(total_List, DateTime.ParseExact(ord.getDate(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    float price = float.Parse(ord.TotalPrice.ToString() ?? "0");
                    total_List[index].total += price;
                }
                else
                {
                    count++;
                    int id = count;
                    string date = ord.getDate();
                    float total = float.Parse(ord.TotalPrice.ToString() ?? "0");
                    Total_Statistics new_total = new Total_Statistics()
                    {
                        id = id,
                        date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        total = total,
                    };
                    total_List.Add(new_total);
                }
            }
            return GetCompletebyDate(total_List, start, end);
        }

        public List<Total_Statistics> GetCompletebyDate(List<Total_Statistics> list, DateTime? start, DateTime? end)
        {
            List<Total_Statistics> result = new List<Total_Statistics>();
            if (list == null || list.Count == 0)
            {
                AddDate(DateTime.Parse(start.ToString()), DateTime.Parse(end.ToString()).AddDays(1), result);
                return result;
            }
            list.Sort((x, y) => x.date.CompareTo(y.date));
            result.Add(list[0]);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].date != result[result.Count - 1].date)
                {
                    if (list[i].date.CompareTo(result[result.Count - 1].date) == 1)
                    {
                        AddDate(result[result.Count - 1].date.AddDays(1), list[i].date, result);
                        result.Add(list[i]);
                    }
                    else
                    {
                        result.Add(list[i]);
                        AddDate(list[i].date.AddDays(1), result[result.Count - 1].date, result);
                    }
                }
            }
            AddMissDate(result, start, end);
            return result;
        }

        private static void AddMissDate(List<Total_Statistics> list, DateTime? start, DateTime? end)
        {
            if (list == null || list.Count == 0) return;
            list.Sort((x, y) => x.date.CompareTo(y.date));
            if (list[0].date.CompareTo(start) == 1)
                AddDate(DateTime.Parse(start.ToString()), list[list.Count - 1].date.AddDays(1), list);
            if (list[list.Count - 1].date.CompareTo(end) == -1)
                AddDate(list[list.Count - 1].date.AddDays(1), DateTime.Parse(end.ToString()), list);
            list.Sort((x, y) => x.date.CompareTo(y.date));
        }

        private static List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
                allDates.Add(date);

            return allDates;
        }

        private static void AddDate(DateTime startDate, DateTime endDate, List<Total_Statistics> list)
        {
            List<DateTime> date = GetDatesBetween(startDate, endDate);
            foreach (DateTime d in date)
            {
                list.Add(new Total_Statistics()
                {
                    id = 0,
                    date = d,
                    total = 0,
                });
            }
        }

        private static int ReturnIndexDuplicate(List<Total_Statistics> list, DateTime keyword)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].date.Equals(keyword))
                {
                    return i;
                }
            }
            return -1;
        }

        public Orderhistory SetDefault()
        {
            return new Orderhistory()
            {
                Fullname = null,
                Email = null,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                TotalPrice = 0,
                Payed = new byte[] { 0 },
                Status = new byte[] { 1 }
            };
        }

        public Orderhistory UpdateByPayMeal(Orderhistory order_history, float total)
        {
            order_history.TotalPrice = total;
            order_history.Payed = new byte[] { 1 };
            order_history.UpdateDate = DateTime.Now;
            return order_history;
        }

        public List<Orderhistory> GetListByDate(List<Orderhistory> order_his_list, DateTime? start, DateTime? end)
        {
            List<Orderhistory> result = new List<Orderhistory>();
            foreach (Orderhistory od in order_his_list)
            {
                if (od.UpdateDate >= start && od.UpdateDate <= end)
                {
                    result.Add(od);
                }
            }
            return result;
        }
    }
}
