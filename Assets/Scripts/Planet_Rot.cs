using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Rot : MonoBehaviour {
    

    public float a;
    int num1;
    float rotate_timer;
    public float rotime = 0.1f;
    // Use this for initialization
    void Start () {
        num1 = UnityEngine.Random.Range(0, 2);
        if (num1 == 0)
        {
            a = a * 1;
        }
        if (num1 == 1)
        {
            a = a * -1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        rotate_timer += Time.deltaTime;
        if (rotate_timer > rotime)
        {
            transform.Rotate(0, 0, a);
                rotate_timer = 0;
        }
    }
}
