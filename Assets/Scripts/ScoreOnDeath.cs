using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    public int amount;

    void OnDestroy()
    {
        ScoreManager.instance.amount += amount;
    }
}
