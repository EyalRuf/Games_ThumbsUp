using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float destroyAfterTime;

    void Start()
    {
        Invoke("Destroy", destroyAfterTime);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
