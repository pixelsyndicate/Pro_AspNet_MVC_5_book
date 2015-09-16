using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asyncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTest();

            RunTest();

            Console.ReadKey();
        }

        private static void RunTest()
        {
            Console.WriteLine("Testing stuff with Task<>");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var oldWayResult = GetApressPageLengthOldWay();
            stopWatch.Stop();
            Console.WriteLine(oldWayResult);

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine(elapsedTime, "RunTime");
            Console.WriteLine("---");

            Console.WriteLine("Testing stuff with async / await");
            stopWatch = new Stopwatch();
            stopWatch.Start();
            var newWayResult = GetApressPageLengthNewWay();
            stopWatch.Stop();
            Console.WriteLine(newWayResult);

            // Get the elapsed time as a TimeSpan value.
            ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine(elapsedTime, "RunTime");
            Console.WriteLine("---");
        }

        public static string GetApressPageLengthOldWay()
        {
            Task<long?> l = MyAsyncMethods.GetPageLengthOldWay();

            // returns "The length of apress.com's Result.Content.Headers.ContentLength is 120287"
            return string.Format("The length of apress.com's Result.Content.Headers.ContentLength is {0}", l.Result);
        }

        public static string GetApressPageLengthNewWay()
        {
            Task<long?> l = MyAsyncMethods.GetPageLengthNewWay();

            // returns "The length of apress.com's Result.Content.Headers.ContentLength is 120287"
            return string.Format("The length of apress.com's Result.Content.Headers.ContentLength is {0}", l.Result);
        }


    }


}