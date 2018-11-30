using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Stardust : MonoBehaviour
{
    // rotation
    public float rx, ry, rz;
    [Range(1f, 50f)]
    public float rspd = 1f;

    // move
    public float dx, dy;
    [Range(1f, 50f)]
    public float movespd = 1f;


    private void Start()
    {
        rx = Random.value;
        ry = Random.value;
        rz = Random.value;

        dx = Random.value;
        dy = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rx * rspd * Time.deltaTime, ry * rspd * Time.deltaTime, rz * rspd * Time.deltaTime);
        transform.Translate(dx * movespd * Time.deltaTime, dy * movespd * Time.deltaTime, 0f);
    }

    public void WallOutReset()
    {
        Vector2 rdir = new Vector2(dx, dy) * -1f;
        Ray2D ray = new Ray2D(transform.position, rdir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rdir, 30f, ~(1<<2));
        transform.position = hit.point;
        Debug.Log("Wall out reset" + hit.point + hit.collider.name);
    }
}
