using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Utility.SingletonMonoBehaviour<CameraManager>
{
    public Camera _camera;

    public override void Init()
    {
        prevSize = _camera.orthographicSize;
        nowSize = _camera.orthographicSize;
        endSize = _camera.orthographicSize;
    }

    public float prevSize, nowSize, endSize;
}
