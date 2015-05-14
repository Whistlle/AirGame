using UnityEngine;
using System.Collections;
using System;

//public delegate void Action();

public class Util
{
   // public delegate void Action();

    public static IEnumerator DelayToInvoke(Action action, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        action();
    }

}
