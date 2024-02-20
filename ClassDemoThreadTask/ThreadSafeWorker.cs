using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoThreadTask
{
    public class ThreadSafeWorker
    {
        // Constant 
        private const bool IsParallel = false;

        // Shared resource
        private List<int> _primes;

        public ThreadSafeWorker()
        {
            _primes = new List<int>();
        }


        public void Start()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();


            for (int i = 0; i < 5; i++)
            {
                FindPrimes(1000000, IsParallel);

            }

            sw.Stop();
            Console.WriteLine($"Time is " + sw.Elapsed);
        }



        private bool IsPrime(int number)
        {
            if (number < 4) { return true; }
            int limit = Convert.ToInt32(Math.Sqrt(number));
            bool isPrime = true;
            for (int i = 2; i <= limit && isPrime; i++)
            {
                isPrime = number % i != 0;
            }
            return isPrime;
        }

        private void FindPrimes(int upper, bool parallel)
        {
            _primes.Clear();
            if (parallel)
            {
                int middle = upper / 2;
                Task t1 = Task.Run(() => FindPrimesInInterval(2, middle));
                Task t2 = Task.Run(() => FindPrimesInInterval(middle + 1, upper));
                t1.Wait();
                t2.Wait();
            }
            else
            {
                FindPrimesInInterval(2, upper);
            }

            string text = $"Found {_primes.Count} primes in [2; {upper}]";
            Console.WriteLine(text);
        }

        private void FindPrimesInInterval(int lower, int upper)
        {
            for (int i = lower; i < upper; i++)
            {
                if (IsPrime(i))
                {

                    // no threadsafe
                    _primes.Add(i);


                    

                }
            }
        }

    }
}
