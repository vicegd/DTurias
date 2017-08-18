using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DTuriasConnectorTwitter.Tasks
{
    class UserTask
    {
        static ILog _logger = LogManager.GetLogger(typeof(UserTask));

        public UserTask()
        {
        }

        public void run()
        {
            _logger.Info("UserTask");
            Thread.Sleep(4000);
        }
    }
}
