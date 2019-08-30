
using UnityEngine;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float turnRate = 90.0f;

        [SerializeField]
        private Rigidbody2D rigidBody = null;

        [SerializeField]
        private float maxThrust = 5.0f;


        public float Velocity
        {
            get
            {
                return rigidBody.velocity.magnitude;
            }
            set
            {
                rigidBody.velocity = rigidBody.velocity.normalized * value;
            }
        }

        public float MaxThrust
        {
            get
            {
                return maxThrust;
            }
        }

        void Start()
        {
            if (rigidBody == null)
            {
                Debug.LogError(this.name + ": rigidbody not set");
            }

            animator = GetComponent<Animator>();

            if (animator == null)
            {
                Debug.LogError(this.name + ": animator not set");
            }

            animator.SetBool("Thrust", false);
        }

        public void UpdateActions()
        {
            if (Input.GetButtonUp("Start"))
            {
                Game.Instance.state = GameState.MENU;
            }
            if (Input.GetButtonUp("Bay Doors"))
            {
                Game.Instance.bayDoorsOpen = !Game.Instance.bayDoorsOpen;
            }
        }

        private void UpdateRotation()
        {
            float turn = -1.0f * Input.GetAxis("Horizontal") * turnRate;
            this.transform.Rotate(new Vector3(0, 0, turn * Time.fixedDeltaTime));
        }


        public void UpdateMovement()
        {
            UpdateRotation();

            float force = Input.GetAxis("Vertical") * maxThrust * Time.fixedDeltaTime;


            rigidBody.AddRelativeForce(new Vector2(0, force), ForceMode2D.Impulse);
            if (rigidBody.velocity.magnitude > maxThrust)
            {
                Velocity = maxThrust;
                //rigidBody.velocity = rigidBody.velocity.normalized * maxThrust;
            }

            // Clamp within a circle of 0,0
            this.transform.position = Vector2.ClampMagnitude(this.transform.position, Game.Instance.worldBounds);

            if (UnityEngine.Input.GetButtonUp("Fire3"))
            {
                Velocity = 0;
            }


            UpdateAnimations(force);

            if (force != 0)
            {
                Game.Instance.IncreaseHeat();
                Game.Instance.ConsumeFuel();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(this.name + ": collision");
            Game.Instance.GameOverCollision();

        }

        private void UpdateAnimations(float force)
        {
            if (force != 0)
            {

                animator.SetBool("Thrust", true);
            }
            else
            {
                animator.SetBool("Thrust", false);
            }
        }
    }
}