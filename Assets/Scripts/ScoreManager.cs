using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int amount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            print("스코너매니저를 복사합니다. 해당 매니저 무시");
        }
    }
}
