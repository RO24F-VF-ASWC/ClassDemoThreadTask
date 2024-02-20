using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoThreadTask
{
    public class ThreadWorker
    {
        /*
         * Delegate
         */

        protected delegate void BeregnMetodeType(int x, int y); 



        public void StartDelegate()
        {
            BeregnMetodeType beregnMetode = (k, l) => { Console.WriteLine($"sum er {k + l}"); };

            Console.WriteLine("sum alene");
            beregnMetode(7, 9);

            beregnMetode += gange;

            Console.WriteLine("Sum med gange");
            beregnMetode(100, 20);

            beregnMetode -= gange;

            Console.WriteLine("sum uden gange");
            beregnMetode(3, 5);

        }

        protected void gange(int h, int g)
        {
            Console.WriteLine($"gange er {h*g}");
        }







        /*
         * Threading
         */

        // what is example
        private static bool done = false; // Static fields are shared between all threads


        public void ExampleThread()
        {
            new Thread(Go).Start();
            Go();
        }

        private void Go()
        {
            if (!done) { done = true; Console.WriteLine("Done");  }
        }


        // show concurrency
        public void StartTaskTest()
        {
            Task t1 = Task.Run(() => DoWork("Number One", ConsoleColor.Red));
            Task t2 = Task.Run(() => DoWork("Number Two", ConsoleColor.Green));

            Thread.Sleep(1000);
            t1.Wait();
            t2.Wait();
        }

        private void DoWork(String name, ConsoleColor colour)
        {
            for (int i = 0; i < 30; i++)
            {
                Console.ForegroundColor = colour;
                Console.WriteLine($"Name {name} no={i}");
            }
        }



    }
}
