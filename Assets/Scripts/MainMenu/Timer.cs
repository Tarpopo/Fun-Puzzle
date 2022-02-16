using System;
using UnityEngine;

public class IntTimer : MonoBehaviour
{
   private int _time;
   private Action _timerEnd;
   private void Update() 
   {
       if (_time > 0)
       {
           _time--;
           if(_time<=0)_timerEnd?.Invoke();
       }
   }

   public void StartTimer(int time, Action action = null)
   {
       _timerEnd = action;
       _time = time;
   }

   public bool IsWork()
   {
       return _time > 0;
   }
}
