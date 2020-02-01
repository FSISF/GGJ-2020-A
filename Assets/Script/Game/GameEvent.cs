using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CheckOjectLevel();

public class GameEvent
{
    public static event CheckOjectLevel CheckObjectLevel;
    public static void DoCheckObjectLevel()
    {
        CheckObjectLevel();
    }
}