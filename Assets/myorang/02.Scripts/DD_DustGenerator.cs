﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Entity;

public class DD_DustGenerator : MonoBehaviour
{
    public enum GeneratorType
    {
        Undefined = 0,
        Ingame,
        Fountain,
        Area
    }
    public GeneratorType Type = GeneratorType.Undefined;
    [Range(0.1f, 1f)]
    public float createInterval = 0.1f;
    public Entity originPrefab;

    public void Start()
    {
        if (originPrefab == null)
            return;
    }

    WaitForSeconds interval;
    IEnumerator CreateFountain()
    {
        interval = new WaitForSeconds(createInterval);
        while (true)
        {

            yield return interval;
        }
    }

    public void Update()
    {
        switch (Type)
        {
            case GeneratorType.Undefined:
                break;

            case GeneratorType.Ingame:
                break;

            case GeneratorType.Fountain:
                break;

            case GeneratorType.Area:
                break;

            default:
                break;
        }
    }
}
