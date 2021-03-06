﻿using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public abstract class TableInDictAspect<TO, T> : TableInDict<TO, T>, IAspect
                                                        where TO : class, IDict, IContextElement
                                                        where T : class, IInTableElement {
        //SILP:IN_DICT_ASPECT_MIXIN_CONSTRUCTOR(TableInDictAspect)
        protected TableInDictAspect(TO owner, string key) : base(owner, key) {  //__SILP__
            _Context = owner == null ? null : owner.GetContext();               //__SILP__
            _Path = Env.GetAspectPath(this);                                    //__SILP__
            _Uri = Env.GetAspectUri(this);                                      //__SILP__
        }                                                                       //__SILP__

        //SILP: ASPECT_MIXIN()
        private readonly IContext _Context;                           //__SILP__
        public IContext GetContext() {                                //__SILP__
            return _Context;                                          //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        public IContext Context {                                     //__SILP__
            get { return _Context; }                                  //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        private readonly string _Path;                                //__SILP__
        public string Path {                                          //__SILP__
            get { return _Path; }                                     //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        private readonly string _Uri;                                 //__SILP__
        public override sealed string Uri {                           //__SILP__
            get {                                                     //__SILP__
                return _Uri;                                          //__SILP__
            }                                                         //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        private bool _Debugging = false;                              //__SILP__
        public bool Debugging {                                       //__SILP__
            get { return _Debugging; }                                //__SILP__
            set { _Debugging = value; }                               //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        public override sealed bool DebugMode {                       //__SILP__
            get { return _Debugging || base.DebugMode; }              //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        public override void OnAdded() {                              //__SILP__
            Env.Instance.Hooks._OnAspectAdded(this);                  //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        public override void OnRemoved() {                            //__SILP__
            Env.Instance.Hooks._OnAspectRemoved(this);                //__SILP__
        }                                                             //__SILP__
                                                                      //__SILP__
        protected override void AddSummaryFields(Data summary) {      //__SILP__
            base.AddSummaryFields(summary);                           //__SILP__
            summary.S(ContextConsts.SummaryPath, _Path)               //__SILP__
                   .B(ContextConsts.SummaryDebugging, _Debugging);    //__SILP__
        }                                                             //__SILP__
    }
}
