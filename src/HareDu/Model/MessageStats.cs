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

namespace HareDu.Model
{
    using Newtonsoft.Json;

    public class MessageStats
    {
        [JsonProperty("publish")]
        public int Published { get; set; }

        [JsonProperty("ack")]
        public int Acknowledged { get; set; }

        [JsonProperty("deliver")]
        public int Delivered { get; set; }

        [JsonProperty("deliver_get")]
        public int DeliveredOrGet { get; set; }
    }
}