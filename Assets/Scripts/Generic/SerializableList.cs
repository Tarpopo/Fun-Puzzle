using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableList<T> 
{
    public List<T> list;

    public SerializableList()
    {
        list = new List<T>();
    }

    public SerializableList(List<T> list)
    {
        this.list = list;
    }
}
