using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = 4;

    private void Update()
    {
        transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
    }
}
