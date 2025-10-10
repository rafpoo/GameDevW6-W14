using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyPirateScript : MonoBehaviour
{
    // MOVEMENTTTT
    Animator anim;
    float speed = 5f;
    float rotationSpeed = 10f;
    public float gravity = -9.8f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;

    // COMBO ATTACKKKK
    float comboAtk = 0f;
    float lastAtk = 0;
    private bool isHit = false;
    [SerializeField] private float hitCooldown = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

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

    public void TakeDamage()
    {
        if (isHit) return;
        anim.SetTrigger("hit");

        StartCoroutine(HitCooldown());
    }

    IEnumerator HitCooldown()
    {
        isHit = true;
        yield return new WaitForSeconds(hitCooldown);
        isHit = false;
    }

    public void ResetLompat()
    {
        anim.SetBool("lompat", false);
        Debug.Log("Triggered");
    }

    public void Movement()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 moveDir = Vector3.ProjectOnPlane(
            Camera.main.transform.forward * v + Camera.main.transform.right * h,
            Vector3.up
        ).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            anim.SetBool("jalan", true);

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            anim.SetBool("jalan", false);
        }

        controller.Move(moveDir * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // transform.position += Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up) * v * speed * Time.deltaTime;
        // transform.position += Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up) * h * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("lompat", true);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    }
}
