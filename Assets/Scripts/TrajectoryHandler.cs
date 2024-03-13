using UnityEngine;
using Zenject;

public class TrajectoryHandler : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _linePositionsCount = 500;
    [SerializeField] private Transform _projectileSpawnPosition;

    [Inject] private LaunchSettings _launchSettings;

    public void DrawTrajectory()
    {
        Vector3 origin = _projectileSpawnPosition.position;
        Vector3 startVelocity = _projectileSpawnPosition.forward * _launchSettings.LauchForce;

        _lineRenderer.positionCount = _linePositionsCount;

        for (int i = 0; i < _linePositionsCount; i++)
        {
            _lineRenderer.SetPosition(i, origin);
            origin = Vector3.MoveTowards(origin, origin + startVelocity, _launchSettings.Speed);
            startVelocity.y += _launchSettings.Gravity * _launchSettings.TimeStep;
        }
    }
}
