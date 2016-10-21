using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    [SerializeField]
    private KeyCode _leftButton;
    [SerializeField]
    private KeyCode _rightButton;
    [SerializeField]
    private KeyCode _upButton;
    [SerializeField]
    private KeyCode _downButton;
    [SerializeField]
    private KeyCode _attackButton;

    public Vector3 GetNewDirection()
    {
        //TODO if network player
        Vector3 newdirection = Vector3.zero;

        if (Input.GetKey(_upButton))
        {
            newdirection.z += 1;
        }
        if (Input.GetKey(_downButton))
        {
            newdirection.z -= 1;
        }
        if (Input.GetKey(_leftButton))
        {
            newdirection.x -= 1;
        }
        if (Input.GetKey(_rightButton))
        {
            newdirection.x += 1;
        }

        return newdirection.normalized;
    }

    public bool GetAttack()
    {
        return Input.GetKeyUp(_attackButton);
    }
}
