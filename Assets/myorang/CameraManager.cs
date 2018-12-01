using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Utility.SingletonMonoBehaviour<CameraManager>
{
    public Camera _camera;
    public float prevSize, nowSize, endSize, minSize, maxSize;

    public override void Init()
    {
        prevSize = _camera.orthographicSize;
        nowSize = _camera.orthographicSize;
        endSize = _camera.orthographicSize;

        minSize = _camera.orthographicSize;
        maxSize = 15f;
    }

    public void SmoothCameraSizeUp(float rate)
    {

    }

}
