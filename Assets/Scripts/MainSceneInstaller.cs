using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private LaunchSettings _launchSettings;
    [SerializeField] private Cannon _cannon;
    [SerializeField] private TrajectoryHandler _trajectoryHandler;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private CameraHandler _cameraHandler;
    [SerializeField] private UIService _uIService;

    public override void InstallBindings()
    {
        Container.Bind<LaunchSettings>().FromInstance(_launchSettings).AsSingle().NonLazy();
        Container.Bind<Cannon>().FromInstance(_cannon).AsSingle().NonLazy();
        Container.Bind<TrajectoryHandler>().FromInstance(_trajectoryHandler).AsSingle().NonLazy();
        Container.Bind<InputHandler>().FromInstance(_inputHandler).AsSingle().NonLazy();
        Container.Bind<CameraHandler>().FromInstance(_cameraHandler).AsSingle().NonLazy();
        Container.Bind<UIService>().FromInstance(_uIService).AsSingle().NonLazy();

        var projectileFactory = new ProjectileFactory(Container, _projectilePrefab, _cannon.ProjectileSpawnPosition);
        Container.Bind<ProjectileFactory>().FromInstance(projectileFactory);
    }
}
