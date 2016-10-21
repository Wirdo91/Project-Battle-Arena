using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
public class PlayerControllerPhys : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField]
    Vector3 direction = Vector3.zero;

    [SerializeField]
    bool _disableInput = false;

    private PlayerInput _input;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    void Update()
    {
        if (Camera.main.WorldToScreenPoint(this.transform.position).y < 0) //Player is out of screen i.e. dead
        {
            Debug.Log(this.gameObject.name + " is dead!");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_disableInput)
            return;

        direction = _input.GetNewDirection();

        _rigidbody.AddForce(direction.normalized * 500 * Time.deltaTime, ForceMode.Force);//Check other ways to move (doesn't work the best)

        //Use attack
    }
}
