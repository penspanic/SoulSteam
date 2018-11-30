using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Entity;

public class DD_SuperStar : Entity
{
    public bool isMovable = true;

    protected Vector3 angleRotate = Vector3.zero;
    protected float scaleRate = 1f;
    protected float scaleBase = 1f;

    public virtual void Init() { }
}
