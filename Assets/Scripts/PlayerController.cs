
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

        [SerializeField]
        private float lastLaser = 0.0f;

        [SerializeField]
        private float laserTimeout = 0.5f;


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
            if (Input.GetButtonUp("Next Weapon"))
            {
                Game.Instance.currentBay++;
                if (Game.Instance.currentBay >= 4)
                {
                    Game.Instance.currentBay = 0;
                }
            }
            if (Input.GetButtonUp("Previous Weapon"))
            {
                Game.Instance.currentBay--;
                if (Game.Instance.currentBay < 0)
                {
                    Game.Instance.currentBay = 3;
                }
            }

            if (Input.GetButtonUp("Next Target"))
            {
                int count = Game.Instance.currentTargets.Count;
                if (count > 0 && Game.Instance.CurrentTarget != null)
                {
                    int index = Game.Instance.currentTargets.IndexOf(Game.Instance.CurrentTarget);
                    index++;
                    if (index >= count)
                    {
                        index = 0;
                    }

                    Game.Instance.CurrentTarget = Game.Instance.currentTargets[index];
                }
                //Game.Instance.bayDoorsOpen = !Game.Instance.bayDoorsOpen;
            }
            if (Input.GetButtonUp("Previous Target"))
            {
                int count = Game.Instance.currentTargets.Count;
                if (count > 0 && Game.Instance.CurrentTarget != null)
                {
                    int index = Game.Instance.currentTargets.IndexOf(Game.Instance.CurrentTarget);
                    index--;
                    if (index < 0)
                    {
                        index = count - 1;
                    }

                    Game.Instance.CurrentTarget = Game.Instance.currentTargets[index];
                }
                //Game.Instance.bayDoorsOpen = !Game.Instance.bayDoorsOpen;
            }
            if (Input.GetButtonUp("Fire Missile"))
            {
                FireMissile();
            }

            if (Input.GetButtonUp("Fire Laser"))
            {
                FireLaser();
            }
        }

        private void FireMissile()
        {
            if (Game.Instance.bayDoorsOpen && Game.Instance.CurrentTarget != null)
            {
                Game.Instance.FireMissile(this.transform.position + this.transform.up, this.transform.up, this.gameObject, Game.Instance.CurrentTarget, Game.Instance.bay[Game.Instance.currentBay], false);
                Game.Instance.bay[Game.Instance.currentBay] = "";
            }
        }

        private void FireLaser()
        {
            if ((Time.time - lastLaser) > laserTimeout)
            {
                lastLaser = Time.time;
                //Game.Instance.
                Game.Instance.FireLaser(this.transform.position + this.transform.up, this.transform.up, this.gameObject, false);
                //Game.Instance.IncreaseHeat(6);
                //Game.Instance.bay[Game.Instance.currentBay] = "";
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

            if (UnityEngine.Input.GetButtonUp("Brake"))
            {
                Velocity = 0;
                // Take a penalty for using the brakes
                // Fixme: remove hard coded            
                Game.Instance.IncreaseHeat(20);
                Game.Instance.ConsumeFuel(10);
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
            if (other.gameObject.tag == "Projectile")
            {
                Missile m = other.gameObject.GetComponent<Missile>(); // FIXME: Make this more efficient
                if (m != null)
                {
                    if (m.source == this.gameObject)
                    {
                        return;
                    }
                    else
                    {
                        Game.Instance.GameOverMissile();
                        return;
                    }
                }
                Laser l = other.gameObject.GetComponent<Laser>(); // FIXME: Make this more efficient
                if (l != null)
                {
                    if (l.source == this.gameObject)
                    {
                        return;
                    }
                    else{
                        Game.Instance.GameOverMissile();
                        return;
                    }
                }
            }

            Debug.Log(this.name + ": collision");
            Game.Instance.GameOverCollision();

        }

        private void UpdateAnimations(float force)
        {
            if (force >= 0.01 || force <= -0.01)
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