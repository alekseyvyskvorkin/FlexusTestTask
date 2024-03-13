using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private Action OnShoot;
    private Action<Vector2> OnMouseMove;

    [Inject] private Cannon _cannon;
    [Inject] private ProjectileFactory _projectileFactory;
    [Inject] private LaunchSettings _lauchSettings;
    [Inject] private CameraHandler _cameraHandler;

    private float _shootTime = 0;

    private Vector2 _previousTouchPosition;

    private void Start()
    {
        OnMouseMove += _cannon.RotateLauncher;
        OnShoot += _projectileFactory.Create;
        OnShoot += _cannon.Animate;
        OnShoot += _cameraHandler.Shake;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
            _previousTouchPosition = (Vector2)Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            var inputPosition = (Vector2)Input.mousePosition;
            if (Time.time > _shootTime)
            {
                OnShoot?.Invoke();
                _shootTime = Time.time + _lauchSettings.ShootRate;
            }
            if (inputPosition != _previousTouchPosition)
            {
                OnMouseMove?.Invoke((Vector2)Input.mousePosition - _previousTouchPosition);
                _previousTouchPosition = (Vector2)Input.mousePosition;
            }
        }
    }

    private void OnDisable()
    {
        OnMouseMove -= _cannon.RotateLauncher;
        OnShoot -= _projectileFactory.Create;
        OnShoot -= _cannon.Animate;
        OnShoot -= _cameraHandler.Shake;
    }
}
