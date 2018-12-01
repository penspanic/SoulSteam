using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Testment : MonoBehaviour
{
    public static DD_Testment Testment = null;
    public bool isTest = false;

    public void Awake()
    {
        if (Testment == null)
            Testment = this;
        else
            Debug.LogError("테스트기 1개 이상 존재");
    }

    [Header("별가루")]
    [Range(100f, 2000f)]
    public float dust_rotateSpeed = 100f;    // 회전 속도
    [Range(0f, 50f)]
    public float dust_moveSpeedBase = 1f;    // 기본 속도
    [Range(0f, 1f)]
    public float[] dust_moveSpeedLevelRate = { 1f, 0.75f, 0.5f };

    [Range(0.1f, 1f)]
    public float dust_scaleBase = 0.1f;      // 기본 크기
    [Range(0f, 1f)]
    public float[] dust_scaleLevelRate = { 0.5f, 0.75f, 1f };
}
