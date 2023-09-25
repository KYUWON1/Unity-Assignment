using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance;
    public int life;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("해당인스턴스제거, 인스턴스 복제");
        }
    }
}
