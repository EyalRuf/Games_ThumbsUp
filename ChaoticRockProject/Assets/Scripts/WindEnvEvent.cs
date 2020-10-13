using UnityEngine;
using System.Collections;

namespace Assets
{
    public class WindEnvEvent : EnvEvent
    {
        public Vector3 windDirection;
        public float windStrength;

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
            this.gameObject.SetActive(true);
        }

        public override void EndEnvEvent()
        {
            Debug.Log("EndWindEffect");
            this.gameObject.SetActive(false);
        }

        private void OnTriggerStay(Collider other)
        {
            other.GetComponent<Rigidbody>().AddForce(windDirection * windStrength);
        }
    }
}