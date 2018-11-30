using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Stardust : DD_SuperStar
{
    // rotation
    private float rx, ry, rz;
    [Range(100f, 1000f)]
    public float rspd = 1f;

    // move
    private float dx, dy;
    public Vector3 dir;
    [Range(0f, 50f)]
    public float movespd = 1f;

    [Range(0.1f, 1f)]
    public float s1 = 0.5f, s2 = 0.75f, s3 = 1f; // s1 < s2 < s3
    public float _scale = 0.1f;
    private float myScale = 1f;

    public override void Init()
    {
        dir = Vector3.zero;
        angleRotate = Vector3.zero;
    }

    private void Start()
    {
        angleRotate.x = Random.value;
        angleRotate.y = Random.value;
        angleRotate.z = Random.value;

        dx = Mathf.Round(Random.Range(-1f, 1f) * 100f) / 100f;
        dy = Mathf.Round(Random.Range(-1f, 1f) * 100f) / 100f;
        dir.x = dx; dir.y = dy; dir.z = 0f;
        dir = dir.normalized;

        // 50, 30, 20
        int random = Random.Range(0, 10);
        switch (random)
        {
            default:
                break;

            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                myScale *= s1 * _scale;
                break;

            case 5:
            case 6:
            case 7:
                myScale *= s2 * _scale;
                break;

            case 8:
            case 9:
                myScale *= s3 * _scale;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(angleRotate * rspd * Time.deltaTime);
        dir.z = 0;
        transform.position += dir * movespd * Time.deltaTime;
        transform.localScale = Vector3.one * myScale;
    }

    private readonly static string LayerMaskWall = "Wall";
    private RaycastHit hit;
    public void WallOutReset()
    {
        if (Physics.Raycast(transform.position, -dir, out hit, 100f, 1<<LayerMask.NameToLayer(LayerMaskWall)))
        {
            transform.position = hit.point;
        }
    }
}
