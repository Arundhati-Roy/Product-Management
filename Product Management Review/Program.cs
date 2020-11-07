using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
            //GetCountForPID(t);
            //DisplayAllEmp(t);
            //GetAvgForPId(t);
            //GetCountForPIDUsingLambda(t);
            //tpl();
            //DisplayEmpWithTPL(t);

            //string[] words = CreateWordArray(@"https://en.wikipedia.org/wiki/Blog");

            #region ParallelTasks
            Parallel.Invoke(
                () =>
            {
                Console.WriteLine("Begin first task...");
                DisplayEmpWithTPL(t);
            },
            () =>
            {
                Console.WriteLine("Begin second task...");
                GetCountForPID(t);
            }, //Close second action
            () =>
            {
                Console.WriteLine("Begin 3rd task...");
                GetAvgForPId(t);
            }//close third action
            );//close parallel.invoke
            #endregion

        }
        public static string[] CreateWordArray(string url)
        {
            Console.WriteLine($"Retrieving from {url}");

            //Dowload a web page easily
            string blog = new WebClient().DownloadString(url);

            //Separate string into an array, removing some punctuation
            return blog.Split(
                new char[] { ' ', '\u000A', ',', ';', ':', '.', '-', '_', '/' },
                StringSplitOptions.RemoveEmptyEntries);
        }
        public static void tpl()
        {
            Console.WriteLine("Using C# for loop");
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(" i= {0},thread= {1}",
                    i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10);
            }
            Console.WriteLine("Using Parallel for\n");
            Parallel.For(0, 10, i =>
              {
                  Console.WriteLine(" i= {0},thread= {1}",
                      i, Thread.CurrentThread.ManagedThreadId);
                  Thread.Sleep(10);
              });
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
            Console.WriteLine("Get count");
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
        /*        public static void GetCountForPIDUsingLambda(DataTable emp)
                {
                    var empName = emp.AsEnumerable().GroupBy(g=> emp.Columns["ProdId"]).Select(x=>new
                                   {
                                       ProdId = x.Key,
                                       Count = x.Count()
                                   });
                    foreach (var n in empName)
                    {
                        Console.WriteLine(n);
                    }
                }
        */
        public static void SkipTop(DataTable emp)
        {
            var empName = (from employee in emp.AsEnumerable()
                           orderby employee.Field<string>("Rating") descending
                           select employee.Field<string>("isLike")).Skip(3);
            foreach (string n in empName)
            {
                Console.WriteLine(n);
            }
        }
        public static void DisplayAllEmp(DataTable emp)
        {
            var empName = from employee in emp.AsEnumerable()
                          select employee;
            foreach (var n in empName)
            {
                Console.WriteLine((string)n[0] + " " + n[1] + " " + n[2] + " " + n[3]);
            }
        }
        public static void GetAvgForPId(DataTable emp)
        {
            Console.WriteLine("Get average rating");
            var empName = (from employee in emp.AsEnumerable()
                           group employee by employee.Field<string>("ProdId") into g
                           select new
                           {
                               ProdId = g.Key,
                               AvgRating = (from l in g.AsEnumerable()
                                            select Convert.ToInt32(l.Field<string>("Rating"))).Average()
                           });
            foreach (var n in empName)
            {
                Console.WriteLine(n);
            }
        }
        public static void DisplayEmpWithTPL(DataTable emp)
        {
            Console.WriteLine("Display prod with tpl");
            var empName = from employee in emp.AsEnumerable()
                          select employee.Field<string>("ProdId");
            /*foreach (string i in empName)
            {
                Console.WriteLine(" i= {0},thread= {1}",
                                    i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10);
            }
            Console.WriteLine("Using Parallel for\n");*/
            Parallel.ForEach(empName, i =>
            {
                Console.WriteLine(" i= {0},thread= {1}",
                    i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10);
            });
        }

    }
}

    

