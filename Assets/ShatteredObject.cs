using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<VaseSpawner>().vaseList.Add(gameObject);
    }
}
