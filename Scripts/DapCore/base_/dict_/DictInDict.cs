using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public abstract class DictInDict<TO, T> : DictElement<TO, T>, IInDictElement
                                                        where TO : class, IDict
                                                        where T : class, IInDictElement {
        //SILP: IN_DICT_MIXIN(DictInDict)
        protected DictInDict(TO owner, string key) : base(owner, key) {  //__SILP__
        }                                                                //__SILP__
                                                                         //__SILP__
        public IDict OwnerAsDict {                                       //__SILP__
            get { return Owner; }                                        //__SILP__
        }                                                                //__SILP__
    }
}


