using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private Animator anim;

    // membuat IK foot
    [Range(0, 1f)] public float distanceToGround;
    public LayerMask layerMask;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetBool("StatJalan", !anim.GetBool("StatJalan"));
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // left foot
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);

        RaycastHit hit;
        Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out hit, distanceToGround + 1f, layerMask))
        {
            if (hit.transform.tag == "walkable")
            {
                Vector3 footPosition = hit.point;
                footPosition.y += distanceToGround;
                anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                // anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}
