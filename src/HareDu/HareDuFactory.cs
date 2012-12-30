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

namespace HareDu
{
    using System;
    using Contracts;

    public static class HareDuFactory
    {
        public static HareDuClient New(Action<ClientInitArgs> args)
        {
            try
            {
                var hareDuInitArgs = new ClientInitArgsImpl();
                args(hareDuInitArgs);
                var client = new HareDuClientImpl(hareDuInitArgs);

                return client;
            }
            catch (Exception e)
            {
                throw new HareDuClientInitException("", e);
            }
        }
    }
}