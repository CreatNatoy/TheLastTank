using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : ShootableTank
{
    private float _timer;

    public override void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _ui.UpdateHp(_currentHealth); 
        if(_currentHealth <= 0 )
        {
            Stats.ResetAllStats();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    protected override void Move()
    {
      //  transform.Translate(Vector2.down * Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime); 
       Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.velocity = direction.normalized * _movementSpeed;  // дает возможность двигатсья на искосок 
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
}
