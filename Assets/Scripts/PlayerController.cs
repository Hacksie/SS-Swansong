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
        private Rigidbody2D rb;

        [SerializeField]
        private float maxThrust = 2.0f;

        [SerializeField]
        private float currentThrust = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            //rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateMovement()
        {
            float turn = -1.0f * UnityEngine.Input.GetAxis("Horizontal") * turnRate;
            this.transform.Rotate(new Vector3(0, 0, turn * Time.deltaTime));
            float force = UnityEngine.Input.GetAxis("Vertical") * maxThrust * Time.deltaTime;

            rb.drag = (force / maxThrust);
            rb.AddRelativeForce(new Vector2(0, force), ForceMode2D.Impulse);

            // if (rb.velocity.magnitude < maxThrust)
            // {
            //     rb.velocity = rb.velocity.normalized * maxThrust;
            // }
        }
    }
}