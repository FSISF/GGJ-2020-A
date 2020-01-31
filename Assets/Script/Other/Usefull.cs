using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Usefull
{
    /// <summary>
    /// 計時器
    /// </summary>
    /// <param name="second"></param>
    /// <param name="complete"></param>
    public static void Timer(double second, Action complete)
    {
        Observable.Timer(TimeSpan.FromSeconds(second)).DoOnCompleted(() =>
        {
            if (complete != null)
            {
                complete();
            }
        }).Subscribe();
    }

    /// <summary>
    /// 協程
    /// </summary>
    /// <param name="enumerator"></param>
    /// <returns></returns>
    public static IDisposable Coroutine(Func<IEnumerator> enumerator)
    {
        return Observable.FromCoroutine(enumerator).Subscribe();
    }
}