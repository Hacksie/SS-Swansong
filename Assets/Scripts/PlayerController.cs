using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float turnRate = 90.0f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateMovement()
        {
            float turn = -1.0f * UnityEngine.Input.GetAxis ("Horizontal") * turnRate;
            this.transform.Rotate(new Vector3(0,0,turn * Time.deltaTime));

            

        }
    }
}