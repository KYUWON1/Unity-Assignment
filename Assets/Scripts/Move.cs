using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;

    public float xSpeed = 10f;
    public float zSpeed = 10f;
    public float runSpeed = 5f;
    public float jumpPower = 5f;
    public float damagePower = 5f;
    public float rotationSpeed;
    public ParticleSystem playerParticle;
    bool ParticlePlay = false;
    bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
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

        if(Input.GetKeyDown(KeyCode.T)){
            if(!ParticlePlay){
                ParticlePlay = true;
                playerParticle.Play();
            }
            else{
                ParticlePlay = false;
                playerParticle.Stop();
            }
        } 
        

        if (Input.GetKeyDown(KeyCode.LeftShift)){xSpeed *= runSpeed;zSpeed *= runSpeed;} // 달리기 구현 
        if (Input.GetKeyUp(KeyCode.LeftShift)){xSpeed /= runSpeed;zSpeed /= runSpeed;}
        
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0,mouseX * rotationSpeed,0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                Vector3 jumpVec =  new Vector3(0,jumpPower,0);
                rb.AddForce(jumpVec,ForceMode.Impulse);
            }
        }
    
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Terrain")
            isJumping = false;
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 damageVec = new Vector3(-damagePower,0,0);
            rb.AddRelativeForce(damageVec,ForceMode.Impulse);
        }
    }
}
