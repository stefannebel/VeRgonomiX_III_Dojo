using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string ColliderTag = "Destroyer";

    public UnityEvent onTouchEnter;
    public UnityEvent onTouchExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == ColliderTag) onTouchEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == ColliderTag) onTouchEnter.Invoke();
    }

    public void ChangeMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }
}
