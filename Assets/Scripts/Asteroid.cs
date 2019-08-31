using UnityEngine;

namespace HackedDesign
{
    public class Asteroid : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Projectile")
            {
                Explode();
                Missile m = other.gameObject.GetComponent<Missile>();
                m.Explode();                
            }
        }

        public void Explode()
        {
            Debug.Log(this.name + ": explode");
            this.gameObject.SetActive(false);
        }              
    }
}
