using System;
using UniRx;
using UnityEngine;

public class PlayerController
{
    IObservable<Vector3> inputVectorStream;
    PlayerModel model;

    public PlayerController(IObservable<Vector3> _inputVectorStream, PlayerModel _model)
    {
        inputVectorStream = _inputVectorStream;
        model = _model;
        model.PositionDeltaStream = inputVectorStream.Select(v => v * model.Speed);
    }
}
