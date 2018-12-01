using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Entity;

public class SpaceArea : MonoBehaviour
{
    public List<Dust> stardusts = new List<Dust>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Wall"))
            return;

        if (other.name.Contains("Dust"))
        {
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
