using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
   [SerializeField] private string _link;
   
   public void OpenUrl()
   {
      if (_link == string.Empty) return;
      Application.OpenURL(_link);
   }
}
