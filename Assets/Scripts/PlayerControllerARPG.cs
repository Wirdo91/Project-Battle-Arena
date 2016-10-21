using UnityEngine;
using System.Collections;

/// <summary>
/// http://gaurav.munjal.us/Universal-LPC-Spritesheet-Character-Generator/#?weapon=wand_wood
/// </summary>

public class PlayerControllerARPG : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed = 5f;

	private float _currentSpeed = 0f;

    private Vector3 _currentDirection = Vector3.zero;

    private Animator _animator;

    private Transform _transform;

    private SkinControllerARPG _skinController;

    public AnimationAction _currentAnimationAction = AnimationAction.Idle;

    // Use this for initialization
    void Start()
    {
        _transform = this.GetComponent<Transform>();
        _animator = this.GetComponent<Animator>();
        _skinController = this.GetComponent<SkinControllerARPG>();
    }

    // Update is called once per frame
    void Update()
    {
		_currentSpeed = 0f;

        ReadMoveInput();
        UpdatePosition();
        UpdateAnimator();
    }

    void UpdatePosition()
    {
        _transform.Translate(_currentDirection * _currentSpeed * Time.deltaTime, Space.World);
    }

    void UpdateAnimator()
    {
        if (_currentSpeed > 0)
        {
            _animator.SetBool("Move", true);
        }
        else
        {
            _animator.SetBool("Move", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Bow");
        }

        switch (_currentAnimationAction)
        {
            case AnimationAction.Idle:
                _skinController._currentAnimationRow = AnimationRow.Die;
                break;
            case AnimationAction.Move:
                if (Mathf.Abs(Vector3.Angle(_currentDirection, Vector3.left)) <= 45)
                {
                    _skinController._currentAnimationRow = AnimationRow.MoveLeft;
                }
                else if (Mathf.Abs(Vector3.Angle(_currentDirection, Vector3.right)) <= 45)
                {
                    _skinController._currentAnimationRow = AnimationRow.MoveRight;
                }
                else if (Mathf.Abs(Vector3.Angle(_currentDirection, Vector3.forward)) <= 45)
                {
                    _skinController._currentAnimationRow = AnimationRow.MoveUp;
                }
                else if (Mathf.Abs(Vector3.Angle(_currentDirection, Vector3.back)) <= 45)
                {
                    _skinController._currentAnimationRow = AnimationRow.MoveDown;
                }
                else
                {
                    _skinController._currentAnimationRow = AnimationRow.MoveDown;
                }
                break;
            case AnimationAction.Bow:
                if (Mathf.Abs(Vector3.Angle(_currentDirection, Vector3.left)) <= 45)
                {
                    _skinController._currentAnimationRow = AnimationRow.BowLeft;
                }
                else if (Mathf.Abs(Vector3.Angle(_currentDirection, Vector3.right)) <= 45)
                {
                    _skinController._currentAnimationRow = AnimationRow.BowRight;
                }
                else if (Mathf.Abs(Vector3.Angle(_currentDirection, Vector3.forward)) <= 45)
                {
                    _skinController._currentAnimationRow = AnimationRow.BowUp;
                }
                else if (Mathf.Abs(Vector3.Angle(_currentDirection, Vector3.back)) <= 45)
                {
                    _skinController._currentAnimationRow = AnimationRow.BowDown;
                }
                else
                {
                    _skinController._currentAnimationRow = AnimationRow.BowDown;
                }
                break;
        }
    }

    void ReadMoveInput()
    {
		Vector3 newDirection = _currentDirection;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
		{
			newDirection = Vector3.zero;
			_currentSpeed = _maxSpeed;
		}

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newDirection.z -= 1;
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
