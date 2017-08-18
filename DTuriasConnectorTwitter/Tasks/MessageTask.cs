using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DTuriasConnectorTwitter.Tasks
{
    class MessageTask
    {
        static ILog _logger = LogManager.GetLogger(typeof(MessageTask));

        public MessageTask()
        {
        }

        public void run()
        {
            while (true)
            {
                _logger.Info("MessageTask");
                Thread.Sleep(4000);
            }
        }
    }
}
