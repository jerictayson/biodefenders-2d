using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private float _attackCooldown = 0.5f;
    private PlayerMovement _movement;
    private float _cooldownTimer = Mathf.Infinity;
    
    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _cooldownTimer > _attackCooldown && _movement.CanAttack())
        {
            Attack();
        }
        
        _cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        _animator.SetTrigger("attack");
        _cooldownTimer = 0f;
    }
}
