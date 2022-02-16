using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransition
{
    void StartAnim(bool close, Action onEndTransition);
}
