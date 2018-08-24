using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SDAU.GCT.OA.Common
{
    // public delegate void WriteLogDel(string str);
    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        // public static WriteLogDel WriteLogDelFunc;
        public static List<ILogWriter> ILogWriterList = new List<ILogWriter>();
        static LogHelper()
        {
            // WriteLogDelFunc = WriteLogToFile;        
            // WriteLogDelFunc += WriteLogToMongodb;
            //ILogWriterList.Add(new TextFileWriter());
            //ILogWriterList.Add(new SQLServerWriter());
            ILogWriterList.Add(new Log4NetWriter());
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    lock (ExceptionStringQueue)
                    {
                        if (ExceptionStringQueue.Count > 0)
                        {
                            string str = ExceptionStringQueue.Dequeue();
                            //从队列获取错误信息写到日志文件里
                            //WriteLogDelFunc(str);
                            foreach (var ILogWriter in ILogWriterList)
                            {
                                ILogWriter.LogWriter(str);
                            }
                        }
                        else
                        {
                            Thread.Sleep(30);
                        }
                    }
                }
            });
        }

        //private static void WriteLogToMongodb(string str)
        //{
        //    throw new NotImplementedException();
        //}

        //private static void WriteLogToFile(string str)
        //{
        //    throw new NotImplementedException();
        //}

        public static void WriteLog(string ExceptionText)
        {
            lock (ExceptionStringQueue)
            {
                ExceptionStringQueue.Enqueue(ExceptionText);
            }
        }
    }
}
