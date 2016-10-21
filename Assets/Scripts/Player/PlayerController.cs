using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float _maxSpeed = 5f;

    private Vector2 _currentDirection = Vector2.zero;

    private Animator _animator;

    private Transform _transform;

	// Use this for initialization
	void Start ()
	{
	    _transform = this.GetComponent<Transform>();
	    _animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ReadMoveInput();
        UpdatePosition();
        UpdateAnimator();
    }

    void UpdatePosition()
    {
        _transform.Translate(_currentDirection * _maxSpeed * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _animator.SetBool("MovingNorth", true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _animator.SetBool("MovingNorth", false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _animator.SetBool("MovingSouth", true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _animator.SetBool("MovingSouth", false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetBool("MovingLeft", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _animator.SetBool("MovingLeft", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetBool("MovingRight", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _animator.SetBool("MovingRight", false);
        }
    }

    void ReadMoveInput()
    {
        Vector2 newDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newDirection.y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newDirection.y -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newDirection.x += 1;
        }

        _currentDirection = newDirection.normalized;
    }
}
