using UnityEngine;

namespace HackedDesign
{
    public class Radar : MonoBehaviour
    {
        bool disabled;
        float lastDisableTimer = 0;
        public float disableTimeout = 10.0f;

        public float UpdateRadar()
        {
            if(disabled && Time.time - lastDisableTimer > disableTimeout )
            {
                disabled = false;
            }

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
                    if (m.name == "ES-23 Harpoon")
                    {
                        disabled = true;
                        lastDisableTimer = Time.time;
                        m.Explode();
                        Game.Instance.Explosion(this.transform.position);
                    }
                    else if (m.name == "RM-44 Rook")
                    {
                        m.Explode();
                        Explode();
                        Game.Instance.Explosion(this.transform.position);
                    }
                    else
                    {
                        // Radars are immune to normal missiles
                        m.Explode();
                        Game.Instance.Explosion(m.transform.position);
                        
                    }
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
