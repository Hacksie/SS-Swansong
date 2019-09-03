using UnityEngine;

namespace HackedDesign
{
    public class Hawke : MonoBehaviour
    {
        [SerializeField]
        public float thrust;

        [SerializeField]
        public float patrolSpeed;

        [SerializeField]
        public float huntSpeed;

        [SerializeField]
        public float fightSpeed;

        [SerializeField]
        public float rotateSpeed;

        [SerializeField]
        public Vector3 destination;

        [SerializeField]
        public Laser currentMissile;

        [SerializeField]
        public int missileCount = 10;

        [SerializeField]
        public Vector2[] patrol;

        [SerializeField]
        public int patrolIndex = 0;

        [SerializeField]
        public int cargo = 50;

        [SerializeField]
        public int cargoExplode = 3;


        [SerializeField]
        public bool disabled = false;

        [SerializeField]
        public bool exploded = false;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private int health = 5;

        [SerializeField]
        private bool killable = true;



        private new Rigidbody2D rigidbody = null;

        public void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null)
            {
                Debug.LogError(this.name + ": rigidbody is null");
            }
            if (animator == null)
            {
                Debug.LogError(this.name + ": animator not set");
            }

            Reset();
        }

        public void Reset()
        {
            destination = this.transform.position;
            patrolIndex = 0;
            exploded = false;
            disabled = false;
            health = 5;
            currentMissile = null;

        }

        public void UpdateMovement()
        {
            if (gameObject.activeInHierarchy)
            {
                if (!disabled)
                {
                    // Do some collision avoidance

                    rigidbody.velocity = transform.up * thrust * Time.fixedDeltaTime;
                    Vector3 targetVector = Game.Instance.player.transform.position - transform.position;
                    float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;
                    rigidbody.angularVelocity = -1 * rotatingIndex * rotateSpeed * Time.fixedDeltaTime;
                    UpdateAnimations(true);
                }
                else
                {
                    if (gameObject.activeInHierarchy)
                    {
                        rigidbody.velocity = Vector2.zero;
                    }
                    UpdateAnimations(false);
                }
            }
        }


        public void Explode()
        {
            if (currentMissile != null)
            {
                currentMissile.Explode();
            }
            Debug.Log(this.name + ": explode");
            Game.Instance.Explosion(this.transform.position);
            this.gameObject.SetActive(false);
            exploded = true;
            Game.Instance.NextMission();
        }

        void UpdateAnimations(bool moving)
        {
            animator.SetBool("Thrust", moving);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                if (missileCount > 0 && (currentMissile == null || !currentMissile.gameObject.activeInHierarchy || currentMissile.source != this.gameObject))
                {
                    //missileCount--;
                    currentMissile = Game.Instance.FireLaser(this.transform.position + this.transform.up, this.transform.up, this.gameObject, true);
                    //currentMissile = Game.Instance.FireMissile(this.transform.position + this.transform.up, this.transform.up, this.gameObject, Game.Instance.player.gameObject, "AS-39 Gyrfalcon", true);
                }
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Projectile")
            {
                Missile m = other.gameObject.GetComponent<Missile>();
                if (m != null)
                {
                    m.Explode();

                    if (killable)
                    {
                        if (m.name == "ES-23 Harpoon")
                        {

                            disabled = true;
                            Game.Instance.IncreaseCargo(cargo);
                        }
                        else
                        {
                            Explode();
                            Game.Instance.IncreaseCargo(cargoExplode);
                            Game.Instance.Explosion(this.transform.position);
                        }
                    }
                }
                Laser l = other.gameObject.GetComponent<Laser>();
                if (l != null)
                {
                    if (l.source == this.gameObject)
                    {
                        return;
                    }
                    l.Explode();

                    if (killable)
                    {
                        health -= 1;
                        if (health <= 0)
                        {
                            Explode();
                        }
                    }
                }
            }
        }
    }
}
