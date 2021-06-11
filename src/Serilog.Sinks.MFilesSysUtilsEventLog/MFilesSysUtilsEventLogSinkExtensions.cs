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
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Serilog.Sinks.MFilesSysUtilsEventLog;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.MFilesSysUtilsEventLogSink() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class MFilesSysUtilsEventLogSinkExtensions
    {
        const string DefaultOutputTemplate = "{Message}{NewLine}{Exception}";

        /// <summary>
        /// Writes log events to the M-Files SysUtils event log helper
        /// </summary>
        /// <param name="loggerSinkConfiguration">Logger sink configuration</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="levelSwitch">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="loggerSinkConfiguration"/> is <code>null</code></exception>
        public static LoggerConfiguration MFilesSysUtilsEventLogSink(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            LogEventLevel restrictedToMinimumLevel                  = LevelAlias.Minimum,
            string outputTemplate                                   = DefaultOutputTemplate,
            IFormatProvider formatProvider                          = null,
            LoggingLevelSwitch levelSwitch                          = null)
        {
            if (loggerSinkConfiguration is null) throw new ArgumentNullException(nameof(loggerSinkConfiguration));

            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);

            return loggerSinkConfiguration.Sink(new MFilesSysUtilsEventLogSink(formatter), restrictedToMinimumLevel, levelSwitch);
        }


        /// <summary>
        /// Writes log events to the M-Files SysUtils event log helper
        /// </summary>
        /// <param name="loggerSinkConfiguration">Logger sink configuration</param>
        /// <param name="formatter">Controls the rendering of log events into text, for example to log JSON. To
        /// control plain text formatting, use the overload that accepts an output template.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
        /// <param name="levelSwitch"></param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="loggerSinkConfiguration"/> is <code>null</code></exception>
        /// <exception cref="ArgumentNullException">When <paramref name="formatter"/> is <code>null</code></exception>
        public static LoggerConfiguration MFilesSysUtilsEventLogSink(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            ITextFormatter formatter,
            LogEventLevel restrictedToMinimumLevel                  = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch                          = null)
        {
            if (loggerSinkConfiguration is null)    throw new ArgumentNullException(nameof(loggerSinkConfiguration));
            if (formatter is null)                  throw new ArgumentNullException(nameof(formatter));

            return loggerSinkConfiguration.Sink(new MFilesSysUtilsEventLogSink(formatter), restrictedToMinimumLevel, levelSwitch);
        }
    }
}
