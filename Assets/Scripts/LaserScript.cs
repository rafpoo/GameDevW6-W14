using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private LineRenderer lr;
    private Animator heroAnimator;

    // Start is called before the first frame update
    void Start()
    {
        heroAnimator = GameObject.Find("hero").GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool aiming = heroAnimator.GetBool("StatAim");
        lr.enabled = aiming;

        if (!aiming) return;

        lr.SetPosition(0, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            lr.SetPosition(1, transform.forward * 5000);
        }
    }
}
