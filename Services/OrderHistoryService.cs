using booking.DAO;
using booking.IServices;
using booking.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySqlX.XDevAPI.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace booking.Services
{
    public class Total_Statistics
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public float total { get; set; }
    }
    public class OrderHistoryService : IOrderHistoryService
    {
        public bool DeleteByZeroMeal(List<Ordertable> order_list, Ordertable order, int odHistory, int tableID)
        => OrderHistoryDAO.Instance.DeleteByZeroMeal(order_list, order, odHistory, tableID);

        public int GetIDOrderHistory(int ExistID, int newID) => OrderHistoryDAO.Instance.GetIDOrderHistory(ExistID, newID);

        public List<Total_Statistics> GetTotalStatistic(List<Orderhistory> order_his_list, DateTime? start, DateTime? end)
        => OrderHistoryDAO.Instance.GetTotalStatistic(order_his_list, start, end);

        public List<Total_Statistics> GetCompletebyDate(List<Total_Statistics> list, DateTime? start, DateTime? end)
        => OrderHistoryDAO.Instance.GetCompletebyDate(list, start, end);

        public Orderhistory SetDefault() => OrderHistoryDAO.Instance.SetDefault();

        public Orderhistory UpdateByPayMeal(Orderhistory order_history, float total)
        => OrderHistoryDAO.Instance.UpdateByPayMeal(order_history, total);
        public List<Orderhistory> GetListByDate(List<Orderhistory> order_his_list, DateTime? start, DateTime? end)
        => OrderHistoryDAO.Instance.GetListByDate(order_his_list, start, end);

        public Orderhistory FindbyID(int id) => OrderHistoryDAO.Instance.FindbyID(id);
        public float GetTotal(Orderhistory order_history) => OrderHistoryDAO.Instance.GetTotal(order_history);

        public bool UpdateOrderHistory(Orderhistory orderH) => OrderHistoryDAO.Instance.UpdateOrderHistory(orderH);

        public List<Orderhistory> GetAll() => OrderHistoryDAO.Instance.GetAll();
    }
}
