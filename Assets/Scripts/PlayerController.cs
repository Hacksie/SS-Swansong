using System.Collections;
using System.Collections.Generic;
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
        private Rigidbody2D rigidBody;

        [SerializeField]
        private float maxThrust = 2.0f;

        [SerializeField]
        private float currentThrust = 0.0f;

        // Start is called before the first frame update
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

        public void UpdateMovement()
        {
            float turn = -1.0f * UnityEngine.Input.GetAxis("Horizontal") * turnRate;
            this.transform.Rotate(new Vector3(0, 0, turn * Time.fixedDeltaTime));
            float force = UnityEngine.Input.GetAxis("Vertical") * maxThrust * Time.fixedDeltaTime;

            rigidBody.drag = force / maxThrust;
            rigidBody.AddRelativeForce(new Vector2(0, force), ForceMode2D.Impulse);

            // Clamp within a circle of 0,0
            this.transform.position = Vector2.ClampMagnitude(this.transform.position, Game.Instance.worldBounds);


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