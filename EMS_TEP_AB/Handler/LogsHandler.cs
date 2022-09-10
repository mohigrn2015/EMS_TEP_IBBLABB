using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EMS_TEP_AB.Handler
{
    public class LogsHandler
    {
        private Object _logFileLock;
        private string _logFileLocation;
        private EventLog _eventLog;

        public LogsHandler()
        {
            string directory = "D:\\EMS_Log";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            else
            {
                _logFileLocation = directory;
            }
        }
        public void Log(string message)
        {
            try
            {
                string fileName = "EMS" + DateTime.Now.ToString("yyyy_mm_dd") + ".txt";
                _logFileLock = new object();
                lock (_logFileLock)
                {
                    File.AppendAllText(_logFileLocation + "\\" + fileName, DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss") + "# " + message + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                _eventLog.WriteEntry("Log " + ex.Message.ToString(), EventLogEntryType.Error);
            }
        }
    }
}
