using System;
using System.Collections.Generic;
using System.Linq;

namespace angeldnd.dap {
    public abstract class DictProperty<T> : DictInBothAspect<IProperties, T>, IDictProperties, IProperty
                                                where T : class, IProperty {
        private bool DoEncode(Data data) {
            /*
            foreach (T element in _Elements.Values) {
                Data subData = element.Encode();
                if (subData != null) {
                    if (!data.SetData(element.Key, subData)) {
                        return false;
                    }
                } else {
                    return false;
                }
            }
            */
            return true;
        }

        private bool DoDecode(Data data) {
            /*
            RemoveByChecker<T>(Pass, (T element) => true);
            if (_Elements.Count > 0) {
                Error("Orghan Elements Found: {0}", _Elements.Count);
            }
            foreach (var key in data.Keys) {
                if (key == ObjectConsts.KeyType) continue;

                Data subData = data.GetData(key);
                if (subData == null) {
                    Log.Error("Invalid Elements Data: {0} -> {1}", key, data.GetValue(key));
                    return false;
                }
                Property prop = SpecHelper.AddWithSpec(this, key, Pass, false, subData);
                if (!(prop is T)) {
                    Log.Error("Type Mismatched: {0}: {1} -> {2}", key, typeof(T).Name, prop.GetType().FullName);
                    return false;
                }
            }
            */
            return true;
        }

        //SILP: GROUP_PROPERTY_MIXIN(DictProperty)
        public DictProperty(IProperties owner, string key) : base(owner, key) {           //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        public DictProperty(IProperties owner, int index) : base(owner, index) {          //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        //IProperty                                                                       //__SILP__
        public Data Encode() {                                                            //__SILP__
            if (!string.IsNullOrEmpty(Type)) {                                            //__SILP__
                Data data = new Data();                                                   //__SILP__
                if (data.SetString(ObjectConsts.KeyType, Type)) {                         //__SILP__
                    if (DoEncode(data)) {                                                 //__SILP__
                        return data;                                                      //__SILP__
                    }                                                                     //__SILP__
                }                                                                         //__SILP__
            }                                                                             //__SILP__
            if (LogDebug) Debug("Not Encodable!");                                        //__SILP__
            return null;                                                                  //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        public bool Decode(Data data) {                                                   //__SILP__
            string type = data.GetString(ObjectConsts.KeyType);                           //__SILP__
            if (type == Type) {                                                           //__SILP__
                return DoDecode(data);                                                    //__SILP__
            } else {                                                                      //__SILP__
                Error("Type Mismatched: {0}, {1}", Type, type);                           //__SILP__
            }                                                                             //__SILP__
            return false;                                                                 //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        private void FireOnChanged() {                                                    //__SILP__
            WeakListHelper.Notify(_VarWatchers, (IVarWatcher watcher) => {                //__SILP__
                watcher.OnChanged(this);                                                  //__SILP__
            });                                                                           //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        //IVar                                                                            //__SILP__
        public object GetValue() {                                                        //__SILP__
            return null;                                                                  //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        private WeakList<IVarWatcher> _VarWatchers = null;                                //__SILP__
                                                                                          //__SILP__
        public int VarWatcherCount {                                                      //__SILP__
            get { return WeakListHelper.Count(_VarWatchers); }                            //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        public bool AddVarWatcher(IVarWatcher watcher) {                                  //__SILP__
            if (WeakListHelper.Add(ref _VarWatchers, watcher)){                           //__SILP__
                return true;                                                              //__SILP__
            }                                                                             //__SILP__
            return false;                                                                 //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        public bool RemoveVarWatcher(IVarWatcher watcher) {                               //__SILP__
            if (WeakListHelper.Remove(_VarWatchers, watcher)) {                           //__SILP__
                return true;                                                              //__SILP__
            }                                                                             //__SILP__
            return false;                                                                 //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        public BlockVarWatcher AddVarWatcher(IBlockOwner owner,                           //__SILP__
                                             Action<IVar> _watcher) {                     //__SILP__
            BlockVarWatcher watcher = new BlockVarWatcher(owner, _watcher);               //__SILP__
            if (AddVarWatcher(watcher)) {                                                 //__SILP__
                return watcher;                                                           //__SILP__
            }                                                                             //__SILP__
            return null;                                                                  //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        public int ValueCheckerCount {                                                    //__SILP__
            get { return 0; }                                                             //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        public void AllValueCheckers<T1>(Action<T1> callback) where T1 : IValueChecker {  //__SILP__
        }                                                                                 //__SILP__
                                                                                          //__SILP__
        public int ValueWatcherCount {                                                    //__SILP__
            get { return 0; }                                                             //__SILP__
        }                                                                                 //__SILP__
    }
}
