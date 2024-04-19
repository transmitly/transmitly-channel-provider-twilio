// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
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

using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Transmitly.ChannelProvider.TwilioClient
{
	internal static class EnumUtil
	{
		public static T? ToEnum<T>(string? value)
		{
			if(string.IsNullOrWhiteSpace(value))
				return default;

			var type = typeof(T);
			if (type.GetTypeInfo().IsEnum)
			{
				foreach (var name in Enum.GetNames(type))
				{
					var attr = type.GetRuntimeField(name).GetCustomAttribute<EnumMemberAttribute>(true);
					if (attr != null && attr.Value == value)
						return (T)Enum.Parse(type, name);
				}

				return default;
			}
			throw new InvalidOperationException("Not Enum");
		}
	}
}
