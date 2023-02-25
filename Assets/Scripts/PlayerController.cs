using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    float speed = 10.0f;
    float horizontalInput;
    float verticalInput;

    private float prevSpeed = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        var v = Vector3.forward * Time.deltaTime * speed * verticalInput + Vector3.right * Time.deltaTime * speed * horizontalInput;
        if (v.magnitude > 0)
        {
            transform.SetPositionAndRotation(transform.position + v, Quaternion.LookRotation(v));
        }

        if (prevSpeed == 0 && v.magnitude > 0)
        {
            animator.SetTrigger("TrRun");
        }
        else if (prevSpeed > 0 && v.magnitude == 0)
        {
            animator.SetTrigger("TrIdle");
        }
        prevSpeed = v.magnitude;
    }
}
