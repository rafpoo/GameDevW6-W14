using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyPirateScript : MonoBehaviour
{
    Animator anim;

    float comboAtk = 0f;
    float lastAtk = 0;
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

        // serang
        if (Input.GetButtonDown("Fire1"))
        {
            lastAtk = Time.time;
            comboAtk++;
            comboAtk = Mathf.Clamp(comboAtk, 0, 3);
            GetComponent<Animator>().SetFloat("serang", comboAtk);
        }

        // reset serang
        if (Time.time - lastAtk >= 1f)
        {
            comboAtk = 0;
            Debug.Log("masuk " + comboAtk);
            GetComponent<Animator>().SetFloat("serang", comboAtk);
        }
    }

    public void ResetLompat()
    {
        anim.SetBool("lompat", false);
        Debug.Log("Triggered");
    }
}
