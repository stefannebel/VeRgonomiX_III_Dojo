using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.Return)) transform.localPosition = Vector3.zero;
    }
}
