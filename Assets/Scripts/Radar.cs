using UnityEngine;

namespace HackedDesign
{
    public class Radar : MonoBehaviour
    {
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

        public void Reset()
        {
            
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Projectile")
            {
                Missile m = other.gameObject.GetComponent<Missile>();
                if (m != null)
                {
                    Explode();
                    m.Explode();
                }
                Laser l = other.gameObject.GetComponent<Laser>();
                if (l != null)
                {
                    l.Explode();
                }                
            }
        }

        public void Explode()
        {
            Debug.Log(this.name + ": exploded");
            this.gameObject.SetActive(false);
        }                

    }
}
