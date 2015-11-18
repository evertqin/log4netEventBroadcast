using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace log4netDBLogging
{
   public class DBLoggingEventArgs : EventArgs
    {
        public DateTime DataTime { get; set; }
        public string Level { get; set; }
        public string LoggerName { get; set; }
        public string ThreadName { get; set; }
        public string Message { get; set; }
        public string FormattedMessage { get; set; }
    }
}
