using System.Collections.Generic;
using UnityEngine;

public class BlackBoard
{
    private Dictionary<string, object> data = new();

    public void Set<T>(string key, T value)
    {
        data.TryAdd(key, value);
    }

    public T Get<T>(string key)
    {
        //return (T)data[key];
        return data.TryGetValue(key, out var value) ? (T)value : default(T);
    }
}
