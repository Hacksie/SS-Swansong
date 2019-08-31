using UnityEngine;

namespace HackedDesign
{
    public class ProxMine : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Player") // Ignore the shield
            {
                Explode();
                Game.Instance.GameOverMine();
                return;
            } 

            if(other.tag == "Projectile")
            {
                Explode();
                Missile m = other.GetComponent<Missile>();
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