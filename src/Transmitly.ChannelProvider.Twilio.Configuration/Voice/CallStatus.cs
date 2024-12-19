﻿// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
//  
//  Licensed under the Apache License, Version 2.0 (the "License")
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System.Runtime.Serialization;

namespace Transmitly.ChannelProvider.TwilioClient.Configuration.Voice
{
    /// <summary>
    /// A descriptive status for the call.
    /// </summary>
    public enum CallStatus
    {
        Unknown,
        [EnumMember(Value = "queued")]
        Queued,
        [EnumMember(Value = "initiated")]
        Initiated,
        [EnumMember(Value = "ringing")]
        Ringing,
        [EnumMember(Value = "in-progress")]
        InProgress,
        [EnumMember(Value = "completed")]
        Completed,
        [EnumMember(Value = "busy")]
        Busy,
        [EnumMember(Value = "failed")]
        Failed,
        [EnumMember(Value = "no-answer")]
        NoAnswer
    }
}