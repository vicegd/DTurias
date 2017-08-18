using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DTuriasConnectorTwitter.Tasks
{
    class TrendTask
    {
        static ILog _logger = LogManager.GetLogger(typeof(TrendTask));

        public TrendTask()
        {
        }

        public void run()
        {
            _logger.Info("TrendTask");
            Thread.Sleep(4000);
        }
    }
}
