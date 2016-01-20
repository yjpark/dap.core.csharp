using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public interface IProperties : IVars {
    }

    public interface ITreeProperties<T> : IProperties, ITree<T>
                                            where T : class, IProperty {
        /*
        IProperty AddProperty(Pass pass, string path, Pass propertyPass, bool open, Data data);
        IProperty AddProperty(string path, Pass propertyPass, bool open, Data data);
        */
    }

    public interface ITableProperties<T> : IProperties, ITable<T>
                                            where T : class, IProperty {
    }

    public static class PropertiesConsts {
        public const string TypeBoolProperty = "Bool";
        public const string TypeIntProperty = "Int";
        public const string TypeLongProperty = "Long";
        public const string TypeFloatProperty = "Float";
        public const string TypeDoubleProperty = "Double";
        public const string TypeStringProperty = "String";
        public const string TypeDataProperty = "Data";

        public const string KeyValue = "v";
    }

    public sealed class Properties : TreeSection<IContext, IProperty>, ITreeProperties<IProperty> {
        public Properties(IContext owner, Pass pass) : base(owner, pass) {
        }
    }
}
