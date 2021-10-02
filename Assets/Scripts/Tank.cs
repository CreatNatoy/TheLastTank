using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Tank : MonoBehaviour
{
    [Header("Общие характеристики")] // загаловок в юнити инспекторе
    [SerializeField] private int _maxHealth = 30;
    [Range(0f,5f)] //диапазон в юнити инспекторе 
    [SerializeField] protected float _movementSpeed = 3f;
    [SerializeField] protected float _angleOffset = 90f;
    [Tooltip("Скорость поворота")] // подсказка при наведений в юнити инспекторе
    [SerializeField] protected float _rotationSpeed = 7f;
    [Space(10)] // отступ в юните инспекторе
    [SerializeField] private int _points = 0;
    protected UI _ui;
    protected Rigidbody2D _rigidbody;
    protected int _currentHealth;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
        _rigidbody = GetComponent<Rigidbody2D>();
        _ui = GameObject.FindObjectOfType<UI>();
        _ui.UpdateScoreAndLevel();
        _ui.UpdateHp(_currentHealth); 

    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage; 
        if(_currentHealth <= 0)
        {
            Stats.Score += _points;
            _ui.UpdateScoreAndLevel(); 
            Destroy(gameObject); 
        }
    }

    protected abstract void Move(); 

    protected void SetAngle(Vector3 target)
    {
        Vector3 deltaPosition = target - transform.position;
        float angleZ = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0f, 0f, angleZ + _angleOffset);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * _rotationSpeed);
    }
}
