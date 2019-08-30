using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Radar : MonoBehaviour
    {
        public int pulseOffset;
        public int pulseSpeed;
        public float lastPulse;
        // Start is called before the first frame update
        void Start()
        {
            pulseOffset = Random.Range(0, pulseSpeed);
        }

        public float UpdateRadar()
        {

                // lastPulse = Time.time;
                
                //Debug.Log(this.name + ": update radar");
                //float distance = (this.transform.position - Game.Instance.player.transform.position).magnitude;
                float sqrdistance = (this.transform.position - Game.Instance.player.transform.position).sqrMagnitude;
                float inverse = 1 / sqrdistance;
                return inverse;
                /*
                float trigger = inverse * (Game.Instance.CrossSection * Game.Instance.CrossSection);
                if (trigger > 1)
                {
                    Debug.Log(this.name + ": distance " + distance + " xsec " + Game.Instance.CrossSection + " trigger " + trigger);
                }*/
            

            // A player in the safe zone can't be found
        }

    }
}
