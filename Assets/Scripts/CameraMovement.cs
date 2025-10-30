using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float turnSpeed = 4.0f;
    private Transform posHero;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        posHero = GameObject.Find("hero").transform.Find("char_point_cam").transform;
        offset = new Vector3(posHero.localPosition.x, posHero.localPosition.y, posHero.localPosition.z - 3.2f);
    }

    // Update is called once per frame
    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = posHero.position + offset;
        transform.LookAt(posHero.position);
    }
}
