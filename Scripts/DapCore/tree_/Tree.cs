using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public interface IInTreeElement : IElement {
        /*
         * Constructor(TO owner, string path, Pass pass)
         */
        string Path { get; }
        string RevPath { get; }
    }

    public interface IInTreeElement<TO> : IElement<TO>, IInTreeElement
                                            where TO : ITree {
    }

    public interface ITree : IOwner {
        char Separator { get; }

        //Partial IDict
        int Count { get; }
        ICollection<string> Keys { get; }

        //Clear
        void Clear(Pass pass);
        void Clear();

        //Has
        bool Has(string path);

        //Path Helpers
        int GetDepth(string path);
        string GetName(string path);
        string GetParentPath(string path);
    }

    public interface ITree<T> : ITree
                                    where T : class, IInTreeElement {
        //Partial IDict<string, T>
        T this[string index] { get; }
        ICollection<T> Values { get; }
        IEnumerator<KeyValuePair<string, T>> GetEnumerator();
        bool TryGetValue(string path, out T element);

        //Generic Add
        T1 Add<T1>(Pass pass, string path, Pass elementPass) where T1 : class, T;
        T1 Add<T1>(Pass pass, string path) where T1 : class, T;
        T1 Add<T1>(string path, Pass elementPass) where T1 : class, T;
        T1 Add<T1>(string path) where T1 : class, T;

        //Add
        T Add(Pass pass, string path, Pass elementPass);
        T Add(Pass pass, string path);
        T Add(string path, Pass elementPass);
        T Add(string path);

        //Generic New With Factory
        T1 New<T1>(string type, Pass pass, string path, Pass elementPass) where T1 : class, T;
        T1 New<T1>(string type, Pass pass, string path) where T1 : class, T;
        T1 New<T1>(string type, string path, Pass elementPass) where T1 : class, T;
        T1 New<T1>(string type, string path) where T1 : class, T;

        //New With Factory
        T New(string type, Pass pass, string path, Pass elementPass);
        T New(string type, Pass pass, string path);
        T New(string type, string path, Pass elementPass);
        T New(string type, string path);

        //Generic Remove
        T1 Remove<T1>(Pass pass, string path, Pass elementPass) where T1 : class, T;
        T1 Remove<T1>(Pass pass, string path) where T1 : class, T;
        T1 Remove<T1>(string path, Pass elementPass) where T1 : class, T;
        T1 Remove<T1>(string path) where T1 : class, T;

        //Remove
        T Remove(Pass pass, string path, Pass elementPass);
        T Remove(Pass pass, string path);
        T Remove(string path, Pass elementPass);
        T Remove(string path);

        //Remove By Checker
        List<T> RemoveByChecker(Pass pass, Func<T, bool> checker);
        List<T> RemoveByChecker(Func<T, bool> checker);

        //Get
        T1 Get<T1>(string path) where T1 : class, T;
        T Get(string path);
        T1 GetOrAdd<T1>(string path) where T1 : class, T;
        T GetOrAdd(string path);

        //Generic Filter
        void Filter<T1>(string pattern, Action<T1> callback) where T1 : class, T;
        void All<T1>(Action<T1> callback) where T1 : class, T;
        List<T1> Filter<T1>(string pattern) where T1 : class, T;
        List<T1> All<T1>() where T1 : class, T;

        //Filter
        void Filter(string pattern, Action<T> callback);
        void All(Action<T> callback);
        List<T> Filter(string pattern);
        List<T> All();

        //Generic Relation
        T1 GetParent<T1>(string path) where T1 : class, T;
        void FilterChildren<T1>(string path, Action<T1> callback) where T1 : class, T;
        List<T1> GetChildren<T1>(string path) where T1 : class, T;
        T1 GetAncestor<T1>(string path) where T1 : class, T;
        T1 GetDescendant<T1>(string path, string relativePath) where T1 : class, T;
        void FilterDescendants<T1>(string path, Action<T1> callback) where T1 : class, T;
        List<T1> GetDescendants<T1>(string path) where T1 : class, T;

        //Relation
        T GetParent(string path);
        void FilterChildren(string path, Action<T> callback);
        List<T> GetChildren(string path);
        T GetAncestor(string path);
        T GetDescendant(string path, string relativePath);
        void FilterDescendants(string path, Action<T> callback);
        List<T> GetDescendants(string path);
    }

    public static class TreeConsts {
        public const char Separator = '.';
    }

    public abstract partial class Tree<T> : Object, ITree<T>
                                                where T : class, IInTreeElement {
        public virtual char Separator {
            get { return TreeConsts.Separator; }
        }

        private readonly Dictionary<string, T> _Elements = new Dictionary<string, T>();

        protected Tree(Pass pass) : base(pass) {
        }

        public bool Has(string path) {
            return _Elements.ContainsKey(path);
        }

        protected virtual void OnElementAdded(T element) {}
        protected virtual void OnElementRemoved(T element) {}
    }
}
