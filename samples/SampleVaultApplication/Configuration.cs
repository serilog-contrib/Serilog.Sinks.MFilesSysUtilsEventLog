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