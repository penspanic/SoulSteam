using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Click_Create : MonoBehaviour
{

    public GameObject prefab_red;
    public GameObject prefab_blue;

    int Num = 1;
    bool Num_2 = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Num_2 == true)
        {
            if (Num > 0 && Num < 3)
            {
                Num++;
            }
            else
            {
                Num_2 = false;
                Num = Num - 1;
            }
        }
        if (Num_2 == false)
        {
            if (Num > 0 && Num < 3)
            {
                Num--;
            }
            else
            {
                Num_2 = true;
                Num = Num + 1;
            }
        }
        Vector3 createPos = new Vector3(transform.position.x, transform.position.y, -10);
        // 마우스 왼쪽 버튼 클릭
        if (Input.GetMouseButtonDown(0))
        {

            //스크린 좌표를 월드 좌표로 변환
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 touchPos = new Vector2(wp.x, wp.y);
            // 오브젝트 위치 갱신 
            createPos = touchPos;

            // 생성 
            if (Num == 1)
            {
                GameObject Inst_Red = Instantiate(prefab_red, createPos, Quaternion.identity) as GameObject;
            }
            if (Num == 2)
            {
                GameObject Inst_Blue = Instantiate(prefab_blue, createPos, Quaternion.identity) as GameObject;
            }
            
    }

}

}
