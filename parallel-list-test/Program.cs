using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace parallel_list_test
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessSomeStuff();

            GC.Collect();

            Console.ReadKey();
        }

        private static void ProcessSomeStuff()
        {
            var list = new List<string>();
            var list2 = new ConcurrentBag<string>();
            var list3 = new ConcurrentBag<string>();

            int count = 1000000;
            Console.WriteLine($"Started");

            for (int i = 0; i < count; i++)
            {
                list.Add(@"I like tacos 123123405)##)@#$@#%*(%9I like tacos 123123405)##)@#$@#%*(%9I like tacos 123123405)##)@#$@#%*(%9I like tacos 123123405)##)@#$@#%*(%9I like tacos 123123405)##)@#$@#%*(%9I like tacos 123123405)##)@#$@#%*(%9I like tacos 123123405)##)@#$@#%*(%9");
            }

            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();
            sw1.Start();

            foreach (var s in list)
            {
                list2.Add(ProcessString(s));
            }

            sw1.Stop();

            Console.WriteLine($"Elapsed Single: {sw1.Elapsed}");

            sw2.Start();

            Parallel.ForEach(list,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            }, currentItem =>
            {
                list3.Add(ProcessString(currentItem));
            });

            sw2.Stop();
            Console.WriteLine($"Elapsed Multip: {sw2.Elapsed}");

            Console.WriteLine($"List1 count: {list.Count}");
            Console.WriteLine($"List2 count: {list2.Count}");
            Console.WriteLine($"List3 count: {list3.Count}");

        }

        private static string ProcessString(string input)
        {
            char[] arrayChars = input.ToCharArray();

            List<byte> bytes = new List<byte>();

            var charBytes = Encoding.ASCII.GetBytes(arrayChars);

            foreach(var b in charBytes)
            {
                bytes.Add(b);
            }

            return input;
        }
    }
}
