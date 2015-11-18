using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using log4net.Core;
using PGI.Data;

namespace log4netDBLogging
{
    public class DBLogAppender : AppenderSkeleton
    {
        public string DB { get; set; }
        public string ApplicationName { get; set; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            var dbLoggingEventArgs = new DBLoggingEventArgs
					{
						DataTime = loggingEvent.TimeStamp,
						Level = loggingEvent.Level.ToString(),
						LoggerName = loggingEvent.LoggerName,
						ThreadName = loggingEvent.ThreadName,
						Message = loggingEvent.MessageObject.ToString(),
                        FormattedMessage = RenderLoggingEvent(loggingEvent),
					};
            var server = (PGIServer)Enum.Parse(typeof (PGIServer), DB, true);
            DBLoggingService.Instance.Broadcast(server, ApplicationName, dbLoggingEventArgs);
        }

    }
}
