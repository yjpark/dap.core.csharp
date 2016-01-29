using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public abstract class DictElement<TO, T> : Dict<T>, IElement<TO>
                                                        where TO : class, IOwner
                                                        where T : class, IInDictElement {
        //SILP: ELEMENT_MIXIN(DictElement)
        protected DictElement(TO owner, string key) {                    //__SILP__
            _Owner = owner;                                              //__SILP__
            _Key = key;                                                  //__SILP__
        }                                                                //__SILP__
                                                                         //__SILP__
        protected DictElement(TO owner) {                                //__SILP__
            _Owner = owner;                                              //__SILP__
            _Key = string.Format("_{0}", Guid.NewGuid().GetHashCode());  //__SILP__
        }                                                                //__SILP__
                                                                         //__SILP__
        private readonly TO _Owner;                                      //__SILP__
        public TO Owner {                                                //__SILP__
            get { return _Owner; }                                       //__SILP__
        }                                                                //__SILP__
        public IOwner GetOwner() {                                       //__SILP__
            return _Owner;                                               //__SILP__
        }                                                                //__SILP__
                                                                         //__SILP__
        private readonly string _Key;                                    //__SILP__
        public string Key {                                              //__SILP__
            get { return _Key; }                                         //__SILP__
        }                                                                //__SILP__
                                                                         //__SILP__
        public override bool DebugMode {                                 //__SILP__
            get { return _Owner == null ? false : _Owner.DebugMode; }    //__SILP__
        }                                                                //__SILP__
                                                                         //__SILP__
        public virtual void OnAdded() {}                                 //__SILP__
        public virtual void OnRemoved() {}                               //__SILP__
    }
}
