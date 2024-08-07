using booking.IServices;
using booking.Models;
using System.Reflection.Metadata.Ecma335;

namespace booking.Services
{
    public class Total_Statistics
    {
        public int id { get; set; }
        public string date { get; set; }
        public float total { get; set; }
    }
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly IOrderService order_service = new OrderService();
        private readonly Orderhistory object_odhistory = new Orderhistory();
        private readonly Table table_object = new Table();

        public bool deleteByZeroMeal(List<Ordertable> order_list, Ordertable order, int odHistory, int tableID)
        {

            if (order_service.checkOrderMeal(order_list))
            {
                order.DeleteOrderTable();
                object_odhistory.deleteByID(odHistory);

                //update status table
                table_object.markTableAsOrdered(tableID, false);
                return true;
            }
            else { order.DeleteOrderTable(); return true; }
        }

        public int getIDOrderHistory(int ExistID, int newID)
        {
            if (newID == -1 && ExistID == -1)
            {
                Orderhistory od_history = setDefault();
                od_history.AddOderHistory();
                return od_history.Id;
            }
            else if (newID == -1 && ExistID != -1) return ExistID;
            else return newID;
        }

        public List<Total_Statistics> getTotalStatistic(List<Orderhistory> order_his_list)
        {
            int count = 0;
            List<Total_Statistics> total_List = new List<Total_Statistics> ();
            foreach(Orderhistory ord in order_his_list)
            {
                if(returnIndexDuplicate(total_List,ord.getDate()) != -1)
                {
                    int index = returnIndexDuplicate(total_List, ord.getDate());
                    float price = float.Parse(ord.TotalPrice.ToString() ?? "0");
                    total_List[index].total += price;
                }
                else
                {
                    count++;
                    int id = count;
                    string date = ord.getDate();
                    float total = float.Parse(ord.TotalPrice.ToString());
                    Total_Statistics new_total = new Total_Statistics() 
                    { 
                        id = id,
                        date = date,
                        total = total,
                    };
                    total_List.Add(new_total);
                }
            }
            return total_List;
        }
        
        private int returnIndexDuplicate(List<Total_Statistics> list, string keyword)
        {
            for(int i=0; i < list.Count; i++)
            {
                if (list[i].date.Equals(keyword))
                {
                    return i;
                }
            }
            return -1;
        }

        public Orderhistory setDefault()
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

        public Orderhistory updateByPayMeal(Orderhistory order_history, float total)
        {
            order_history.TotalPrice = total;
            order_history.Payed = new byte[] { 1 };
            order_history.UpdateDate = DateTime.Now;
            return order_history;
        }
    }
}
