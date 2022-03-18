using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Tank : MonoBehaviour
{
    [Header("Общие характеристики")] 
    [SerializeField] private int _maxHealth = 30;
    [Range(0f,5f)] 
    [SerializeField] protected float _movementSpeed = 3f;
    [SerializeField] protected float _angleOffset = 90f;
    [Tooltip("Скорость поворота")] 
    [SerializeField] protected float _rotationSpeed = 7f;
    [Space(10)] 
    [SerializeField] private int _points = 0;
    protected UI _ui;
    protected Rigidbody2D _rigidbody;
    protected int _currentHealth;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
        _rigidbody = GetComponent<Rigidbody2D>();
       _ui = GameObject.FindObjectOfType<UI>();
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

    private Quaternion AnglePlayer(Vector3 target)
    {
        Vector3 deltaPosition = target - transform.position;
        float angleZ = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0f, 0f, angleZ + _angleOffset);
        return angle; 
    }

    protected void SetAngle(Vector3 target)
    {
        Quaternion angle = AnglePlayer(target); 
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * _rotationSpeed);
    }

    protected void LookPlayer(Vector3 target)
    {
        Quaternion angle = AnglePlayer(target);
        transform.rotation = angle; 
    }

}
