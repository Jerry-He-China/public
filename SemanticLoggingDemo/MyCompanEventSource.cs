﻿using System.Diagnostics.Tracing;

namespace SemanticLoggingDemo
{
    [EventSource(Name = "MyCompany")]
    public class MyCompanyEventSource : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Page = (EventKeywords)1;
            public const EventKeywords DataBase = (EventKeywords)2;
            public const EventKeywords Diagnostic = (EventKeywords)4;
            public const EventKeywords Perf = (EventKeywords)8;
        }

        public class Tasks
        {
            public const EventTask Page = (EventTask)1;
            public const EventTask DbQuery = (EventTask)2;
        }

        private static MyCompanyEventSource _log = new MyCompanyEventSource();
        private MyCompanyEventSource() { }
        public static MyCompanyEventSource Log { get { return _log; } }

        [Event(1, Message = "Application Failure: {0}",
            Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
        internal void Failure(string message)
        {
            WriteEvent(1, message);
        }

        [Event(2, Message = "Starting up.", Keywords = Keywords.Perf,
            Level = EventLevel.Informational)]
        internal void Startup()
        {
            WriteEvent(2);
        }

        [Event(3, Message = "loading page {1} activityID={0}",
            Opcode = EventOpcode.Start,
            Task = Tasks.Page, Keywords = Keywords.Page,
            Level = EventLevel.Informational)]
        internal void PageStart(int id, string url)
        {
            if (IsEnabled()) WriteEvent(3, id, url);
        }
    }
}
