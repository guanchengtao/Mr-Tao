using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.Common
{
    public class Log4NetWriter : ILogWriter
    {
        public void LogWriter(string str)
        {
            ILog logWriter = log4net.LogManager.GetLogger("GCT");
            logWriter.Error(str);
        }
    }
}
