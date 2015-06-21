using System;
using System.Collections.Generic;

namespace angeldnd.dap {
    public struct ContextConsts {
        public const string TypeContext = "Context";

        public const string PathVars = "_vars";
        public const string PathProperties = "_properties";
        public const string PathChannels = "_channels";
        public const string PathHandlers = "_handlers";

        public const string SuffixHandlerAsync = "~";
        public const string SuffixChannelResponse = ">";

        public static string GetAsyncHandlerPath(string handlerPath) {
            return handlerPath + SuffixHandlerAsync;
        }

        public static string GetResponseChannelPath(string handlerPath) {
            return handlerPath + SuffixChannelResponse;
        }
    }

    public class Context : Entity {
        public override string Type {
            get { return ContextConsts.TypeContext; }
        }

        public readonly Vars Vars;
        public readonly Properties Properties;
        public readonly Channels Channels;
        public readonly Handlers Handlers;

        public Context() {
            Vars = Add<Vars>(ContextConsts.PathVars);
            Properties = Add<Properties>(ContextConsts.PathProperties);
            Channels = Add<Channels>(ContextConsts.PathChannels);
            Handlers = Add<Handlers>(ContextConsts.PathHandlers);
        }

        public Data Dump() {
            Data data = Properties.Encode();
            if (data != null) {
                return data.GetData(EntityConsts.KeyAspects);
            }
            return null;
        }

        public bool Load(Data data) {
            return Properties.DecodeAspects(data) > 0;
        }

        public bool FireEvent(string channelPath, Data evt) {
            return Channels.FireEvent(channelPath, evt);
        }

        public Channel GetChannel(string channelPath) {
            return Channels.GetChannel(channelPath);
        }

        public Channel AddChannel(string channelPath) {
            return Channels.AddChannel(channelPath);
        }

        public Data HandleRequest(string handlerPath, Data req) {
            return Handlers.HandleRequest(handlerPath, req);
        }

        public Handler GetHandler(string handlerPath) {
            return Handlers.GetHandler(handlerPath);
        }

        public Handler AddHandler(string handlerPath) {
            return Handlers.AddHandler(handlerPath);
        }

        public bool SetVarValue<T>(string varPath, T v) {
            if (Vars.HasVar<T>(varPath)) {
                return Vars.SetValue<T>(varPath, v);
            } else {
                return Vars.AddVar<T>(varPath, v) != null;
            }
        }

        public T GetVarValue<T>(string varPath, T defaultValue) {
            return Vars.GetValue<T>(varPath, defaultValue);
        }

        public bool SetVarValue<T>(string varsPath, string varPath, T v) {
            Vars vars = Get<Vars>(varsPath);
            if (vars == null) {
                vars = Add<Vars>(varsPath);
            }
            if (vars != null) {
                if (vars.HasVar<T>(varPath)) {
                    return vars.SetValue<T>(varPath, v);
                } else {
                    return vars.AddVar<T>(varPath, v) != null;
                }
            }
            return false;
        }

        public T GetVarValue<T>(string varsPath, string varPath, T defaultValue) {
            Vars vars = Get<Vars>(varsPath);
            if (vars != null) {
                return vars.GetValue<T>(varPath, defaultValue);
            }
            return defaultValue;
        }

        //SILP: CONTEXT_PROPERTIES_HELPER(Bool, bool)
        public BoolProperty AddBool(string path, Object pass, bool val) { //__SILP__
            return Properties.AddBool(path, pass, val);                   //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public BoolProperty AddBool(string path, bool val) {              //__SILP__
            return Properties.AddBool(path, val);                         //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public BoolProperty RemoveBool(string path) {                     //__SILP__
            return Properties.RemoveBool(path);                           //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool IsBool(string path) {                                 //__SILP__
            return Properties.IsBool(path);                               //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool GetBool(string path) {                                //__SILP__
            return Properties.GetBool(path);                              //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool GetBool(string path, bool defaultValue) {             //__SILP__
            return Properties.GetBool(path, defaultValue);                //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool SetBool(string path, bool value) {                    //__SILP__
            return Properties.SetBool(path, value);                       //__SILP__
        }                                                                 //__SILP__

