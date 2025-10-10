using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float mouseSens = 3f;
    public float yMin = -20f;
    public float yMax = 60f;
    public float zoomSpeed = 2f;

    private float rotationX;
    private float rotationY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!target) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, yMin, yMax);

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, 2f, 8f);

        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = target.position + offset;

        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
