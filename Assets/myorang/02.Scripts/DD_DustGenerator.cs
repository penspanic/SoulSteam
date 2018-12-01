﻿using System.Collections;
using System.Collections.Generic;
using Common.StaticInfo;
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
    public string OriginEntityId;

    public void Start()
    {
        StaticInfoManager.Instance.Init("StaticData/Common/");
        switch (Type)
        {
            case GeneratorType.Undefined:
                break;

            case GeneratorType.Ingame:
                break;

            case GeneratorType.Fountain:
                StartCoroutine("CreateFountain");
                break;

            case GeneratorType.Area:
                break;
            default:
                break;
        }
    }

    WaitForSeconds interval;
    IEnumerator CreateFountain()
    {
        interval = new WaitForSeconds(createInterval);
        while (true)
        {
            Common.StaticData.EntityInfo entityInfo = StaticInfoManager.Instance.EntityInfos[OriginEntityId];
            Dust target = EntityManager.Instance.Create<Dust>(entityInfo);
            target.SetParameter(transform.position, new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));

            yield return interval;
        }
    }
    
    void CreateArea()
    {
        for (int i = 0; i < 100; ++i)
        {
            Common.StaticData.EntityInfo entityInfo = StaticInfoManager.Instance.EntityInfos[OriginEntityId];
            EntityManager.Instance.Create<Dust>(entityInfo);
        }
    }

    public void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, 36f * Time.deltaTime);
    }
}
