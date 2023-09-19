using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject prefab;
    public GameObject shootPoint;
    public GameObject camera;
    public int ShootMode = 1;
    float delay =0.0f;
    float waitTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            ShootMode = 1;
        if(Input.GetKeyDown(KeyCode.Alpha2))
            ShootMode = 2;
        if(Input.GetKeyDown(KeyCode.Alpha3))
            ShootMode = 3;
    

        //일반모드 
        if (ShootMode == 1){
            if (Input.GetKeyDown(KeyCode.Mouse0)){
            GameObject clone = Instantiate(prefab);
            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;
            }
        }
        //샷건모드 
        else if (ShootMode == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)){
            //Debug.Log("샷건 모드");
            GameObject clone1 = Instantiate(prefab);
            GameObject clone2 = Instantiate(prefab);
            GameObject clone3 = Instantiate(prefab);
            clone1.transform.position = shootPoint.transform.position;
            clone1.transform.rotation = shootPoint.transform.rotation;
            clone2.transform.position = shootPoint.transform.position;
            clone2.transform.rotation = shootPoint.transform.rotation * Quaternion.Euler(0,20,0);
            clone3.transform.position = shootPoint.transform.position;
            clone3.transform.rotation = shootPoint.transform.rotation * Quaternion.Euler(0,-20,0);
            }
        }
        //연사모드
        else if (ShootMode == 3)
        {   
            if (Input.GetKey(KeyCode.Mouse0))
            {   
                delay += 0.1f;
                if(delay >= waitTime)
                {
                    //Debug.Log("연사모드");
                    GameObject clone1 = Instantiate(prefab);
                    clone1.transform.position = shootPoint.transform.position;
                    clone1.transform.rotation = shootPoint.transform.rotation;
                    delay = 0.0f; 
                }
                
            }
        }
    }
}
