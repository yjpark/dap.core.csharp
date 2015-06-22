# DECLARE_LIST(name, var_name, cs_type, list_name) #
```C#
protected List<${cs_type}> ${list_name} = null;

public bool Add${name}(${cs_type} ${var_name}) {
    if (${list_name} == null) ${list_name} = new List<${cs_type}>();
    if (!${list_name}.Contains(${var_name})) {
        ${list_name}.Add(${var_name});
        return true;
    }
    return false;
}

public bool Remove${name}(${cs_type} ${var_name}) {
    if (${list_name} != null && ${list_name}.Contains(${var_name})) {
        ${list_name}.Remove(${var_name});
        return true;
    }
    return false;
}

``` 

# DATA_TYPE(type, cs_type) #
```
public bool Is${type}(string key) {
    DataType type = GetValueType(key);
    return type == DataType.${type};
}

public ${cs_type} Get${type}(string key) {
    if (_${type}Values != null && Is${type}(key)) {
        ${cs_type} result;
        if (_${type}Values.TryGetValue(key, out result)) {
            return result;
        }
    }
    return default(${cs_type}); 
}

public ${cs_type} Get${type}(string key, ${cs_type} defaultValue) {
    if (_${type}Values != null && Is${type}(key)) {
        ${cs_type} result;
        if (_${type}Values.TryGetValue(key, out result)) {
            return result;
        }
    }
    return defaultValue;
}

public bool Set${type}(string key, ${cs_type} val) {
    if (!_ValueTypes.ContainsKey(key)) {
        _ValueTypes[key] = DataType.${type};
        if (_${type}Values == null) {
            _${type}Values = new Dictionary<string, ${cs_type}>();
        }
        _${type}Values[key] = val;
        return true;
    }
    return false;
}

``` 

# DATA_QUICK_SETTER(name, type, cs_type) #
```
public Data ${name}(string key, ${cs_type} val) {
    Set${type}(key, val);
    return this;
}

```

# ASPECT_MIXIN() #
```
private Entity _Entity = null;
public Entity Entity {
    get { return _Entity; }
}

private string _Path = null;
public string Path {
    get { return _Path; }
}

private bool _Inited = false;
public bool Inited {
    get { return _Inited; }
}

public bool Init(Entity entity, string path) {
    if (_Inited) return false;
    if (entity == null || string.IsNullOrEmpty(path)) return false;

    _Entity = entity;
    _Path = path;
    _Inited = true;
    return true;
}

```

# ASPECT_EVENTS_MIXIN() #
```
public virtual void OnAdded() {}
public virtual void OnRemoved() {}
```

# ASPECT_ENCODE_DECODE_MIXIN() #
```
protected virtual bool DoEncode(Data data) {
    return true;
}

protected virtual bool DoDecode(Data data) {
    return true;
}

```

# DAPOBJECT_MIXIN() #
```
public virtual string Type {
    get { return null; }
}

public Data Encode() {
    if (!string.IsNullOrEmpty(Type)) {
        Data data = new Data();
        if (data.SetString(DapObjectConsts.KeyType, Type)) {
            if (DoEncode(data)) {
                return data;
            }
        }
    }
    if (LogDebug) Debug("Not Encodable!");
    return null;
}

public bool Decode(Data data) {
    string type = data.GetString(DapObjectConsts.KeyType);
    if (type == Type) {
        return DoDecode(data);
    }
    return false;
}

```

# ENTITY_LOG_MIXIN() #
```
private DebugLogger _DebugLogger = DebugLogger.Instance;

private bool _DebugMode = false;
public bool DebugMode {
    get { return _DebugMode; }
    set {
        _DebugMode = true;
    }
}

private string[] _DebugPatterns = {""};
public virtual string[] DebugPatterns {
    get { return _DebugPatterns; }
    set {
        _DebugPatterns = value;
    }
}

public virtual bool LogDebug {
    get { return _DebugMode || Log.LogDebug; }
}

public virtual string GetLogPrefix() {
    return string.Format("[{0}] ", GetType().Name);
}

public void Critical(string format, params object[] values) {
    if (DebugMode) {
        _DebugLogger.Critical(
            _DebugLogger.GetLogHint() + GetLogPrefix() + string.Format(format, values));
    } else {
        Log.Critical(GetLogPrefix() + string.Format(format, values));
    }
}

public void Error(string format, params object[] values) {
    if (DebugMode) {
        _DebugLogger.Error(
            _DebugLogger.GetLogHint() + GetLogPrefix() + string.Format(format, values));
    } else {
        Log.Error(GetLogPrefix() + string.Format(format, values));
    }
}

public void Info(string format, params object[] values) {
    if (DebugMode) {
        _DebugLogger.LogWithPatterns(LoggerConsts.INFO, DebugPatterns,
                _DebugLogger.GetLogHint() + GetLogPrefix() + string.Format(format, values));
    } else {
        Log.Info(GetLogPrefix() + string.Format(format, values));
    }
}

public void Debug(string format, params object[] values) {
    if (DebugMode) {
        _DebugLogger.LogWithPatterns(LoggerConsts.DEBUG, DebugPatterns,
                _DebugLogger.GetLogHint() + GetLogPrefix() + string.Format(format, values));
    } else {
        Log.Debug(GetLogPrefix() + string.Format(format, values));
    }
}

```

# ASPECT_LOG_MIXIN() #
```C#
public virtual string GetLogPrefix() {
    if (_Entity != null) {
        return string.Format("{0}[{1}] [{2}]", _Entity.GetLogPrefix(), GetType().Name, Path);
    } else {
        return string.Format("[] [{0}] [{1}]", GetType().Name, Path);
    }
}
```

# ACCESSOR_LOG_MIXIN() #
```C#
private DebugLogger _DebugLogger = DebugLogger.Instance;

public bool DebugMode {
    get { return Entity != null && Entity.DebugMode; }
}

public bool LogDebug {
    get { return (Entity != null && Entity.LogDebug) || Log.LogDebug; }
}

public void Critical(string format, params object[] values) {
    if (DebugMode) {
        _DebugLogger.Critical(
                _DebugLogger.GetLogHint() + GetLogPrefix() + string.Format(format, values));
    } else {
        Log.Critical(GetLogPrefix() + string.Format(format, values));
    }
}

public void Error(string format, params object[] values) {
    if (DebugMode) {
        _DebugLogger.Error(
                _DebugLogger.GetLogHint() + GetLogPrefix() + string.Format(format, values));
    } else {
        Log.Error(GetLogPrefix() + string.Format(format, values));
    }
}

public void Info(string format, params object[] values) {
    if (DebugMode) {
        _DebugLogger.LogWithPatterns(LoggerConsts.INFO, Entity.DebugPatterns,
                _DebugLogger.GetLogHint() + GetLogPrefix() + string.Format(format, values));
    } else {
        Log.Info(GetLogPrefix() + string.Format(format, values));
    }
}

public void Debug(string format, params object[] values) {
    if (DebugMode) {
        _DebugLogger.LogWithPatterns(LoggerConsts.DEBUG, Entity.DebugPatterns,
                _DebugLogger.GetLogHint() + GetLogPrefix() + string.Format(format, values));
    } else {
        Log.Debug(GetLogPrefix() + string.Format(format, values));
    }
}

```

# ENTITY_ASPECT_LOG_MIXIN() #
```
```
