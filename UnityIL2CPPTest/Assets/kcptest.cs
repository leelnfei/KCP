﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TestKCP;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System;

public class kcptest : MonoBehaviour
{
    static kcptest instance;
    public TextMeshProUGUI kcpr1;
    public TextMeshProUGUI kcpr2;
    private void Awake()
    {
        instance = this;   
    }
    
    public void StartTest()
    {
        Program.Main(null);
    }

    private void Update()
    {
        while (actions.TryDequeue(out var action))
        {
            action?.Invoke();
        }
    }

    ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();

    public static void Log1(string s)
    {
        instance.actions.Enqueue(() => { instance.kcpr1.text = s; });
    }

    public static void Log2(string s)
    {
        instance.actions.Enqueue(() => { instance.kcpr2.text = s; });
    }
}
