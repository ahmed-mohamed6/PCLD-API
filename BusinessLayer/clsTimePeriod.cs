using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    internal class clsTimePeriod
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public clsTimePeriod(TimeSpan Start, TimeSpan End)
        {
            if (End < Start)
            {
                throw new ArgumentException("End time must be greater than or equal to start time.");
            }
            this.Start = Start;
            this.End = End;
        }
    }
}
