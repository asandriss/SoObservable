using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;


namespace ImSoObeservable
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            IObservable<int> sequence = Generate();     //Interval();          // Create();    

            sequence.Subscribe
            (
                next => Console.WriteLine($"OnNext: {next} - ThreadID = {Thread.CurrentThread.ManagedThreadId}"),
                exc => Console.WriteLine($"OnError: {exc}"),
                () => Console.WriteLine("OnCompleted.")
            );

            Console.WriteLine($"Hello World! - ThreadID = {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
            //while (true)
            //{
            //    Console.WriteLine($"Going to sleep now - ThreadID = {Thread.CurrentThread.ManagedThreadId}");
            //    Thread.Sleep(1250);
            //    Console.WriteLine($"Just woke up! doing heavy maths - ThreadID = {Thread.CurrentThread.ManagedThreadId}");
            //    for (int i = 0; i < 10000000; i++)
            //    {
            //        var result = (i * random.Next(10000)) / ((random.Next(99)+1)/100m);
            //    }
            //}
        }

        private static IObservable<long> Interval()
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(500));
        }

        private static IObservable<int> Create()
        {
            // complete control over creation
            return Observable.Create<int>(obs =>
            {
                obs.OnNext(1);
                obs.OnNext(22);
                obs.OnNext(42);
                obs.OnCompleted();
                return Disposable.Empty;
            });
        }

        private static IObservable<int> Generate()
        {
            // generate observable - set seed, break condition (false for infinite), increment, selector
            return Observable.Generate
            (
                1,
                o => o <= 5,
                o => o + 1,
                o => o
            ).Delay(TimeSpan.FromMilliseconds(500));
            // interesting that this sequence executes on main thread, while Interval executes in background.
        }
    }
}
