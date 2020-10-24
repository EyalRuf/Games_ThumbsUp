using UnityEngine;
using System.Collections;
using System;

namespace Assets
{
    public class WindEnvEvent : EnvEvent
    {
        public Vector3 windDirection;
        public float windStrength;
        private float gradualWind = 0;
        [SerializeField] private float windGradualFactor = 0.01f;

        public override void StartEnvEvent()
        {
            this.gameObject.SetActive(true);
            gradualWind = 0;
        }

        public override void EndEnvEvent()
        {
            this.gameObject.SetActive(false);
        }

        private void OnTriggerStay(Collider other)
        {
            gradualWind = Mathf.Lerp(gradualWind, windStrength, windGradualFactor);
            other.GetComponent<Rigidbody>().AddForce(windDirection * gradualWind, ForceMode.Acceleration);
        }
    }
}