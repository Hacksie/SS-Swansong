using UnityEngine;

namespace HackedDesign
{
    public class CargoShip : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Projectile")
            {
                Explode();
                Missile m = other.gameObject.GetComponent<Missile>();
                if (m != null)
                {
                    m.Explode();
                }
                else {
                    Debug.LogError(this.name + ": missile doesn't have Missile component");
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
