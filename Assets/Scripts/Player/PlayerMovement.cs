using UnityEngine;

namespace Player
{
    
    //create enum for player state
    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Attacking
    }
    
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
        private PlayerState _state;
        public static bool CanMove = true, CanShoot = true;

        private bool _grounded;
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
            if(CanMove){
                Walk();
                if (Input.GetKeyDown(KeyCode.Space) && _isGrounded())
                {
                    Jump();
                }
                    
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
            if (!CanMove)
            {
                _animator.SetInteger("state", (int) PlayerState.Idle);
                return;
            }

            if(_horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _state = PlayerState.Running;
            }
            else if(_horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _state = PlayerState.Running;
            }
            else
            {
                _state = PlayerState.Idle;
            }
            
            //check if player is jumping
            if (_body.velocity.y > .1f)
            {
                _state = PlayerState.Jumping;
            }
            _animator.SetInteger("state", (int) _state);
        }

        private void Jump()
        {
            _body.velocity = new Vector2(_body.velocity.x, 5f);
        }

        private bool _isGrounded()
        {
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
