﻿using Serilog.Core;
using Serilog.Events;
using System.Threading;

namespace BigFiles.Logging
{
    class ThreadIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(
                propertyFactory.CreateProperty("ThreadId", Thread.CurrentThread.ManagedThreadId));
        }
    }
}
