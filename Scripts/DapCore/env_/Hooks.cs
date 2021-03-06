using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public sealed class Hooks : DictAspect<IContext, Hook> {
        public Hooks(IContext owner, string key) : base(owner, key) {
        }

        // Should only be called from IContext.OnAdded();
        public void _OnContextAdded(IContext context) {
            ForEach((Hook hook) => {
                hook._OnContextAdded(context);
            });
        }

        // Should only be called from IContext.OnRemoved();
        public void _OnContextRemoved(IContext context) {
            ForEach((Hook hook) => {
                hook._OnContextRemoved(context);
            });
        }

        // Should only be called from IAspect.OnAdded();
        public void _OnAspectAdded(IAspect aspect) {
            ForEach((Hook hook) => {
                hook._OnAspectAdded(aspect);
            });
        }

        // Should only be called from IAspect.OnRemoved();
        public void _OnAspectRemoved(IAspect aspect) {
            ForEach((Hook hook) => {
                hook._OnAspectRemoved(aspect);
            });
        }

        public override void OnAdded() {
            //Do Nothing.
        }

        public override void OnRemoved() {
            //Do Nothing.
        }
    }
}
