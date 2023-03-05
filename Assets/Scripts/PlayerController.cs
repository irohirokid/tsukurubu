using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
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

        var inputVectorStream = this.UpdateAsObservable().Select(inputVector);
        inputVectorStream.Where(v => v.magnitude > 0)
                         .Subscribe(v => transform.SetPositionAndRotation(transform.position + v, Quaternion.LookRotation(v)));
        inputVectorStream.Select(v => v.magnitude > 0)
                         .DistinctUntilChanged()
                         .Subscribe(x => animator.SetTrigger(x ? "TrRun" : "TrIdle"));
    }

    Vector3 inputVector(Unit _)
    {
        return Vector3.forward * Time.deltaTime * speed * Input.GetAxis("Vertical") + Vector3.right * Time.deltaTime * speed * Input.GetAxis("Horizontal");
    }
}
