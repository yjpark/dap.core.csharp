using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public interface IObject : ILogger {
        string Type { get; }
        int Revision { get; }
        string RevInfo { get; }

        bool DebugMode { get; }
        string[] DebugPatterns { get; }

        string LogPrefix { get; }

        void CriticalFromProxy(string format, params object[] values);
        void ErrorFromProxy(string format, params object[] values);
        void InfoFromProxy(string format, params object[] values);
        void DebugFromProxy(string format, params object[] values);
        void CustomFromProxy(string kind, string format, params object[] values);
    }

    public static class ObjectConsts {
        public const string KeyType = "type";
    }

    public abstract class Object : Logger, IObject, IBlockOwner {
        private readonly static DefaultLogWriter _ProxyDefaultWriter = new DefaultLogWriter(3);
        private readonly static DebugLogWriter _ProxyDebugWriter = new DebugLogWriter(3);

        private static T As<T>(object obj, bool logError) where T : class, IObject {
            if (obj == null) return null;

            if (!(obj is T)) {
                if (logError) {
                    Log.Error("Type Mismatched: <{0}> -> {1}: {2}",
                                typeof(T).FullName, obj.GetType().FullName, obj);
                }
                return null;
            }
            return (T)obj;
        }

        public static bool TryAs<T>(object obj, out T result) where T : class, IObject {
            result = As<T>(obj, false);
            return result != null;
        }

        public static T As<T>(object obj) where T : class, IObject {
            return As<T>(obj, true);
        }

        public static bool Is<T>(object obj) where T : class, IObject {
            return As<T>(obj, false) != null;
        }

        public virtual string Type {
            get { return GetType().Name; }
        }

        private int _Revision = 0;
        public int Revision {
            get { return _Revision; }
        }

        public virtual string RevInfo {
            get { return string.Format("({0})", _Revision); }
        }

        protected void AdvanceRevision() {
            _Revision += 1;
        }

        public override string LogPrefix {
            get {
                return string.Format("[{0}] {1} ", Type, RevInfo);
            }
        }

        public override string ToString() {
            return string.Format("[{0}: {1}]", Type, RevInfo);
        }

        public void CriticalFromProxy(string format, params object[] values) {
            string msg = GetLogMsg(format, values);
            if (DebugMode) {
                _ProxyDebugWriter.CriticalFrom(this, msg);
            } else {
                _ProxyDefaultWriter.CriticalFrom(this, msg);
            }
        }

        public void ErrorFromProxy(string format, params object[] values) {
            string msg = GetLogMsg(format, values);
            if (DebugMode) {
                _ProxyDebugWriter.ErrorFrom(this, msg);
            } else {
                _ProxyDefaultWriter.ErrorFrom(this, msg);
            }
        }

        public void InfoFromProxy(string format, params object[] values) {
            string msg = GetLogMsg(format, values);
            if (DebugMode) {
                _ProxyDebugWriter.LogWithPatternsFrom(this, LoggerConsts.INFO, DebugPatterns, msg);
            } else {
                _ProxyDefaultWriter.InfoFrom(this, msg);
            }
        }

        public void DebugFromProxy(string format, params object[] values) {
            if (LogDebug) {
                string msg = GetLogMsg(format, values);
                if (DebugMode) {
                    _ProxyDebugWriter.LogWithPatternsFrom(this, LoggerConsts.DEBUG, DebugPatterns, msg);
                } else {
                    _ProxyDefaultWriter.DebugFrom(this, msg);
                }
            }
        }

        public void CustomFromProxy(string kind, string format, params object[] values) {
            string msg = GetLogMsg(format, values);
            if (DebugMode) {
                _ProxyDebugWriter.LogWithPatternsFrom(this, kind, DebugPatterns, msg);
            } else {
                _ProxyDefaultWriter.CustomFrom(this, kind, msg);
            }
        }

        //SILP:BLOCK_OWNER()
        private List<WeakBlock> _Blocks = null;                       //__SILP__
                                                                      //__SILP__
        public void AddBlock(WeakBlock block) {                       //__SILP__
            if (_Blocks == null) {                                    //__SILP__
                _Blocks = new List<WeakBlock>();                      //__SILP__
            }                                                         //__SILP__
            if (!_Blocks.Contains(block)) {                           //__SILP__
                _Blocks.Add(block);                                   //__SILP__
            }                                                         //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        public void RemoveBlock(WeakBlock block) {                    //__SILP__
            if (_Blocks == null) {                                    //__SILP__
                return;                                               //__SILP__
            }                                                         //__SILP__
            int index = _Blocks.IndexOf(block);                       //__SILP__
            if (index >= 0) {                                         //__SILP__
                _Blocks.RemoveAt(index);                              //__SILP__
            }                                                         //__SILP__
        }                                                             //__SILP__
    }
}
