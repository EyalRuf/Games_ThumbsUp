using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    private GameManager gm;

    public float destroyRockDelay;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        gm.AddScore(col.name);
        DestroySelf();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
