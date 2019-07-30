using System;
using System.Reactive.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;


namespace ImSoObeservable
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            IObservable<long> sequence = Observable.Interval(TimeSpan.FromMilliseconds(500));

            sequence.Subscribe
            (
                next => Console.WriteLine($"OnNext: {next} - ThreadID = {Thread.CurrentThread.ManagedThreadId}"),
                exc => Console.WriteLine($"OnError: {exc}"),
                () => Console.WriteLine("OnCompleted.")
            );

            Console.WriteLine($"Hello World! - ThreadID = {Thread.CurrentThread.ManagedThreadId}");

            while (true)
            {
                Console.WriteLine($"Going to sleep now - ThreadID = {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1250);
                Console.WriteLine($"Just woke up! doing heavy maths - ThreadID = {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 10000000; i++)
                {
                    var result = (i * random.Next(10000)) / ((random.Next(99)+1)/100m);
                }
            }
        }
    }
}
