using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DTuriasConnectorTwitter.Tasks
{
    class PlaceTask
    {
        static ILog _logger = LogManager.GetLogger(typeof(PlaceTask));

        public PlaceTask()
        {
        }

        public void run()
        {
            _logger.Info("PlaceTask");
            Thread.Sleep(4000);
        }
    }
}
