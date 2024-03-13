using UnityEngine;
using Zenject;

public class ProjectileFactory
{
    private DiContainer _diContainer;

    private Projectile _projectilePrefab;
    private Transform _spawnProjectilePosition;

    public ProjectileFactory(DiContainer diContainer, Projectile projectile, Transform spawnProjectilePosition)
    {
        _diContainer = diContainer;
        _projectilePrefab = projectile;
        _spawnProjectilePosition = spawnProjectilePosition;
    }

    public void Create()
    {
        var projectile = _diContainer.InstantiatePrefabForComponent<Projectile>(_projectilePrefab, _spawnProjectilePosition);
        projectile.Initialize();
    }
}
