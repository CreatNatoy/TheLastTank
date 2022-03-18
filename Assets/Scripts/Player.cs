using UnityEngine;

public class Player : ShootableTank
{
    [SerializeField] private GameObject _panelGameOver; 
    private float _timer;

    protected override void Start()
    {
        base.Start();
        _ui.UpdateScoreAndLevel();
        _ui.UpdateHp(_currentHealth);       
    }

    private void Update()
    {
        Move();
        SetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (_timer <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
                _timer = _reloadTime;
            }
        }
        else
            _timer -= Time.deltaTime; 
    }

    public override void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _ui.UpdateHp(_currentHealth); 
        if(_currentHealth <= 0 )
        {
            Stats.ResetAllStats();
            Time.timeScale = 0f; 
            _panelGameOver.SetActive(true); 
        }
    }

    protected override void Move()
    {
       Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.velocity = direction.normalized * _movementSpeed;
    }

}
