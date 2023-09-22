using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _horizontal;
    private Rigidbody2D _body;
    private Animator _animator;
    [SerializeField]
    private float _moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        UpdateSprite();
    }

    private void Walk()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _body.velocity = new Vector2(_horizontal * _moveSpeed, _body.velocity.y);
    }

    private void UpdateSprite()
    {
        if(_horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(_horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        _animator.SetBool("running", _horizontal != 0);
    }

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, 5f);
    }
}
