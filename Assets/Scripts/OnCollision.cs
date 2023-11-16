using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public GameObject prefab;
    public int killScore = 1;
    public ParticleSystem enemyParticle;


    // 설정한 프리펩과 접촉시 제거 
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemyParticle.Play();
            Destroy(gameObject, 0.5f);
            ScoreManager.instance.amount +=killScore;
        }
            
    }
}
