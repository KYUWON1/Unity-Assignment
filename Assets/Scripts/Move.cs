using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float xSpeed = 10f;
    public float zSpeed = 10f;
    public float rotationSpeed = 1.0f;
    public float runSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        if (Input.GetKey(KeyCode.D)){transform.Translate(xSpeed * Time.deltaTime,0,0);}
        if (Input.GetKey(KeyCode.A)){transform.Translate(-xSpeed * Time.deltaTime,0,0);}
        if (Input.GetKey(KeyCode.W)){transform.Translate(0,0,zSpeed * Time.deltaTime);}
        if (Input.GetKey(KeyCode.S)){transform.Translate(0,0,-zSpeed * Time.deltaTime);}

        if (Input.GetKeyDown(KeyCode.LeftShift)){xSpeed *= runSpeed;zSpeed *= runSpeed;} // 달리기 구현 
        if (Input.GetKeyUp(KeyCode.LeftShift)){xSpeed /= runSpeed;zSpeed /= runSpeed;}
        
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0,mouseX * rotationSpeed,0);
    }
}
