using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExamPlatform.Logger
{
    public class SingletonFirst
    {
        private static SingletonFirst _instance;
        private SingletonFirst() { }
        public static SingletonFirst Instance
        {
            get
            {
                if (_instance == null)
                {
                     _instance = new SingletonFirst();
                }
                return _instance;
            }
        }
        public ILog GetLogger()
        {
            ILog logger = LogManager.GetLogger(typeof(Program));
            return logger;
        }
        public ILog SetLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            ILog logger = LogManager.GetLogger(typeof(Program));
            return logger;
        }

    }
}
