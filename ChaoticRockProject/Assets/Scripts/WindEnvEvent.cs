using UnityEngine;
using System.Collections;

namespace Assets
{
    public class WindEnvEvent : EnvEvent
    {
      

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void StartEnvEvent()
        {
            Debug.Log("StartWindEffect");
        }

        public override void EndEnvEvent()
        {
            Debug.Log("EndWindEffect");
        }
    }
}