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

        [SerializeField]
        private GameUIPresenter gameUI;

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

        public int MaxFuel
        {
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

        public void IncreaseHeat()
        {
            Game.Instance.heat += Game.Instance.heatGainPerSecond * Time.fixedDeltaTime;
            if (Game.Instance.heat > Game.Instance.maxHeat)
            {
                Game.Instance.heat = Game.Instance.maxHeat;
            }
        }

        public void BleedHeat()
        {
            Game.Instance.heat -= Game.Instance.heatBleedPerSecond * Time.fixedDeltaTime;
            if (Game.Instance.heat < 0)
            {
                Game.Instance.heat = 0;
            }            
        }



        // Start is called before the first frame update
        void Start()
        {
            if (player == null)
            {
                Debug.LogError(this.name + ": player controller not set");
            }

            if (gameUI == null)
            {
                Debug.LogError(this.name + ": game ui not set");
            }

        }


        void FixedUpdate()
        {
            player.UpdateMovement();
            BleedHeat();
        }

        void LateUpdate()
        {
            gameUI.UpdateUI();
        }
    }

    public enum GameState
    {
        MENU,
        PLAYING
    }
}