﻿// Copyright 2012-2013 Albert L. Hives, Chris Patterson, Rajesh Gande, et al.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace HareDu.Tests
{
    using System;
    using System.Collections.Generic;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public class QueueTests :
        HareDuTestBase
    {
        [Test]
        public void Verify_Can_Create_Queue()
        {
            var request = Client.CreateQueue(Settings.Default.VirtualHost, Settings.Default.Queue, x => x.IsDurable());
            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Create_Queue_Using_TPL()
        {
            var request = Client.CreateQueue(Settings.Default.VirtualHost, Settings.Default.Queue, x => x.IsDurable());
            var response = request.ContinueWith(x => { Assert.AreEqual(true, x.Result.IsSuccessStatusCode); });
            response.Wait();
        }

        [Test]
        public void Verify_Can_Delete_Queue()
        {
            var request = Client.DeleteQueue(Settings.Default.VirtualHost, Settings.Default.Queue);
            Assert.AreEqual(true, request.Result.IsSuccessStatusCode);
        }

        [Test]
        public void Verify_Can_Delete_Queue_Using_TPL()
        {
            var request = Client.DeleteQueue(Settings.Default.VirtualHost, Settings.Default.Queue);
            var response = request.ContinueWith(x => { Assert.AreEqual(true, x.Result.IsSuccessStatusCode); });
            response.Wait();
        }

        [Test]
        public void Verify_Can_Get_All_Queues()
        {
            var queues = Client.GetListOfAllQueues()
                               .Result
                               .GetResponse<IEnumerable<Queue>>();

            foreach (var queue in queues)
            {
                Console.WriteLine("Name: {0}", queue.Name);
                Console.WriteLine("Virtual Host: {0}", queue.VirtualHostName);
                Console.WriteLine("Memory: {0}", queue.Memory);
                Console.WriteLine("Messages: {0}", queue.Messages);
                Console.WriteLine("Messages Ready: {0}", queue.MessagesReady);
                Console.WriteLine("Messages Unacknowledged: {0}", queue.MessagesUnacknowledged);
                Console.WriteLine("Node: {0}", queue.Node);
                Console.WriteLine("Durable: {0}", queue.IsDurable);
                Console.WriteLine("Consumers: {0}", queue.Consumers);
                Console.WriteLine("Idle Since: {0}", queue.IdleSince);
                Console.WriteLine("****************************************************");
                Console.WriteLine();
            }
        }

        [Test]
        public void Verify_Can_Get_All_Queues_Using_TPL()
        {
            var request = Client.GetListOfAllQueues();
            var response = request.ContinueWith(x =>
                                                    {
                                                        var queues = x.Result.GetResponse<IEnumerable<Queue>>();
                                                        foreach (var queue in queues)
                                                        {
                                                            Console.WriteLine("Name: {0}", queue.Name);
                                                            Console.WriteLine("Virtual Host: {0}", queue.VirtualHostName);
                                                            Console.WriteLine("Memory: {0}", queue.Memory);
                                                            Console.WriteLine("Messages: {0}", queue.Messages);
                                                            Console.WriteLine("Messages Ready: {0}", queue.MessagesReady);
                                                            Console.WriteLine("Messages Unacknowledged: {0}",
                                                                              queue.MessagesUnacknowledged);
                                                            Console.WriteLine("Node: {0}", queue.Node);
                                                            Console.WriteLine("Durable: {0}", queue.IsDurable);
                                                            Console.WriteLine("Consumers: {0}", queue.Consumers);
                                                            Console.WriteLine("Idle Since: {0}", queue.IdleSince);
                                                            Console.WriteLine(
                                                                "****************************************************");
                                                            Console.WriteLine();
                                                        }
                                                    });
            response.Wait();
        }
    }
}