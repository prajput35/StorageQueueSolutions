using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;



namespace StorageQueueSolutions
{
    class Program
    {
        private static string queue_connection_string = "DefaultEndpointsProtocol=https;AccountName=az204300sa;AccountKey=EPU9Oxvr4tG58GukQJcgDs+yxeaaPSv7j+v+pBr8DUzTNScV0JC9LzJOryZgBMvAGDBTm45GM9wGaSUu/jPI2Q==;EndpointSuffix=core.windows.net";
        private static string queue_name = "appqueue";
        static void Main(string[] args)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(queue_connection_string);
            CloudQueueClient cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference(queue_name);


            //AddMessages(cloudQueue);
            //Console.WriteLine("All messages sent");
            GetMessages(cloudQueue);
            Console.ReadLine();
            
        }

        private static void AddMessages(CloudQueue cloudQueue)
        {
            for (int i = 0; i < 5; ++i)
            {
                Order obj = new Order();
                CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(obj.ToString());
                cloudQueue.AddMessage(cloudQueueMessage);
            }
        }

        private static void GetMessages(CloudQueue _queue)
        {
            _queue.FetchAttributes();
            int? _count = _queue.ApproximateMessageCount;

            for (int i = 0; i < _count; i++)
            {
                CloudQueueMessage _message = _queue.GetMessage();
                Console.WriteLine(_message.AsString);
                _queue.DeleteMessage(_message);
            }
        }
    }
}
    