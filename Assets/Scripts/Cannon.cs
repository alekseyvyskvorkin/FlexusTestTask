using UnityEngine;
using Zenject;
using System.Collections;

public class Cannon : MonoBehaviour
{
    public Transform ProjectileSpawnPosition => _projectileSpawnPosition;

    [SerializeField] private Transform _bottomPivot;
    [SerializeField] private Transform _cannonPivot;
    [SerializeField] private Transform _projectileSpawnPosition;

    [SerializeField] private Vector3 _cannonEndAnimationPosition;
    private Vector3 _startCannonPosition;

    private float _rotationX;
    private float _rotationY;

    private float _maxAngleX = 45f;
    private float _maxAngleY = 50f;    

    [Inject] private TrajectoryHandler _trajectoryHandler;
    [Inject] private LaunchSettings _launchSettings;

    private void Start()
    {
        _startCannonPosition = _cannonPivot.transform.localPosition;
    }

    public void RotateLauncher(Vector2 input)
    {
        _rotationX += -input.y;
        _rotationY += input.x;
        _rotationX = Mathf.Clamp(_rotationX, -_maxAngleX, _maxAngleX);
        _rotationY = Mathf.Clamp(_rotationY, -_maxAngleY, _maxAngleY);
        _cannonPivot.rotation = Quaternion.Euler(new Vector3(_rotationX, _rotationY, 0));
        _bottomPivot.rotation = Quaternion.Euler(new Vector3(0, _rotationY, 0));

        _trajectoryHandler.DrawTrajectory();
    }

    public void Animate()
    {
        StartCoroutine(CAnimate());
    }

    private IEnumerator CAnimate()
    {
        float backTime = _launchSettings.ShootRate / 2;
        float time = 0;
        while (time < backTime)
        {
            time += Time.deltaTime;
            _cannonPivot.transform.localPosition = Vector3.Lerp(_cannonPivot.transform.localPosition, _cannonEndAnimationPosition, time / backTime);
            yield return null;
        }
        time = 0;
        while (time < backTime)
        {
            time += Time.deltaTime;
            _cannonPivot.transform.localPosition = Vector3.Lerp(_cannonPivot.transform.localPosition, _startCannonPosition, time / backTime);
            yield return null;
        }
    }
}
