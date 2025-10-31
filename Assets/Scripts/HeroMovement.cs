using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 3f;
    private Animator anim;
    private Rigidbody rb;

    [Header("Foot IK Settings")]
    [Range(0, 1f)] public float distanceToGround = 0.1f;
    public LayerMask layerMask;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            Vector3 moveDir = new Vector3(h, 0f, v);
            moveDir = Camera.main.transform.TransformDirection(moveDir);
            moveDir.y = 0f;
            moveDir.Normalize();

            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.position += moveDir * movementSpeed * Time.deltaTime;

            anim.SetBool("StatJalan", true);
        }
        else
        {
            anim.SetBool("StatJalan", false);
        }

        // AIM
        anim.SetBool("StatAim", Input.GetMouseButton(1));
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim == null) return;

        AdjustFootIK(AvatarIKGoal.LeftFoot);
        AdjustFootIK(AvatarIKGoal.RightFoot);
    }

    private void AdjustFootIK(AvatarIKGoal foot)
    {
        anim.SetIKPositionWeight(foot, 1);
        anim.SetIKRotationWeight(foot, 1);

        Ray ray = new Ray(anim.GetIKPosition(foot) + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, distanceToGround + 1f, layerMask))
        {
            if (hit.transform.CompareTag("walkable"))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += distanceToGround;

                anim.SetIKPosition(foot, footPosition);
                anim.SetIKRotation(foot, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}
