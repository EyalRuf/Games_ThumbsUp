using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnvEvent))]
public class EnvEventManager : MonoBehaviour
{
    [SerializeField] private float envEventCounter = 0;
    [SerializeField] EnvEvent envEvent;
    [SerializeField] private bool isEventActive = false;

    const float timeBetweenEventsMax = 40;
    const float timeBetweenEventsMin = 15;
    const float eventDurationMax = 10;
    const float eventDurationMin = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        envEventCounter = Random.Range(timeBetweenEventsMin, timeBetweenEventsMax);
    }

    // Update is called once per frame
    void Update()
    {
        envEventCounter -= Time.deltaTime;

        if(envEventCounter <= 0)
        {
            if(isEventActive)
            {
                envEvent.EndEnvEvent();
                envEventCounter = Random.Range(timeBetweenEventsMin, timeBetweenEventsMax);
                isEventActive = false;
            } 
            else
            {
                envEvent.StartEnvEvent();
                envEventCounter = Random.Range(eventDurationMin, eventDurationMax);
                isEventActive = true;
            }          
        }
    }
}
