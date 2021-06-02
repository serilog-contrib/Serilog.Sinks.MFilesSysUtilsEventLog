// MFilesSysUtilsEventLogSink.cs
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
using System.IO;
using MFiles.VAF.Common;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.MFilesSysUtilsEventLog
{
    public class MFilesSysUtilsEventLogSink : ILogEventSink
    {
        const int MaximumPayloadLengthChars = 31839; // https://msdn.microsoft.com/en-us/library/e29k5ebc%28v=vs.110%29.aspx

        private readonly ITextFormatter _textFormatter;

        public MFilesSysUtilsEventLogSink(ITextFormatter formatter)
        {
            _textFormatter              = formatter ?? throw new ArgumentNullException(nameof(formatter));

        }

        public void Emit(LogEvent logEvent)
        {
            // Do not log Debug or Verbose level
            if (logEvent.Level == LogEventLevel.Debug || logEvent.Level == LogEventLevel.Verbose)
            {
                return;
            }


            using (var payloadWriter = new StringWriter())
            {
                _textFormatter.Format(logEvent, payloadWriter);

                var payload = payloadWriter.ToString();

                if (payload.Length > MaximumPayloadLengthChars)
                {
                    payload = payload.Substring(0, MaximumPayloadLengthChars);
                }

                var entryType = System.Diagnostics.EventLogEntryType.Information;
                if (logEvent.Level == LogEventLevel.Warning)
                {
                    entryType = System.Diagnostics.EventLogEntryType.Warning;
                }
                else if (logEvent.Level == LogEventLevel.Error || logEvent.Level == LogEventLevel.Fatal)
                {
                    entryType = System.Diagnostics.EventLogEntryType.Error;
                }

                SysUtils.ReportToEventLog(payload, entryType);
            }
        }
    }
}
