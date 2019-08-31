using UnityEngine;

namespace HackedDesign
{
    public class AsteroidBig : MonoBehaviour
    {
        public Asteroid asteroid1;
        public Asteroid asteroid2; 

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
                Laser l = other.gameObject.GetComponent<Laser>();
                if (l != null)
                {
                    l.Explode();
                }
            }
        }

        public void Explode()
        {
            Debug.Log(this.name + ": explode");
            this.gameObject.SetActive(false);

            SpawnAsteroids();
        }

        // Check of asteroid escapes world bounds and hide

        public void SpawnAsteroids()
        {
            float randomAngle = Random.Range(0, 360);
            Vector3 position = Quaternion.Euler(0, 0, randomAngle) * (Vector2.up * 1f);

            int cargo = Random.Range(Game.Instance.asteroidBigCargoMin, Game.Instance.asteroidBigCargoMax);
            Game.Instance.IncreaseCargo(cargo);
            
            this.asteroid1.transform.position = this.transform.position + position;
            this.asteroid2.transform.position = this.transform.position - position;
            this.asteroid1.gameObject.SetActive(true);
            this.asteroid2.gameObject.SetActive(true);            
            this.asteroid1.rigidbody.AddRelativeForce(position * 1, ForceMode2D.Impulse);
            this.asteroid2.rigidbody.AddRelativeForce(position * 1, ForceMode2D.Impulse);            
        }
    }
}
