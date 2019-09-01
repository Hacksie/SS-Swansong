using UnityEngine;

namespace HackedDesign
{
    public class CargoShip : MonoBehaviour
    {
        [SerializeField]
        public float thrust;

        [SerializeField]
        public float rotateSpeed;

        [SerializeField]
        public Vector2[] patrol;

        [SerializeField]
        public int patrolIndex = 0;

        private new Rigidbody2D rigidbody = null;

        public void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null)
            {
                Debug.LogError(this.name + ": rigidbody is null");
            }

            Reset();
        }


        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Projectile")
            {

                Missile m = other.gameObject.GetComponent<Missile>();
                if (m != null)
                {
                    m.Explode();
                    Explode();
                    Game.Instance.Explosion(this.transform.position);
                }
                // Laser l = other.gameObject.GetComponent<Laser>();
                // if (l != null)
                // {
                //     l.Explode();
                // }
            }
        }

        public void UpdateMovement()
        {
            Vector3 target = new Vector3(patrol[patrolIndex].x, patrol[patrolIndex].y);

            if ((transform.position - target).sqrMagnitude < 2)
            {
                patrolIndex++;
                if (patrolIndex >= patrol.Length)
                {
                    patrolIndex = 0;
                }
            }

            // Do some collision avoidance

            rigidbody.velocity = transform.up * thrust * Time.fixedDeltaTime;
            Vector3 targetVector = target - transform.position;
            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;
            rigidbody.angularVelocity = -1 * rotatingIndex * rotateSpeed * Time.fixedDeltaTime;
        }



        public void Reset()
        {
            patrolIndex = 0;
        }

        public void Explode()
        {
            Debug.Log(this.name + ": explode");
            this.gameObject.SetActive(false);
        }


    }
}
