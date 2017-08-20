using DTuriasCore.Models;
using DTuriasConnectorTwitter.Http;
using DTuriasConnectorTwitter.Tasks;
using log4net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Reflection;
using log4net.Config;

namespace DTuriasConnectorTwitter
{
    class Program
    {
        static ILog _logger = LogManager.GetLogger(typeof(Program));
        static Connector connector;

        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            connector = new Connector("http://localhost:54780");
            Manager manager;

            using (var fileProperties = new StreamReader(new FileStream("properties.json", FileMode.Open)))
            {
                manager = new Manager(fileProperties);              
            }

            using (var fileQueries = new StreamReader(new FileStream("queries.json", FileMode.Open)))
            {
                var search = JsonConvert.DeserializeObject<Config.Queries>(fileQueries.ReadToEnd());

                foreach (var message in search.messagesToSearch)
                {
                    var messageTask = new MessageTask(manager, message);
                    Thread messageTaskThread = new Thread(new ThreadStart(messageTask.Run));
                    messageTaskThread.Start();
                }



                /*var placeTask = new PlaceTask();
                Thread placeTaskThread = new Thread(new ThreadStart(placeTask.run));
                placeTaskThread.Start();

                var trendTask = new TrendTask();
                Thread trendTaskThread = new Thread(new ThreadStart(trendTask.run));
                trendTaskThread.Start();

                var userTask = new UserTask();
                Thread userTaskThread = new Thread(new ThreadStart(userTask.run));
                userTaskThread.Start();*/


                
            }
            // Publish the Tweet "Hello World" on your Timeline
            //Tweet.PublishTweet("Hello World!");
            /*TodoItem i = new TodoItem()
            {
                Name = "VVV",
                IsComplete = false
            };

            //_logger.Info(JsonConvert.SerializeObject(i));
            connector.CreateItem(i);*/
 

            Console.ReadLine();
        }



    }
}
