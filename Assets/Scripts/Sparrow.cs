using UnityEngine;

namespace HackedDesign
{
    public class Sparrow : MonoBehaviour
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

        // [SerializeField]
        // public SparrowState state = SparrowState.DISABLED;

        [SerializeField]
        public Vector3 destination;

        [SerializeField]
        public Missile currentMissile;

        [SerializeField]
        public int missileCount = 10;

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
            currentMissile = null;
            //state = SparrowState.DISABLED;
            patrolIndex = 0;
            exploded = false;
            health = 5;
            disabled = false;
            missileCount = 10;
        }

        public void UpdateMovement()
        {
            // Check Mission etc


            if (gameObject.activeInHierarchy)
            {
                rigidbody.velocity = Vector2.zero;
            }
            UpdateAnimations(false);

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
            {//Story Event
                if (Game.Instance.currentMission == 7)
                {
                    Game.Instance.NextMission();
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
                }
                Laser l = other.gameObject.GetComponent<Laser>();
                if (l != null)
                {
                    l.Explode();
                }
            }
        }
    }

}
