using UnityEngine;
using System.Collections;
using Zenject;

public class Projectile : MonoBehaviour
{
    private const float MaxHalfSize = 0.5f;
    private const int EnoughtContactsCount = 2;

    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private SpriteRenderer _hitSprite;
    [SerializeField] private float _lifeTime = 3f;

    private Vector3 _velocity;

    private float _gravity;
    private float _speed;
    private float _timeStep;

    private int _contactCount;

    [Inject] private LaunchSettings _launchSettings;

    public void Initialize()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        _velocity =  transform.forward * _launchSettings.LauchForce;
        _gravity = _launchSettings.Gravity;
        _speed = _launchSettings.Speed;
        _timeStep = _launchSettings.TimeStep;
        transform.parent = null;
        StartCoroutine(CShootRoutine());
    }

    private IEnumerator CShootRoutine()
    {
        float time = 0;
        while (_contactCount < EnoughtContactsCount && time < _lifeTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _velocity.normalized, _speed);
            _velocity.y += _gravity * _timeStep;
            time += Time.deltaTime;

            if (Physics.Raycast(transform.position, _velocity, out var hit, MaxHalfSize))
            {
                _velocity = Vector3.Reflect(_velocity, hit.normal);
                transform.LookAt(transform.position + hit.normal);
                if (hit.collider.material != null)
                    _velocity *= hit.collider.material.bounciness;
                
                _contactCount++;
            }

            yield return null;
        }
        OnExplode();
    }

    private void OnExplode()
    {
        if (_contactCount == EnoughtContactsCount)
        {
            _hitSprite.gameObject.SetActive(true);
            _hitSprite.transform.parent = null;
        }
        
        _explosion.Play();
        _explosion.transform.parent = null;
        Destroy(gameObject);
    }
}
