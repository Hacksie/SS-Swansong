using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        [SerializeField]
        private PlayerController player;

        Game()
        {
            Instance = this;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            player.UpdateMovement();
        }

        void FixedUpdate()
        {
            
        }        
    }
}