using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootableTank : Tank
{
    [SerializeField] private string _projectileTag; 
    [SerializeField] private Transform _shootPosition;
    [SerializeField] protected float _reloadTime = 0.5f;
    private ObjectPooler _objectPooler;

    protected override void Start()
    {
        base.Start();
        _objectPooler = ObjectPooler.Instance; 
    }

    protected void Shoot()
    {
        _objectPooler.SpawnFromPool(_projectileTag, _shootPosition.position, transform.rotation); 
    }
}
