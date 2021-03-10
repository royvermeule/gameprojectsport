using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    private const float _movementSpeed   = 10f;
    private const float _jumpForce       = 13.5f;
    private const float _baseGravity     = 30.0f;
    private const float _fastFallGravity = 70.0f;

    private float _gravity = _baseGravity;

    private bool _leftPressed        = false;
    private bool _rightPressed       = false;
    private bool _spacePressed       = false;
    private int  _horizontalMovement = 0;

    private bool _onGround = false;

    private Vector2 _motion = new Vector2(0, 0);

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Get input
        _leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        _rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        if (_leftPressed && _rightPressed)
        {
            _horizontalMovement = 0;
        }
        else if (_leftPressed)
        {
            _horizontalMovement = -1;
        }
        else if (_rightPressed)
        {
            _horizontalMovement = 1;
        }
        else
        {
            _horizontalMovement = 0;
        }
        _spacePressed = Input.GetKey(KeyCode.Space);

        // Move player
        _motion = new Vector2(_horizontalMovement * _movementSpeed, _motion.y);
        if (!_onGround)
        {
            _motion.y -= _gravity * Time.deltaTime;
        }

        if (_spacePressed && _onGround)
        {
            _motion.y = _jumpForce;
        }

        if (!_spacePressed && !_onGround && _motion.y > 0.0f)
        {
            _gravity = _fastFallGravity;
        }
        else
        {
            _gravity = _baseGravity;
        }

        _rigidbody.velocity = _motion;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _onGround = true;
        _motion.y = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _onGround = false;
    }
}
