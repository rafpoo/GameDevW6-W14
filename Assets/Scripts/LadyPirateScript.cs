using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyPirateScript : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        if (v != 0)
        {
            anim.SetBool("jalan", true);
        }
        else
        {
            anim.SetBool("jalan", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("lompat", true);
        }
    }

    public void ResetLompat()
    {
        anim.SetBool("lompat", false);
        Debug.Log("Triggered");
    }
}
