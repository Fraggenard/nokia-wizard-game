using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool facingLeft;

    [SerializeField]
    public int movementSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        facingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKey(KeyCode.Keypad8)) {
            gameObject.transform.position += new Vector3(0, movementSpeed, 0);
        } else if (Input.GetKey(KeyCode.Keypad4)) {
            if (!facingLeft) {
                facingLeft = true;
            }
            gameObject.transform.position += new Vector3(-movementSpeed, 0, 0);
        } else if (Input.GetKey(KeyCode.Keypad6)) {
            if (facingLeft) {
                facingLeft = false;
            }
            gameObject.transform.position += new Vector3(movementSpeed, 0, 0);
        } else if (Input.GetKey(KeyCode.Keypad2)) {
            gameObject.transform.position += new Vector3(0, -movementSpeed, 0);
        }

        if (facingLeft) {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        } else {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
