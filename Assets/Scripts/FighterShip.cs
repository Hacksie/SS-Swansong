using UnityEngine;

namespace HackedDesign
{
    public class FighterShip : MonoBehaviour
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
        public FighterState state = FighterState.PATROL;

        [SerializeField]
        public Vector3 destination;

        [SerializeField]
        public Missile currentMissile;

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
            state = FighterState.PATROL;
            patrolIndex = 0;
            exploded = false;
        }

        public void UpdateMovement()
        {
            if (!disabled)
            {
                switch (state)
                {
                    case FighterState.PATROL:
                        Vector3 destination = new Vector3(patrol[patrolIndex].x, patrol[patrolIndex].y);

                        if ((transform.position - destination).sqrMagnitude < 2)
                        {
                            patrolIndex++;
                            if (patrolIndex >= patrol.Length)
                            {
                                patrolIndex = 0;
                            }
                        }
                        break;

                    case FighterState.HUNT:
                        break;

                    case FighterState.FIGHT:
                        destination = Game.Instance.player.transform.position;
                        break;

                }

                // Do some collision avoidance

                rigidbody.velocity = transform.up * thrust * Time.fixedDeltaTime;
                Vector3 targetVector = destination - transform.position;
                float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;
                rigidbody.angularVelocity = -1 * rotatingIndex * rotateSpeed * Time.fixedDeltaTime;
                UpdateAnimations(true);
            }
            else
            {
                if(gameObject.activeInHierarchy)
                {
                    rigidbody.velocity = Vector2.zero;
                }
                UpdateAnimations(false);
            }            

        }

        public void Hunt(Vector3 position)
        {
            //this.transform.position = position;
            this.state = FighterState.HUNT;
            this.destination = position;
        }

        public void Explode()
        {
            Debug.Log(this.name + ": explode");
            Game.Instance.Explosion(this.transform.position);
            this.gameObject.SetActive(false);
            exploded = true;
        }

        void UpdateAnimations(bool moving)
        {
            animator.SetBool("Thrust", moving);
        }        

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                state = FighterState.FIGHT;
                if (missileCount > 0 && (currentMissile == null || !currentMissile.gameObject.activeInHierarchy || currentMissile.source != this.gameObject))
                {
                    missileCount--;
                    currentMissile = Game.Instance.FireMissile(this.transform.position + this.transform.up, this.transform.up, this.gameObject, Game.Instance.player.gameObject, "AIM-393", true);
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

                    if (m.name == "ES-23 Harpoon")
                    {
                        m.Explode();
                        disabled = true;
                        Game.Instance.IncreaseCargo(cargo);
                    }
                    else
                    {
                        m.Explode();
                        Explode();
                        Game.Instance.IncreaseCargo(cargoExplode);
                        Game.Instance.Explosion(this.transform.position);
                    }
                }
                Laser l = other.gameObject.GetComponent<Laser>();
                if (l != null)
                {
                    l.Explode();
                }
            }
        }
    }

    public enum FighterState
    {
        PATROL,
        HUNT,
        FIGHT
    }
}
