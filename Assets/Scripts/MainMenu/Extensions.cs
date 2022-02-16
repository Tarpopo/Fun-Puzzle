using System;
using System.Collections.Generic;

public static class Extensions 
{
    public static void CycleStep<T> (this List<T> list, int direction)
    {
        int startIndex, lastIndex;
        
        if (Math.Sign(direction) > 0)
        {
            startIndex = list.Count - 1;
            lastIndex = 0;
        }
        else
        {
            startIndex = 0;
            lastIndex = list.Count - 1;
        }
        
        var elem=list[startIndex];
        list.Remove(elem);
        list.Insert(lastIndex,elem);
    }

    public static bool CheckMidIndex<T>(this List<T> list, int index)
    {
        var count=Math.Ceiling((double)(list.Count-1)/2);
        return index == (int)count;
    }
    public static int GetMidIndex<T>(this List<T> list)
    {
        var count=Math.Ceiling((double)(list.Count-1)/2);
        return (int)count;
    }
    public static int GetEdgeIndex<T>(this List<T> list, int direction)
    {
        direction = Math.Sign(direction);
        return direction > 0 ? list.Count - 1 : 0;
    }

    // public static bool IsEdgeIndex<T>(this List<T> list, int index)
    // {
    //     return index == list.Count - 1 || index == 0;
    // }
}
