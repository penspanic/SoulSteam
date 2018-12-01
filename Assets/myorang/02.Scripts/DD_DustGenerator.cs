using System.Collections;
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
        CreateArea();
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
