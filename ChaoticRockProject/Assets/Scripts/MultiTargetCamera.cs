using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiTargetCamera : MonoBehaviour
{
    public GameObject[] targets;

    public Vector3 offset;
    public float smoothTime = .5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomSpeed = 25f;

    private Vector3 velocity;
    private Camera cam;


    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    private void LateUpdate()
    {
        if (targets.Length != 0)
        {
            Move();
            Zoom();
        }
    }

    public void Zoom()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Clamp(GetGreatestDistance(), minZoom, maxZoom), Time.deltaTime * zoomSpeed);
    }

    public void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    
    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {

            bounds.Encapsulate(targets[i].transform.position);


        }

        return bounds.size.magnitude / 2;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Length == 1)
        {
            return targets[0].transform.position;
        }

        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.center;
    }
}
