using UnityEngine;
using System.Collections;

public enum BlockType
{
	EMPTY,
    //Safe
	SPAWN,
	SAFE,
    //Normal
	GROUND,
    //traps
	FALLING = 10,
	SPIKED,
	FROZEN
}

public class Block : MonoBehaviour {

	[SerializeField]
	BlockType _currentBlockType = BlockType.EMPTY;
	public BlockType BlockType { get { return _currentBlockType; } }
	BlockType _previousBlockType = BlockType.EMPTY;

	[SerializeField]
	MeshRenderer _blockRenderer;
	[SerializeField]
	BoxCollider _blockCollider;

	Rigidbody _rigidbody;

    [SerializeField] private PhysicMaterial _groundMat;
    [SerializeField] private PhysicMaterial _spikedMat;
    [SerializeField] private PhysicMaterial _frozenMat;
	
	public bool IsBlockSelected { get { return _isBlockSelected; } set { _isBlockSelected = value; UpdateSelection(); } }
    private bool _isBlockSelected = false;

    [SerializeField]
	MeshRenderer _selectionCube;

    Vector3 _originalPosition = Vector3.zero;

    private bool _discovered = false;

    private bool discovered
    {
        get { if ((int) _currentBlockType < 10) return true; else return _discovered; }
        set { _discovered = true; }
    }

    void Start()
    {
        _originalPosition = transform.localPosition;

        if (_currentBlockType == BlockType.SPAWN)
        {
            PlayerManager.instance.GetNewPlayer().transform.position = new Vector3(this.transform.position.x, 2, this.transform.position.z);
        }
    }
    void Update()
    {
        if (_currentBlockType != _previousBlockType)
            UpdateBlock(_currentBlockType);
    }

    public void Init(Vector3 originalPosition)
    {
        ForceUpdateCurrentBlock();
        ShowSelector(false);
        _originalPosition = originalPosition;
    }

    /// <summary>
    /// Update to new type
    /// </summary>
    /// <param name="newType"></param>
    public void UpdateBlock(BlockType newType)
	{
		_currentBlockType = newType;

        _discovered = false;

		UpdateRenderer();
		UpdatePhysicsMaterial();

		_previousBlockType = _currentBlockType;
	}

    /// <summary>
    /// Update using current type
    /// </summary>
	public void ForceUpdateCurrentBlock()
	{
		UpdateRenderer(true);
		UpdatePhysicsMaterial();
	}

    public void ResetBlock()
    {
        this.transform.localPosition = _originalPosition;
        this.transform.rotation = Quaternion.identity;
        this.transform.localScale = Vector3.one;

        _discovered = false;

        if (_rigidbody != null)
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        ForceUpdateCurrentBlock();
    }

    //TODO Show state
    //If build phase
    //If game phase

#region Update helper methods

	private void UpdatePhysicsMaterial()
	{
		if (_groundMat != null & _spikedMat != null && _frozenMat != null)
        {
            if (GetComponentInChildren<SendCollisionToParent>() == null)
            {
                this.transform.GetChild(0).gameObject.AddComponent<SendCollisionToParent>();
            }
            GetComponentInChildren<SendCollisionToParent>().onCollisionEnterCustom += OnCollisionEnter;
            
            if (_currentBlockType == BlockType.EMPTY)
			{
				if (_blockCollider.enabled)
					_blockCollider.enabled = false;
			}
			else
			{
				if (!_blockCollider.enabled)
					_blockCollider.enabled = true;

				switch (_currentBlockType)
				{
					case BlockType.FROZEN:
						_blockCollider.material = _frozenMat;
						break;
					case BlockType.SPIKED:
						_blockCollider.material = _spikedMat;
						break;
					case BlockType.FALLING:
					default:
						_blockCollider.material = _groundMat;
						break;
				}
			}
		}
		else
		{
			Debug.LogWarning("Remember to set materials on Cube prefab");
		}
	}

    private void UpdateSelection()
    {
        if (IsBlockSelected)
        {
            _selectionCube.material.color = Color.grey;
        }
        else
        {
            _selectionCube.material.color = Color.white;
        }
    }

    public void ShowSelector(bool show)
    {
        if (show)
        {
            if (!_selectionCube.enabled)
            {
                _selectionCube.enabled = true;
            }
        }
        else
        {

            if (_selectionCube.enabled)
            {
                _selectionCube.enabled = false;
            }
        }
    }

    private void UpdateRenderer(bool ignorePrevious = false)
    {
        if (!ignorePrevious && _previousBlockType == _currentBlockType)
            return;

        if (_currentBlockType == BlockType.EMPTY)
        {
            if (_blockRenderer.enabled)
                _blockRenderer.enabled = false;
        }
        else
        {
            if (!_blockRenderer.enabled)
                _blockRenderer.enabled = true;

            if (!discovered)
                _blockRenderer.material.color = Color.green;
            else
                switch (_currentBlockType)
                {
                    case BlockType.GROUND:
                        _blockRenderer.material.color = Color.green;
                        break;
                    case BlockType.FALLING:
                        _blockRenderer.material.color = Color.black;
                        break;
                    case BlockType.FROZEN:
                        _blockRenderer.material.color = Color.blue;
                        break;
                    case BlockType.SPIKED:
                        _blockRenderer.material.color = Color.grey;
                        break;
                    case BlockType.SAFE:
                        _blockRenderer.material.color = Color.yellow;
                        break;
                    case BlockType.SPAWN:
                        _blockRenderer.material.color = Color.white;
                        break;
                }
        }
    }

    public void Destroy()
    {
        DestroyImmediate(this.gameObject);
    }
#endregion

    void OnCollisionEnter(Collision col)
    {
        if ((int)_currentBlockType >= 10)//Unhide block if trap
        {
            _discovered = true;
            UpdateRenderer(true);
        }

        if (_currentBlockType == BlockType.FALLING) //If falling, enable gravity if player collision
        {
            if (col.transform.GetComponent<PlayerControllerPhys>() != null)
            {
                if (_rigidbody == null)
                {
                    _rigidbody = gameObject.AddComponent<Rigidbody>();
                }
                _rigidbody.useGravity = true;
            }
        }
    }
}
