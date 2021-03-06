using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public static class ContextConsts {
        public const string TypeContext = "Context";

        public const string KeyProperties = "Properties";
        public const string KeyChannels = "Channels";
        public const string KeyHandlers = "Handlers";
        public const string KeyVars = "Vars";
        public const string KeyManners = "Manners";
        public const string KeyBus = "Bus";

        [DapParam(typeof(string))]
        public const string SummaryPath = "path";
        [DapParam(typeof(bool))]
        public const string SummaryDebugging = "debugging";

        [DapParam(typeof(string))]
        public const string SummaryValueType = "value_type";
        [DapParam(typeof(string))]
        public const string SummaryValue = "value";
        [DapParam(typeof(Data))]
        public const string SummaryData = "data";
        [DapParam(typeof(int))]
        public const string SummaryCheckerCount = "cherker_count";
        [DapParam(typeof(int))]
        public const string SummaryWatcherCount = "watcher_count";
        [DapParam(typeof(int))]
        public const string Summary2ndWatcherCount = "2nd_watcher_count";

        public const string SuffixHandlerAsync = "~";
        public const string SuffixChannelResponse = ">";

        public static string GetAspectKey(params string[] segments) {
            return string.Join(".", segments);
        }

        public static string GetAsyncHandlerKey(string handlerKey) {
            return handlerKey + SuffixHandlerAsync;
        }

        public static string GetResponseChannelKey(string handlerKey) {
            return handlerKey + SuffixChannelResponse;
        }
    }
}
