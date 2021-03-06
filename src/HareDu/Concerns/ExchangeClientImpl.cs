﻿// Copyright 2013-2014 Albert L. Hives, Chris Patterson, et al.
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

namespace HareDu.Concerns
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Async;
    using Contracts;
    using Internal;
    using Model;

    internal class ExchangeClientImpl :
        HareDuClientBase,
        ExchangeClient
    {
        public ExchangeClientImpl(HareDuClientBehaviorImpl args) :
            base(args)
        {
        }

        public Task<IEnumerable<Exchange>> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                "Sent request to return all information pertaining to all exchanges on all virtual hosts on current RabbitMQ server.");

            string url = "api/exchanges";

            return base.Get(url, cancellationToken).As<IEnumerable<Exchange>>(cancellationToken);
        }

        public Task<Exchange> Get(string exchange, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return information pertaining to exchange '{0}' belonging to virtual host '{1}'.",
                    exchange, Init.VirtualHost));

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), exchange);

            return base.Get(url, cancellationToken).As<Exchange>(cancellationToken);
        }

        public Task<IEnumerable<Binding>> GetAllBindings(string exchange, Action<BindingDirection> args,
                                                         CancellationToken cancellationToken =
                                                             default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to return all the bindings for exchange '{0}' belonging to virtual host '{1}'.",
                    exchange, Init.VirtualHost));

            var argsImpl = new BindingDirectionImpl();
            args(argsImpl);

            string url = string.Format("api/exchanges/{0}/{1}/bindings/{2}",
                                       Init.VirtualHost.SanitizeVirtualHostName(), exchange, argsImpl.BindingDirection);

            return base.Get(url, cancellationToken).As<IEnumerable<Binding>>(cancellationToken);
        }

        public Task<ServerResponse> New(string exchange, Action<ExchangeBehavior> behavior,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(
                string.Format(
                    "Sent request to RabbitMQ server to create a new exchange '{0}' within virtual host '{1}'.",
                    exchange, Init.VirtualHost));

            var behaviorImpl = new ExchangeBehaviorImpl();
            behavior(behaviorImpl);

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), exchange);

            return base.Put(url, behaviorImpl, cancellationToken).Response<ServerResponse>(cancellationToken);
        }

        public Task<ServerResponse> Delete(string exchange,
                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.RequestCanceled(LogInfo);

            LogInfo(string.Format("Sent request to RabbitMQ server to delete exchange '{0}' from virtual host '{1}'.",
                                  exchange, Init.VirtualHost));

            string url = string.Format("api/exchanges/{0}/{1}", Init.VirtualHost.SanitizeVirtualHostName(), exchange);

            return base.Delete(url, cancellationToken).Response<ServerResponse>(cancellationToken);
        }
    }
}