using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtagonistController : MonoBehaviour
{
    private enum MovementDirection
    {
        None,
        Left,
        Right
    }

    private MovementDirection _movementDirection = MovementDirection.None;
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private AudioSource _audioSource;

    private bool _directionKeyPressed;
    private bool _obstacleKeyPressed;

    public float VelocityComponent;

    public float PlayerSpeed;

    public GameObject MessageWindow;
    public Text CoinCountText;
    public Text FinalCoinCountText;

    private float? _startTime;

    void Awake()
    {
        _movementDirection = MovementDirection.None;
    }
    // Use this for initialization
    void Start()
    {
//        _obstacleFilter2D = new ContactFilter2D()
//        {
//            useLayerMask = true,
//            layerMask = LayerMask.GetMask("Obstacle"),
//        };
        _movementDirection = MovementDirection.None;
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();

//        VelocityComponent = PlayerSpeed / (float)Math.Sqrt(2);

        MessageWindow.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        CheckKeyPress();
        CheckRoadCollision();
//        CheckObstacle();
    }

    private void MoveCamera()
    {
        Camera.main.transform.position = new Vector3(_transform.position.x, _transform.position.y-3, -3);
    }

//    private void CheckObstacle()
//    {
//        if (Input.GetAxisRaw("Fire2") > 0 && _obstacleKeyPressed == false)
//        {
//            _obstacleKeyPressed = true;
//            int colliderCount = _collider2D.OverlapCollider(_obstacleFilter2D, _overlappingColliders);
//            for (int i = 0; i < colliderCount; i++)
//            {
//                Destroy(_overlappingColliders[i].gameObject);
//            }
//        }
//        else if (Input.GetAxisRaw("Fire2") == 0)
//        {
//            _obstacleKeyPressed = false;
//        }
//    }

    private void OnDestroy()
    {
        Debug.Log("player destroyed");
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        FinalCoinCountText.text = CoinCountText.text;
        MessageWindow.SetActive(true);
    }

    private void CheckRoadCollision()
    {
//        Debug.Log("checking road collision");
//        Debug.Log("touching: " + _collider2D.IsTouchingLayers(LayerMask.GetMask("Road")).ToString());
//        Debug.Log("dir: " + _movementDirection);
        if (_collider2D.IsTouchingLayers(LayerMask.GetMask("Road")) == false && _movementDirection != MovementDirection.None)
        {
            Debug.Log("Player destroyed by collision");
//            Debug.Log("touching: " + _collider2D.IsTouchingLayers(LayerMask.GetMask("Road")).ToString());
//            Debug.Log("dir: " + _movementDirection);
            Destroy(this.gameObject);
        }
    }

    private void CheckKeyPress()
    {
        if (_startTime != null && Time.time >= _startTime)
        {
            ChangeDirection();
            _startTime = null;
        }
        if (_startTime != null) return;
        if (Input.GetAxisRaw("Fire1") > 0 && _directionKeyPressed == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    _directionKeyPressed = true;
                    ChangeDirection();
                }
            }
            else
            {
                _directionKeyPressed = true;
                ChangeDirection();
            }
        }
        else if (Math.Abs(Input.GetAxisRaw("Fire1")) < 0.01f)
        {
            _directionKeyPressed = false;
        }
    }

    private void ChangeDirection()
    {
        switch (_movementDirection)
        {
            case MovementDirection.None:
                if (_audioSource.isPlaying == false)
                {
                    _audioSource.Play();
                    _startTime = Time.time + 0.15f;
                    return;
                }
                _movementDirection = MovementDirection.Right;
                _rigidbody2D.velocity = new Vector2(VelocityComponent, VelocityComponent);
                break;
            case MovementDirection.Left:
                _movementDirection = MovementDirection.Right;
                _rigidbody2D.velocity = new Vector2(VelocityComponent, VelocityComponent);
                break;
            case MovementDirection.Right:
                _movementDirection = MovementDirection.Left;
                _rigidbody2D.velocity = new Vector2(-VelocityComponent, VelocityComponent);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetDirection(MovementDirection direction)
    {
        _movementDirection = direction;
        switch (_movementDirection)
        {
            case MovementDirection.None:
                _rigidbody2D.velocity = new Vector2(0, 0);
                break;
            case MovementDirection.Left:
                _rigidbody2D.velocity = new Vector2(-VelocityComponent, VelocityComponent);
                break;
            case MovementDirection.Right:
                _rigidbody2D.velocity = new Vector2(VelocityComponent, VelocityComponent);
                break;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }
    }
}