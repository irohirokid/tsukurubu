using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    float speed = 10.0f;
    float horizontalInput;
    float verticalInput;

    ReactiveProperty<float> Magnitude = new ReactiveProperty<float>();

    void Start()
    {
        animator = GetComponent<Animator>();
        Magnitude.Select(x => x > 0).DistinctUntilChanged().Subscribe(x => animator.SetTrigger(x ? "TrRun" : "TrIdle"));
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        var v = Vector3.forward * Time.deltaTime * speed * verticalInput + Vector3.right * Time.deltaTime * speed * horizontalInput;
        Magnitude.Value = v.magnitude;

        if (Magnitude.Value > 0)
        {
            transform.SetPositionAndRotation(transform.position + v, Quaternion.LookRotation(v));
        }
    }
}
