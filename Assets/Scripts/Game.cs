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
        public PlayerController player;

        [Header("Limits")]
        [SerializeField]
        public float minCrossSection;

        [SerializeField]
        public float startingMaxFuel;

        [SerializeField]
        public float maxHeat;

        [SerializeField]
        public float heatBleedPerSecond;

        [SerializeField]
        public float heatGainPerSecond;

        [Header("Current Player State")]
        [SerializeField]
        public float fuel;

        [SerializeField]
        public float maxFuelIncrease;

        [SerializeField]
        public float heat;

        [SerializeField]
        public float minCrossSectionReduction;

        [SerializeField]
        public bool bayDoorsOpen;

        public int MaxFuel {
            get 
            {
                return (int)(startingMaxFuel + maxFuelIncrease);
            }
        }

        
        public int CrossSection
        {
            get
            {
                return (int)Mathf.Clamp(minCrossSection + minCrossSectionReduction + heat + (bayDoorsOpen ? 40 : 0), 0, 100);
            }
        }



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