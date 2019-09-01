using UnityEngine;

namespace HackedDesign
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField]
        public int cargo;

        public new Rigidbody2D rigidbody;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null)
            {
                Debug.LogError(this.name + ": rigidbody is null");
            }
        }

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

        public void Update()
        {
            //FIXME: Check if we go out of bounds
        }

        public void Explode()
        {
            Debug.Log(this.name + ": explode");
            int cargo = Random.Range(Game.Instance.asteroidCargoMin, Game.Instance.asteroidCargoMax);
            Game.Instance.IncreaseCargo(cargo);
            Game.Instance.Explosion(this.transform.position);

            this.gameObject.SetActive(false);
        }              
    }
}
