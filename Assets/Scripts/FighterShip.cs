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

        public void Reset()
        {
            destination = this.transform.position;
            state = FighterState.PATROL;
        }

        public void UpdateMovement()
        {
            switch (state)
            {
                case FighterState.PATROL:
                    return;
                    break;

                case FighterState.HUNT:
                    break;

                case FighterState.FIGHT:
                    destination = Game.Instance.player.transform.position;
                    break;

            }

            rigidbody.velocity = transform.up * thrust * Time.fixedDeltaTime;
            Vector3 targetVector = destination - transform.position;
            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;
            rigidbody.angularVelocity = -1 * rotatingIndex * rotateSpeed * Time.fixedDeltaTime;

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
            this.gameObject.SetActive(false);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                state = FighterState.FIGHT;
                Debug.Log(this.name + ": targeting player");

                if (missileCount > 0 && (currentMissile == null || !currentMissile.gameObject.activeInHierarchy || currentMissile.source != this.gameObject))
                {
                    missileCount--;
                    currentMissile = Game.Instance.FireMissile(this.transform.position + this.transform.up, this.transform.up, this.gameObject, Game.Instance.player.gameObject, "AIM-393", true);
                }
                //Game.Instance.FireMissile()
                
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Projectile")
            {
                Explode();
                Missile m = other.gameObject.GetComponent<Missile>();
                m.Explode();
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
