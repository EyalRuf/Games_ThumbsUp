using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    public int belongToPlayer;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Rock"))
        {
            gm.AddScore(belongToPlayer, col.gameObject.GetComponent<RockBehavior>().score);

            //either destroy or shrink rock
            Destroy(col.gameObject);
        }
    }
}
