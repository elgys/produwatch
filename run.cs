using System;
using System.Threading;
using stopwatch;

class run
{

    private  static bool state {get; set;}


    static void Main(String[] args)
    {
        state = false;
        Console.WriteLine("first make a Stopwatch on {0:HH:mm:ss.fff}",DateTime.Now);
        Stopwatch sw = new Stopwatch(6000);
        Stopwatch sw2 = new Stopwatch(5000);
        sw.FinishedEvent += EndFinishedEvent;
        sw2.FinishedEvent += FinishedEvent;
        while(!state)
        {
            System.Threading.Thread.Sleep(1000);
        }
        Console.WriteLine("last end of Stopwatch on {0:HH:mm:ss.fff}",DateTime.Now);

    }

    public static void FinishedEvent(Object source, String info)
    {
        Console.Beep(2000,100);
    }

    public static void EndFinishedEvent(Object source, String info)
    {
        Console.Beep(2000,100);
        state = true;
    }

}
