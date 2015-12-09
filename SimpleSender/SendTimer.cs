using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSender
{
    /// <summary>
    /// Sends an event when there has been no activity for certain time period
    /// </summary>
    class SendTimer
    {
        public event EventHandler PeriodExceeded;

        private Timer _timer = null;
        private int _period;

        /// <summary>
        /// The InactivityTimer factory
        /// </summary>
        static public SendTimer CreateTimer(int period)
        {
            return new SendTimer(period);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SendTimer(int period)
        {
            _period = period;
        }

        public void ResetTimer()
        {
            if(_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
            
            // Create a timer with one hour interval.
            _timer = new Timer(new TimerCallback(TimerProc),null, _period, Timeout.Infinite);
 
        }

        private void TimerProc(object state)
        {
            if(PeriodExceeded != null)
            {
                PeriodExceeded(this, null);
            }

            ResetTimer();
        }

    }
}
