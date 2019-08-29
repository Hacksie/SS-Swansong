using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        public GameState state;

        [SerializeField]
        private PlayerController player;

        [Header("Limits")]
        [SerializeField]
        private float maxFuel;

        [SerializeField]
        private float maxHeat;        

        [SerializeField]
        private float heatBleed;  

        [Header("Current Player State")]
        [SerializeField]
        private float fuel;

        [SerializeField]
        private float heat;



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

        }

        void FixedUpdate()
        {
            player.UpdateMovement();
        }
    }

    public enum GameState
    {
        MENU,
        PLAYING
    }
}