// Configuration.cs
// 2-6-2021
// Copyright 2021 Dramatic Development - Victor Vogelpoel
// If this works, it was written by Victor Vogelpoel (victor@victorvogelpoel.nl).
// If it doesn't, I don't know who wrote it.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.JsonAdaptor;

namespace SampleVaultApplication
{
    [DataContract]
    public class Configuration
    {
        [DataMember]
        [Security(ChangeBy = SecurityAttribute.UserLevel.SystemAdmin)]
        [JsonConfEditor(
            TypeEditor      = "options",
            Options         = "{selectOptions:[\"OFF\", \"ERROR\", \"WARNING\", \"INFO\"]}",
            Label           = "Log level",
            HelpText        = "Configure the level of logging messages to M-Files vault object",
            IsRequired      = false,
            DefaultValue    = "OFF")]
        public string LogLevel { get; set; } = "OFF";
    }
}