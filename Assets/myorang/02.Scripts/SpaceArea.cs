using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceArea : MonoBehaviour
{
    public List<DD_Stardust> stardusts = new List<DD_Stardust>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("who wall out");
        if (other.name.Contains("Stardust"))
        {
            Debug.Log("who wall out");
            other.SendMessage("WallOutReset");
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("who wall out");
    //    if (other.name.Contains("Stardust"))
    //    {
    //        Debug.Log("who wall out");
    //        other.SendMessage("WallOutReset");
    //    }
    //}
}
