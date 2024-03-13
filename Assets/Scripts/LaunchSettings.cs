using System;
using UnityEngine;

[Serializable]
public class LaunchSettings 
{
    public float LauchForce => _launchForce;
    public float Gravity => _gravity;
    public float TimeStep => _timeStep;
    public float Speed => _speed;
    public float ShootRate => _shootRate;

    [SerializeField] private float _launchForce = 100f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _timeStep = 0.03f;
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private float _shootRate = 0.5f;

    public void ChangeForce(float value)
    {
        _launchForce = value;
    }
}
