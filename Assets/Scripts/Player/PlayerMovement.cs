using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _horizontal;
        private Rigidbody2D _body;
        private Animator _animator;
        private BoxCollider2D _collider;
        [SerializeField]
        private float _moveSpeed = 5f;
        [SerializeField]
        private LayerMask _groundLayer;
        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Walk();
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded())
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
        }

        private bool _isGrounded()
        {
            Debug.Log("Checking if grounded");
            var bounds = _collider.bounds;
            return Physics2D.BoxCast(bounds.center, bounds.size, 0f, 
                Vector2.down, .1f, _groundLayer);
        }

        public bool CanAttack()
        {
            return _horizontal == 0;
        }
    }
}
