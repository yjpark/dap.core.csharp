using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public static class PathConsts {
        public const char PathSeparator = '/';

        public const string PathSeparatorAsString = "/";

        public static string PathToKey(string path) {
            if (path == null) return string.Empty;

            return path.Replace(PathSeparator, DictConsts.KeySeparator);
        }

        public static string KeyToPath(string key) {
            if (key == null) return string.Empty;

            return key.Replace(DictConsts.KeySeparator, PathSeparator);
        }

        public static string Encode(string path, string relPath) {
            if (string.IsNullOrEmpty(path)) {
                if (relPath == null) return string.Empty;

                return relPath;
            } else {
                return string.Format("{0}{1}{2}", path, PathSeparator, relPath);
            }
        }
    }
}
