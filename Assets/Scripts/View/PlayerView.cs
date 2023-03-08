using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    Animator animator;

    PlayerModel model;
    PlayerController controller;

    void Start()
    {
        animator = GetComponent<Animator>();

        model = ScriptableObject.CreateInstance<PlayerModel>();
        controller = new PlayerController(this.UpdateAsObservable().Select(inputVector), model);

        model.PositionDeltaStream.Where(d => d.magnitude > 0)
                                 .Subscribe(d => transform.SetPositionAndRotation(transform.position + d, Quaternion.LookRotation(d)));
        model.PositionDeltaStream.Select(d => d.magnitude > 0)
                                 .DistinctUntilChanged()
                                 .Subscribe(x => animator.SetTrigger(x ? "TrRun" : "TrIdle"));
    }

    Vector3 inputVector(Unit _)
    {
        return (Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal")) * Time.deltaTime;
    }
}
