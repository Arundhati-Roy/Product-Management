using System;
using System.Data;

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

            t.Rows.Add("1","U1", "5","Chai");
            t.Rows.Add("2", "U2","4","Coffee");
            t.Rows.Add("3", "U3","6","Latte");
            DisplayEmp(t);
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
    }
}
