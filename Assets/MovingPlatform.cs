using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    float speed = 10f;

    public bool up;
    public bool down;
    public bool left;
    public bool right;



    public void setTriggersActive(bool isActive)
    {
        MovingPlatformTrigger[] mpt = GetComponentsInChildren<MovingPlatformTrigger>();

        foreach (MovingPlatformTrigger t in mpt)
        {
            t.GetComponent<BoxCollider>().enabled = isActive;
            t.GetComponent<MeshRenderer>().enabled = isActive;
        }
    }
}
