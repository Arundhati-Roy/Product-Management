using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Product_Management_Review
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Linq Demo");
            DataTable t = new DataTable();
            t.Columns.Add("ProdID");
            t.Columns.Add("UserID");
            t.Columns.Add("Rating");
            //t.Columns.Add("Review");
            t.Columns.Add("isLike");

            t.Rows.Add("1", "U1", "5", "Chai");
            t.Rows.Add("2", "U2", "4", "Coffee");
            t.Rows.Add("3", "U3", "6", "Latte");
            t.Rows.Add("5", "U5", "5", "Tea");
            t.Rows.Add("6", "U6", "6", "Chai");
            t.Rows.Add("2", "U7", "3", "Cola");
            t.Rows.Add("4", "U4", "2", "Cola");
            t.Rows.Add("3", "U8", "2", "Tea");
            t.Rows.Add("4", "U9", "1", "Latte");
            t.Rows.Add("5", "U10", "8", "ColdCoffee");
            t.Rows.Add("2", "U11", "9", "Coffee");


            //DisplayEmp(t);
            //DisplayTop(t);
            //RetieveTopForGivenPID(t);
            GetCountForPID(t);
        }
        public static void DisplayEmp(DataTable emp)
        {
            var empName = from employee in emp.AsEnumerable()
                          select employee.Field<string>("isLike");
            foreach (string n in empName)
            {
                Console.WriteLine(n);
            }
        }
        public static void DisplayTop(DataTable emp)
        {
            var empName = (from employee in emp.AsEnumerable()
                           orderby employee.Field<string>("Rating") descending
                           select employee.Field<string>("isLike")).Take(3);
            foreach (string n in empName)
            {
                Console.WriteLine(n);
            }
        }
        public static void RetieveTopForGivenPID(DataTable emp)
        {
            List<string> l = new List<string> { "4", "1", "9" };
            var empName = from employee in emp.AsEnumerable()
                          where Convert.ToInt32(employee.Field<string>("Rating")) > 3
                          && employee.Field<string>("ProdID")
                          //in l
                          == "1"
                          orderby employee.Field<string>("Rating") descending
                          select employee.Field<string>("isLike");
            foreach (string n in empName)
            {
                Console.WriteLine(n);
            }
        }

        public static void GetCountForPID(DataTable emp)
        {
            var empName = (from employee in emp.AsEnumerable()
                           group employee by employee.Field<string>("ProdId") into g
                           select new
                           {
                               ProdId = g.Key,
                               Count = (from l in g select g.Key).Count()
                           });
            foreach (var n in empName)
            {
                Console.WriteLine(n);
            }
        }
    }
}

    

