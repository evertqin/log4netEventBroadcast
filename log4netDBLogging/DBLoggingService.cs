using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PGI.Data;
using PGI.Entities;
using PGI.Entities.Utils;
using PGI.UI.Forms;
using PGI.UI.Forms.Utils;

namespace log4netDBLogging
{
    public class DBLoggingService
    {
        private static volatile DBLoggingService _instance;
        private static object _mutex = new object();
        public ApplicationLog AppLog { get; set; }
        private SqlEntityManager _entMgr;
        private Staff _currentStuff;


        private DBLoggingService() { }

        public static DBLoggingService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new DBLoggingService();
                        }
                    }
                }
                return _instance;
            }
        }


        /// <summary>
        /// You can either specify the logging server and application name here or set it up in the log4net config file
        /// </summary>
        /// <param name="server"></param>
        /// <param name="applicationName"></param>
        public void Start(PGIServer server, string applicationName)
        {
            _entMgr = AuthenticationUtils.GetSqlEntityManager(
                    LoginTypeEnum.WindowsActiveDirectory,
                    applicationName,
                    server, PGIDB.SmartBase
                    );

            AppLog = UIUtils.LogApplicationOpen(_entMgr, applicationName, _entMgr.CurrentStaff);
        }

        public void Broadcast(PGIServer server, string applicationName, DBLoggingEventArgs loggingInfo)
        {
            if (_entMgr == null)
            {
                _entMgr = AuthenticationUtils.GetSqlEntityManager(
                    LoginTypeEnum.WindowsActiveDirectory,
                    applicationName,
                    server, PGIDB.SmartBase
                    );
            }

            if (AppLog == null)
            {
                AppLog = UIUtils.LogApplicationOpen(_entMgr, applicationName, _entMgr.CurrentStaff);
            }
            //%d [%thread] %-5level %logger [%ndc] - %message%newline%exception
            AppLog.Comments += loggingInfo.FormattedMessage + "\n";
            AppLog.Save();

        }

        public void Finished()
        {
            if (_entMgr != null && AppLog != null)
            {
                UIUtils.LogApplicationClose(_entMgr, AppLog);
            }
        }

    }
}
