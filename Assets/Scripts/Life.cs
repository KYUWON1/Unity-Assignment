using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int damage;

    void Update()
    {
        if (LifeManager.instance.life <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            LifeManager.instance.life -= damage;
        }
    }
}
