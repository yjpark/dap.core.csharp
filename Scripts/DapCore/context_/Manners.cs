using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public sealed class Manners : DictAspect<IContext, Manner> {
        public Manners(IContext owner) : base(owner) {
        }
    }
}