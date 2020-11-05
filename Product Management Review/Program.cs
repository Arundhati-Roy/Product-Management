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
            t.Columns.Add("ID");
            t.Columns.Add("PName");

            t.Rows.Add("1", "Chai");
            t.Rows.Add("2", "Coffee");
            t.Rows.Add("3", "Latte");
            DisplayEmp(t);
        }
        public static void DisplayEmp(DataTable emp)
        {
            var empName = from employee in emp.AsEnumerable()
                          select employee.Field<string>("PName");
            foreach (string n in empName)
            {
                Console.WriteLine(n);
            }
        }
    }
}
