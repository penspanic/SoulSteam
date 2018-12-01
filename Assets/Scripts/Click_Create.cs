using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Click_Create : MonoBehaviour
{

    public GameObject prefab_fire;
    public GameObject prefab_ice;
    public GameObject prefab_iron;
    public GameObject prefab_jupiter;
    public GameObject prefab_tree;

    int Num = 0;

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    // Update is called once per frame
    void Update()
    {
        Num=UnityEngine.Random.Range(1, 6);

        Vector3 createPos = new Vector3(transform.position.x, transform.position.y, -10);
        // 마우스 왼쪽 버튼 클릭
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {

            //스크린 좌표를 월드 좌표로 변환
            Vector3 wp = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            Vector2 touchPos = new Vector2(wp.x, wp.y);
            // 오브젝트 위치 갱신 
            createPos = touchPos;

            // 생성 
            if (Num == 1)
            {
                GameObject Inst_Fire = Instantiate(prefab_fire, createPos, Quaternion.identity) as GameObject;
            }
            if (Num == 2)
            {
                GameObject Inst_Ice = Instantiate(prefab_ice, createPos, Quaternion.identity) as GameObject;
            }
            if (Num == 3)
            {
                GameObject Inst_Iron = Instantiate(prefab_iron, createPos, Quaternion.identity) as GameObject;
            }
            if (Num == 4)
            {
                GameObject Inst_Jupiter = Instantiate(prefab_jupiter, createPos, Quaternion.identity) as GameObject;
            }
            if (Num == 5)
            {
                GameObject Inst_Tree = Instantiate(prefab_tree, createPos, Quaternion.identity) as GameObject;
            }
        }

}

}
