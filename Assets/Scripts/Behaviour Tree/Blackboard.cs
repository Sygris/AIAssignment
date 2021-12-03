using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds variables and values relevant to an AI agent
/// </summary>
public class Blackboard
{
    private Dictionary<string, object> _blackboardData = new Dictionary<string, object>();

#if UNITY_EDITOR
    /// <summary>
    /// Function that shows what data the blackboard has stored
    /// </summary>
    public void Show()
    {
        foreach (KeyValuePair<string, object> entry in _blackboardData)
        {
            Debug.Log(string.Format("{0} {1}", entry.Key, entry.Value));
        }
    }
#endif

    /// <summary>
    /// Adds data to the blackboard
    /// </summary>
    /// <param name="key">String that represents the "name"</param>
    /// <param name="value">The object</param>
    public void AddData(string key, object value)
    {
        if (!ContainsKey(key)) _blackboardData.Add(key, value);
        else Debug.LogWarning("[BLACKBOARD] Key already exists! Use ModifyData() instead.");
    }

    /// <summary>
    /// Removes data from the blackboard
    /// </summary>
    /// <param name="key">String that represents the "name"</param>
    public void RemoveData(string key)
    {
        if (!ContainsKey(key)) _blackboardData.Remove(key);
        else Debug.LogWarning("[BLACKBOARD] The key " + "<color=yellow>" + key + "</color> does not exists");
    }

    /// <summary>
    /// Modifies the value of a key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void ModifyData(string key, object value)
    {
        _blackboardData[key] = value;
    }


    /// <summary>
    /// Get value from a key
    /// </summary>
    /// <param name="key">Key of the value to be returned</param>
    /// <returns>Returns the value of the key</returns>
    public object GetData(string key)
    {
        if (_blackboardData.TryGetValue(key, out object tmp)) 
            return tmp;

        Debug.LogWarning("[BLACKBOARD] Data not found");
        return tmp;
    }

    /// <summary>
    /// Checks if key already exists in the blackboard
    /// </summary>
    /// <param name="key">Key to be checked</param>
    /// <returns>If key exists it will return the object if not it will be null</returns>
    private bool ContainsKey(string key)
    {
        return _blackboardData.TryGetValue(key, out _);
    }
}
