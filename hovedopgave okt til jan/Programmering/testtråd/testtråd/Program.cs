using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testtråd
{
    class Program
    {
        static void Main(string[] args)
        {
            Testy test = new Testy();
            Random random = new Random();
            //øverste metode skal køre i et
            var tasks = new Task[3];

                
                
                for (int i = 0; i < 3; i++)
                {
                    int randomnumber = random.Next(3, 10);
                    TimeSpan t = TimeSpan.FromSeconds(randomnumber);
                    int k = i;
                    tasks[i] = Task.Factory.StartNew(() => test.Taller(k, t));
                    /*
                    Thread f = new Thread(() =>
                    
                        test.Taller(k, t)
                    );
                    f.Start();
                    f.Join();
                    */
                }
            Task.WaitAll(tasks);

            
            Console.WriteLine("Færdig");          
        }       
    }
    class Testy
    {
        Random random = new Random();
        public void Taller(int alfa, TimeSpan t)
        {
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("Traad" + alfa + "  i: "+i+" --før sekund " + DateTime.Now.Second + " wait: " +t );

                Thread.Sleep(t); 
            }
               
            
        }
    }

}
