using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {


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
            if(rigidBody == null)
            {
                Debug.LogError(this.name + ": rigidbody not set");
            }
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateMovement()
        {
            float turn = -1.0f * UnityEngine.Input.GetAxis("Horizontal") * turnRate;
            this.transform.Rotate(new Vector3(0, 0, turn * Time.fixedDeltaTime));
            float force = UnityEngine.Input.GetAxis("Vertical") * maxThrust * Time.fixedDeltaTime;

            rigidBody.drag = force / maxThrust;
            rigidBody.AddRelativeForce(new Vector2(0, force), ForceMode2D.Impulse);
        }
    }
}