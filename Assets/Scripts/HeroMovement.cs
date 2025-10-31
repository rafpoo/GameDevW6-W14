using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    Vector3 maju = Vector3.zero;
    float kecepatan = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            Vector3 targetDirection = new Vector3(h, 0f, v);
            targetDirection = Camera.main.transform.TransformDirection(targetDirection);
            targetDirection.y = 0.0f;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = targetRotation;

            Vector3 moveForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
            moveForward.y = 0;
            moveForward.Normalize();

            Vector3 moveRight = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);
            moveRight.y = 0;
            moveRight.Normalize();

            transform.position += (moveForward * v + moveRight * h) * Time.deltaTime * kecepatan;

            GetComponent<Animator>().SetBool("StatJalan", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("StatJalan", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Animator>().SetBool("StatAim", true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            GetComponent<Animator>().SetBool("StatAim", false);
        }
    }
}
