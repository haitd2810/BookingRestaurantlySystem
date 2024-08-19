﻿using booking.IServices;
using booking.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            foreach (Orderhistory ord in order_his_list)
            {
                if(returnIndexDuplicate(total_List,DateTime.Parse(ord.getDate())) != -1)
                {
                    int index = returnIndexDuplicate(total_List, DateTime.Parse(ord.getDate()));
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
                        date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        total = total,
                    };
                    total_List.Add(new_total);
                }
            }
            return getCompletebyDate(total_List);
        }

        public List<Total_Statistics> getCompletebyDate(List<Total_Statistics> list)
        {
            List<Total_Statistics> result = new List<Total_Statistics>();
            list.Sort((x,y) => x.date.CompareTo(y.date));
            result.Add(list[0]);
            for(int i=0; i < list.Count; i++)
            {
                if (list[i].date != result[result.Count - 1].date)
                {
                    if(list[i].date.CompareTo(result[result.Count - 1].date) == 1)
                    {
                        addDate(result[result.Count - 1].date.AddDays(1), list[i].date, result);
                        result.Add(list[i]);
                    }
                    else
                    {
                        result.Add(list[i]);
                        addDate(list[i].date.AddDays(1), result[result.Count - 1].date, result);
                    }
                }
            }
            return result;
        }

        private List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
                allDates.Add(date);
                
            return allDates;
        }
        private void addDate(DateTime startDate, DateTime endDate, List<Total_Statistics> list)
        {
            List < DateTime > date = GetDatesBetween(startDate,endDate);
            foreach (DateTime d in date)
                list.Add(new Total_Statistics()
                {
                    id = 0,
                    date = d,
                    total = 0,
                });
        }

        private int returnIndexDuplicate(List<Total_Statistics> list, DateTime keyword)
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

        public List<Orderhistory> getListByDate(List<Orderhistory> order_his_list, DateTime? start, DateTime? end)
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
