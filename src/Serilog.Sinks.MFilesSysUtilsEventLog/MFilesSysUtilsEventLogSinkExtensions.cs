// MFilesSysUtilsEventLogSinkExtensions.cs
// 18-5-2021
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
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MFilesSysUtilsEventLog;

namespace Serilog
{
    public static class MFilesSysUtilsEventLogSinkExtensions
    {
        public static LoggerConfiguration MFilesSysUtilsEventLogSink(this LoggerSinkConfiguration loggerSinkConfiguration, LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum, IFormatProvider formatProvider = null, LoggingLevelSwitch levelSwitch = null)
        {
            return loggerSinkConfiguration.Sink(new MFilesSysUtilsEventLogSink(formatProvider), restrictedToMinimumLevel, levelSwitch);
        }
    }
}
