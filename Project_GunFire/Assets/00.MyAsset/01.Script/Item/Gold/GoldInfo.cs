using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldInfo : ItemInfo
{

    protected override float GravityScale()
    {
        return 0;
    }

    protected override Vector3 GravityDirection()
    {
        return Vector3.zero;
    }
}
