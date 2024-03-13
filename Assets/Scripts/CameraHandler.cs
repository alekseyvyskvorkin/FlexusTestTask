using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _shakeForce = 0.7f;

    private Transform _cameraTransform;
    private Vector3 _startPosition;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _startPosition = _cameraTransform.transform.localPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(CShake());
    }

    private IEnumerator CShake()
    {
        float time = _shakeDuration;
        while (time > 0)
        {
            _cameraTransform.transform.localPosition = _startPosition + Random.insideUnitSphere * _shakeForce;
            time -= Time.deltaTime;
            yield return null;
        }
        _cameraTransform.localPosition = _startPosition;
    }
}
