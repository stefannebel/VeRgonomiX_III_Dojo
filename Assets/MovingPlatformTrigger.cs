using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour
{
    public Vector2 direction;
    public float speed;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "r")
        {
            Debug.Log(transform.name);
            transform.parent.Translate(new Vector3(direction.x,0,direction.y) * Time.deltaTime * speed);
        }
    }
    
}
