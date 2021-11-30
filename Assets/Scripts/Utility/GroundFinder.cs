using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinder 
{
    private static LayerMask groundLayer = LayerMask.GetMask(Common.GroundLayerName);

    public static Vector3 NextPlatformBeneath(Vector3 currentPosition)
    {
        var overlapList = Physics.OverlapBox(new Vector3(currentPosition.x, currentPosition.y/2, 0), new Vector3(0.15f, currentPosition.y/2, 0), new Quaternion(), groundLayer.value);
        if (overlapList.Length > 0)
        {
            return overlapList[0].transform.position;
        }
        return Vector3.negativeInfinity;
    }
}
