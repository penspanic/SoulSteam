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
    public float rotateSpeed = 100f;    // 회전 속도
    [Range(0f, 50f)]
    public float moveSpeedBase = 1f;    // 기본 속도
    [Range(0f, 1f)]
    public float moveSpeed1 = 1f;       // 1단계 속도
    [Range(0f, 1f)]
    public float moveSpeed2 = 0.75f;    // 2단계 속도
    [Range(0f, 1f)]
    public float moveSpeed3 = 0.5f;     // 3단계 속도

    [Range(0.1f, 1f)]
    public float scaleBase = 0.1f;      // 기본 크기
    [Range(0f, 1f)]
    public float scaleRate1 = 0.5f;     // 1단계 크기
    [Range(0f, 1f)]
    public float scaleRate2 = 0.75f;    // 2단계 크기
    [Range(0f, 1f)]
    public float scaleRate3 = 1f;       // 3단계 크기
}
