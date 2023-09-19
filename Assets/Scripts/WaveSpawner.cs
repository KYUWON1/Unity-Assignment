using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject prefab;
    Vector3 spawn = new Vector3(); 

    public float startTime;
    public float endTime;
    public float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("Spawn",startTime,spawnRate); // start타임 후에 spawnRate만큼 실행 
        Invoke("CancelInvoke",endTime); // endtime후에 invoke 취소 
    }

    // Update is called once per frame
    void Spawn()
    {
        float x = Random.Range(-285f,40f);
        float z = Random.Range(-267f,211f);
        float yAngle = Random.Range(0f,360f);

        Instantiate(prefab, transform.position = new Vector3(x,-5,z), transform.rotation = Quaternion.Euler(0,yAngle,0)); // random 하게 움직이도록 해야할듯
    }
}
