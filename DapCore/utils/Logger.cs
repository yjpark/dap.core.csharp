using System;
using System.Diagnostics;
using System.IO;
//using System.Text.RegularExpressions;

namespace angeldnd.dap {
    public struct LoggerConsts {
        public const string CRITICAL = "CRITICAL";
        public const string ERROR = "ERROR";
        public const string INFO = "INFO";
        public const string DEBUG = "DEBUG";
    }

    public interface Logger {
        bool LogDebug { get; }
        void Critical(string format, params object[] values);
        void Error(string format, params object[] values);
        void Info(string format, params object[] values);
        void Debug(string format, params object[] values);
    }

    public interface LogProvider {
        void AddLog(string type, StackTrace stackTrace, string format, params object[] values);
    }

    public class Log {
        public static LogProvider Provider = null;

        public static void AddLog(string type, StackTrace stackTrace, string format, params object[] values) {
            Provider.AddLog(type, stackTrace, format, values);
        }

        public static bool LogDebug;

        public static void Critical(string format, params object[] values) {
            StackTrace stackTrace = new StackTrace(1, true);
            Provider.AddLog(LoggerConsts.CRITICAL, stackTrace, format, values);
        }

        public static void Error(string format, params object[] values) {
            StackTrace stackTrace = new StackTrace(1, true);
            Provider.AddLog(LoggerConsts.ERROR, stackTrace, format, values);
        }

        public static void Info(string format, params object[] values) {
            Provider.AddLog(LoggerConsts.INFO, null, format, values);
        }

        public static void Debug(string format, params object[] values) {
            if (LogDebug) Provider.AddLog(LoggerConsts.DEBUG, null, format, values);
        }

        public static void Custom(string type, string format, params object[] values) {
            Provider.AddLog(type, null, format, values);
        }
    }

    public class DebugLogger : Logger {
        public static readonly DebugLogger Instance = new DebugLogger(2);

        public readonly int IgnoreStackTraceCount;

        private DebugLogger(int ignoreStackTraceCount) {
            IgnoreStackTraceCount = ignoreStackTraceCount;
        }

        public bool LogDebug {
            get { return true; }
        }

        public string GetMethodPrefix() {
            StackTrace stackTrace = new StackTrace(IgnoreStackTraceCount, true);
            if (stackTrace == null || stackTrace.FrameCount < 1) {
                return "() ";
            }
            StackFrame stackFrame = stackTrace.GetFrame(0);
            var method = stackFrame.GetMethod();
            return method.Name + "() ";
        }

        public void Critical(string format, params object[] values) {
            StackTrace stackTrace = new StackTrace(IgnoreStackTraceCount, true);
            Log.AddLog(LoggerConsts.CRITICAL, stackTrace, format, values);
        }

        public void Error(string format, params object[] values) {
            StackTrace stackTrace = new StackTrace(IgnoreStackTraceCount, true);
            Log.AddLog(LoggerConsts.ERROR, stackTrace, format, values);
        }

        public void Info(string format, params object[] values) {
            Log.AddLog(LoggerConsts.INFO, null, format, values);
        }

        public void Debug(string format, params object[] values) {
            Log.AddLog(LoggerConsts.DEBUG, null, format, values);
        }

        public void LogWithPatterns(string type, string[] patterns, string format, params object[] values) {
            string msg = format;
            if (values != null && values.Length > 0) msg = string.Format(format, values);

            if (IsMatchPatterns(patterns, msg)) {
                StackTrace stackTrace = new StackTrace(IgnoreStackTraceCount, true);
                Log.AddLog(type, stackTrace, format, values);
            } else {
                Log.AddLog(type, null, format, values);
            }
        }

        public void LogWithPattern(string type, string pattern, string format, params object[] values) {
            string msg = format;
            if (values != null && values.Length > 0) msg = string.Format(format, values);

            if (IsMatchPattern(pattern, msg)) {
                StackTrace stackTrace = new StackTrace(IgnoreStackTraceCount, true);
                Log.AddLog(type, stackTrace, format, values);
            } else {
                Log.AddLog(type, null, format, values);
            }
        }

        public bool IsMatchPatterns(string[] patterns, string msg) {
            foreach (string pattern in patterns) {
                if (IsMatchPattern(pattern, msg)) {
                    return true;
                }
            }
            return false;
        }

        public bool IsMatchPattern(string pattern, string msg) {
            if (string.IsNullOrEmpty(pattern)) {
                return true;
            }

            string[] segments = pattern.Split(' ');
            foreach (string segment in segments) {
                if (!IsMatchSegment(segment, msg)) {
                    return false;
                }
            }
            return true;
        }

        public bool IsMatchSegment(string segment, string msg) {
            string[] conditions = segment.Split('|');
            foreach (string condition in conditions) {
                if (IsMatchCondition(condition, msg)) {
                    return true;
                }
            }
            return false;
        }

        public bool IsMatchCondition(string condition, string msg) {
            if (condition.StartsWith("!")) {
                condition = condition.Replace("!", "");
                return !IsMatchCondition(condition, msg);
            }

            //use "+" to match spaces between words
            condition = condition.Replace("+", " ");

            //Smart Case
            string lowerCondition = condition.ToLower();
            if (lowerCondition == condition) {
                msg = msg.ToLower();
            }
            if (condition.StartsWith("^")) {
                condition = condition.Replace("^", "");
                return msg.StartsWith(condition);
            } else if (condition.StartsWith("$")) {
                condition = condition.Replace("$", "");
                return msg.EndsWith(condition);
            } else {
                return msg.Contains(condition);
            }
        }
    }
}