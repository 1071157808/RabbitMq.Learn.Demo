﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "192.168.23.146",
                UserName = "datamip",
                Password = "datamip",
            };

            //第一步：创建connection
            var connection = factory.CreateConnection();

            //第二步：创建一个channel
            var channel = connection.CreateModel();

            for (int i = 0; i < 100; i++)
            {
                var msg = Encoding.UTF8.GetBytes(string.Format("{0} :{1}", i, "你好"));

                var routingKey = i % 13 == 0 ? "*.com" : "*.cn";

                //第五步：发布消息
                channel.BasicPublish("mytopicexchange", routingKey: routingKey, basicProperties: null, body: msg);

                Console.WriteLine(i);
            }
        }
    }
}
