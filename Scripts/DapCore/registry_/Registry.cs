﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;

namespace angeldnd.dap {
    public static class RegistryConsts {
        public const string TypeRegistry = "Registry";
    }

    public sealed class Registry : DictContext<Env, IContext> {
        public override string Type {
            get { return RegistryConsts.TypeRegistry; }
        }

        public Registry(Env owner, string key) : base(owner, key) {
            Channels.Add(EnvConsts.ChannelTick);
        }
    }
}
