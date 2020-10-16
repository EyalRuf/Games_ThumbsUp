using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiTargetCamera : MonoBehaviour
{
    public string[] targetTags;
    public List<GameObject> targets = new List<GameObject>();

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
        //gather all targets by tags
        targets.Clear();

        foreach(string tag in targetTags)
        {
            targets.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }
    }

    private void LateUpdate()
    {
        if (targets.Count != 0)
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
        for (int i = 0; i < targets.Count; i++)
        {

            bounds.Encapsulate(targets[i].transform.position);


        }

        return bounds.size.magnitude / 2;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].transform.position;
        }

        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.center;
    }
}