        public bool SetBool(string path, Object pass, bool value) {       //__SILP__
            return Properties.SetBool(path, pass, value);                 //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        //SILP: CONTEXT_PROPERTIES_HELPER(Int, int)
        public IntProperty AddInt(string path, Object pass, int val) { //__SILP__
            return Properties.AddInt(path, pass, val);                 //__SILP__
        }                                                              //__SILP__
                                                                       //__SILP__
        public IntProperty AddInt(string path, int val) {              //__SILP__
            return Properties.AddInt(path, val);                       //__SILP__
        }                                                              //__SILP__
                                                                       //__SILP__
        public IntProperty RemoveInt(string path) {                    //__SILP__
            return Properties.RemoveInt(path);                         //__SILP__
        }                                                              //__SILP__
                                                                       //__SILP__
        public bool IsInt(string path) {                               //__SILP__
            return Properties.IsInt(path);                             //__SILP__
        }                                                              //__SILP__
                                                                       //__SILP__
        public int GetInt(string path) {                               //__SILP__
            return Properties.GetInt(path);                            //__SILP__
        }                                                              //__SILP__
                                                                       //__SILP__
        public int GetInt(string path, int defaultValue) {             //__SILP__
            return Properties.GetInt(path, defaultValue);              //__SILP__
        }                                                              //__SILP__
                                                                       //__SILP__
        public bool SetInt(string path, int value) {                   //__SILP__
            return Properties.SetInt(path, value);                     //__SILP__
        }                                                              //__SILP__
                                                                       //__SILP__
        public bool SetInt(string path, Object pass, int value) {      //__SILP__
            return Properties.SetInt(path, pass, value);               //__SILP__
        }                                                              //__SILP__
                                                                       //__SILP__
        //SILP: CONTEXT_PROPERTIES_HELPER(Long, long)
        public LongProperty AddLong(string path, Object pass, long val) { //__SILP__
            return Properties.AddLong(path, pass, val);                   //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public LongProperty AddLong(string path, long val) {              //__SILP__
            return Properties.AddLong(path, val);                         //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public LongProperty RemoveLong(string path) {                     //__SILP__
            return Properties.RemoveLong(path);                           //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool IsLong(string path) {                                 //__SILP__
            return Properties.IsLong(path);                               //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public long GetLong(string path) {                                //__SILP__
            return Properties.GetLong(path);                              //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public long GetLong(string path, long defaultValue) {             //__SILP__
            return Properties.GetLong(path, defaultValue);                //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool SetLong(string path, long value) {                    //__SILP__
            return Properties.SetLong(path, value);                       //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool SetLong(string path, Object pass, long value) {       //__SILP__
            return Properties.SetLong(path, pass, value);                 //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        //SILP: CONTEXT_PROPERTIES_HELPER(Float, float)
        public FloatProperty AddFloat(string path, Object pass, float val) { //__SILP__
            return Properties.AddFloat(path, pass, val);                     //__SILP__
        }                                                                    //__SILP__
                                                                             //__SILP__
        public FloatProperty AddFloat(string path, float val) {              //__SILP__
            return Properties.AddFloat(path, val);                           //__SILP__
        }                                                                    //__SILP__
                                                                             //__SILP__
        public FloatProperty RemoveFloat(string path) {                      //__SILP__
            return Properties.RemoveFloat(path);                             //__SILP__
        }                                                                    //__SILP__
                                                                             //__SILP__
        public bool IsFloat(string path) {                                   //__SILP__
            return Properties.IsFloat(path);                                 //__SILP__
        }                                                                    //__SILP__
                                                                             //__SILP__
        public float GetFloat(string path) {                                 //__SILP__
            return Properties.GetFloat(path);                                //__SILP__
        }                                                                    //__SILP__
                                                                             //__SILP__
        public float GetFloat(string path, float defaultValue) {             //__SILP__
            return Properties.GetFloat(path, defaultValue);                  //__SILP__
        }                                                                    //__SILP__
                                                                             //__SILP__
        public bool SetFloat(string path, float value) {                     //__SILP__
            return Properties.SetFloat(path, value);                         //__SILP__
        }                                                                    //__SILP__
                                                                             //__SILP__
        public bool SetFloat(string path, Object pass, float value) {        //__SILP__
            return Properties.SetFloat(path, pass, value);                   //__SILP__
        }                                                                    //__SILP__
                                                                             //__SILP__
        //SILP: CONTEXT_PROPERTIES_HELPER(Double, double)
        public DoubleProperty AddDouble(string path, Object pass, double val) { //__SILP__
            return Properties.AddDouble(path, pass, val);                       //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public DoubleProperty AddDouble(string path, double val) {              //__SILP__
            return Properties.AddDouble(path, val);                             //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public DoubleProperty RemoveDouble(string path) {                       //__SILP__
            return Properties.RemoveDouble(path);                               //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public bool IsDouble(string path) {                                     //__SILP__
            return Properties.IsDouble(path);                                   //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public double GetDouble(string path) {                                  //__SILP__
            return Properties.GetDouble(path);                                  //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public double GetDouble(string path, double defaultValue) {             //__SILP__
            return Properties.GetDouble(path, defaultValue);                    //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public bool SetDouble(string path, double value) {                      //__SILP__
            return Properties.SetDouble(path, value);                           //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public bool SetDouble(string path, Object pass, double value) {         //__SILP__
            return Properties.SetDouble(path, pass, value);                     //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        //SILP: CONTEXT_PROPERTIES_HELPER(String, string)
        public StringProperty AddString(string path, Object pass, string val) { //__SILP__
            return Properties.AddString(path, pass, val);                       //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public StringProperty AddString(string path, string val) {              //__SILP__
            return Properties.AddString(path, val);                             //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public StringProperty RemoveString(string path) {                       //__SILP__
            return Properties.RemoveString(path);                               //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public bool IsString(string path) {                                     //__SILP__
            return Properties.IsString(path);                                   //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public string GetString(string path) {                                  //__SILP__
            return Properties.GetString(path);                                  //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public string GetString(string path, string defaultValue) {             //__SILP__
            return Properties.GetString(path, defaultValue);                    //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public bool SetString(string path, string value) {                      //__SILP__
            return Properties.SetString(path, value);                           //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        public bool SetString(string path, Object pass, string value) {         //__SILP__
            return Properties.SetString(path, pass, value);                     //__SILP__
        }                                                                       //__SILP__
                                                                                //__SILP__
        //SILP: CONTEXT_PROPERTIES_HELPER(Data, Data)
        public DataProperty AddData(string path, Object pass, Data val) { //__SILP__
            return Properties.AddData(path, pass, val);                   //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public DataProperty AddData(string path, Data val) {              //__SILP__
            return Properties.AddData(path, val);                         //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public DataProperty RemoveData(string path) {                     //__SILP__
            return Properties.RemoveData(path);                           //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool IsData(string path) {                                 //__SILP__
            return Properties.IsData(path);                               //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public Data GetData(string path) {                                //__SILP__
            return Properties.GetData(path);                              //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public Data GetData(string path, Data defaultValue) {             //__SILP__
            return Properties.GetData(path, defaultValue);                //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool SetData(string path, Data value) {                    //__SILP__
            return Properties.SetData(path, value);                       //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
        public bool SetData(string path, Object pass, Data value) {       //__SILP__
            return Properties.SetData(path, pass, value);                 //__SILP__
        }                                                                 //__SILP__
                                                                          //__SILP__
    }
}
