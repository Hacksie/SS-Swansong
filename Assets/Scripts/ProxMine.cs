using UnityEngine;

namespace HackedDesign
{
    public class ProxMine : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player") // Ignore the shield
            {
                Explode();
                Game.Instance.GameOverMine();
                return;
            }

            if (other.tag == "Projectile")
            {

                Missile m = other.gameObject.GetComponent<Missile>();
                if (m != null)
                {
                    Explode();
                    m.Explode();
                    Game.Instance.Explosion(this.transform.position);
                }
                Laser l = other.gameObject.GetComponent<Laser>();
                if (l != null)
                {
                    Explode();
                    Game.Instance.Explosion(this.transform.position);
                    l.Explode();
                }

            }
        }

        public void Reset()
        {

        }

        public void Explode()
        {
            Debug.Log(this.name + ": explode");
            this.gameObject.SetActive(false);
        }
    }
}