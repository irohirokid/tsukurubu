using System;
using UniRx;
using UnityEngine;

public class PlayerModel : ScriptableObject
{
    public IObservable<Vector3> PositionDeltaStream;
    public float Speed = 10.0f;
}
