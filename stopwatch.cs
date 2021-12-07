using System;
using System.Timers;

namespace stopwatch
{
    class Stopwatch
    {
        public event EventHandler<string> FinishedEvent;

        private long Time {get; set;}

        private bool isActive{get; set;}

        private bool isStoped{get; set;}

        private DateTime PrevTime{get; set;}

        private Timer nextevent;

        public Timer NextEvent{
            private get
            {
                return this.nextevent;
            }
            set
            {
                Timer next;
                if (this.NextEvent != null )
                {
                    this.NextEvent.Dispose();
                }
                if (this.isActive == true)

                {
                    next= new Timer(1000);
                    next.AutoReset = true;
                    next.Elapsed += ActiveUpdate;
                    next.Start();
                }
                else{
                    next = new Timer(this.Time);
                    next.AutoReset = false;
                    NextEvent.Elapsed += InactiveUpdate;
                    next.Start();
                }
                this.nextevent = next;
            }
        }

        public Stopwatch (int seconds)
        {
            PrevTime = DateTime.Now;
            Time = seconds;
            isActive = true;
            NextEvent = null;
            isStoped = false;
            Console.WriteLine(PrintTime());
        }

        public void Stop()
        {
            if (this.isStoped == false)
            {
                ChangeTime(DateTime.Now);
                this.NextEvent.Dispose();
                this.isStoped = true;
            }
        }

        public void Resume()
        {
            if (this.isStoped == true)
            {
                this.PrevTime = DateTime.Now;
                this.NextEvent = null;
                this.isStoped = false;
            }
        }



        private void ChangeTime(DateTime date)
        {
            TimeSpan Interval = date.Subtract(this.PrevTime);
            this.PrevTime = date;
            this.Time -= (long) Interval.TotalMilliseconds;
            if (Time <= 0)
            {
                finish();
            }
        }

        private string PrintTime()
        {
            long time;
            long mili = this.Time % 1000;
            time =(long) ((this.Time - mili) / 1000);
            long seconds = time % 60;
            time = (long) ((time - seconds) / 60);
            long min = time % 60;
            time = (long)  ((time - min) / 60);
            long hours = time;
            String ans = String.Format("{0:00}:{1:00}:{2:00}",hours,min,seconds);
            return ans;
        }

        private void ActiveUpdate(Object source, ElapsedEventArgs e)
        {
            ChangeTime(DateTime.Now);
            Console.WriteLine(PrintTime());
        }

        private void InactiveUpdate(Object source, ElapsedEventArgs e)
        {
            finish();
        }

        private void finish()
        {
            FinishedEvent?.Invoke(this, "finished");
            this.NextEvent.Dispose();
            isStoped = true;
            Console.WriteLine("done");
        }
    }
}
