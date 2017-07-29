using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A class to allow serializing List<List<T>>"
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class ListWrapper {

    [SerializeField]
    public List<Vector3> List;

    /// <summary>
    /// Constructor to setup internal list.
    /// </summary>
    /// <param name="list"></param>
    public ListWrapper(List<Vector3> list)
    {
        this.List = list;
    }

    /// <summary>
    /// Constructor with no internal list.
    /// </summary>
    public ListWrapper()
    {
        this.List = new List<Vector3>();
    }

    /// <summary>
    /// Pass-through for the add function.
    /// </summary>
    /// <param name="obj"></param>
    public void Add(Vector3 obj)
    {
        this.List.Add(obj);
    }
}
