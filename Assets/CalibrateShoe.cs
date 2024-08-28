using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CalibrateShoe : MonoBehaviour
{
    public Transform leftShoe;
    public Transform rightShoe;
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform cam;
    public Vector3 EulerOffset = new Vector3 (0f, 90f, 0f);

    // Update is called once per frame
    /*void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("Press");
            CalibrateShoes(leftFoot, leftShoe);
            CalibrateShoes(rightFoot, rightShoe);
        }
    }*/

    public void CalibrateBothShoes()
    {
        CalibrateShoes(leftFoot, leftShoe);
        CalibrateShoes(rightFoot, rightShoe);
    }

    void CalibrateShoes(Transform foot, Transform shoe)
    {
        shoe.parent = null;
        shoe.localPosition = Vector3.zero;
        shoe.localRotation = Quaternion.identity;

        Vector3 CameraRotation = Vector3.zero;
        CameraRotation.y = cam.transform.rotation.eulerAngles.y;
        shoe.Rotate(CameraRotation);
        shoe.parent = foot;
        //shoe.rotation.SetEulerAngles(CameraRotation);
        shoe.Rotate(EulerOffset);
        shoe.localPosition = Vector3.zero;

    }

    public void setFootSize(float size)
    {
        float i = 0.005724546f / 27.5f * size * 0.66666f;
        leftShoe.localScale = new Vector3(i, i, i);
    }
}
