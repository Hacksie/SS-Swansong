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
        private MainMenuPresenter menuUI;

        [SerializeField]
        private GameUIPresenter gameUI;

        [Header("Limits")]
        [SerializeField]
        public float minCrossSection;

        [SerializeField]
        public float startingFuel;        

        [SerializeField]
        public float startingMaxFuel;

        [SerializeField]
        public float maxHeat;

        [SerializeField]
        public float startingHeat;        

        [SerializeField]
        public float heatBleedPerSecond;

        [SerializeField]
        public float heatGainPerSecond;

        //[Header("Current Player State")]
        [SerializeField]
        public float Fuel
        {
            get;
            private set;
        }

        [SerializeField]
        public float maxFuelIncrease;

        [SerializeField]
        public float Heat {
            get;
            private set;
        }

        [SerializeField]
        public float minCrossSectionReduction;

        [SerializeField]
        public bool BayDoorsOpen
        {
            get; private set;
        }

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
                return (int)Mathf.Clamp(minCrossSection + minCrossSectionReduction + Heat + (BayDoorsOpen ? 40 : 0), 0, 100);
            }
        }


        Game()
        {
            Instance = this;
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

            if (menuUI == null)
            {
                Debug.LogError(this.name + ": menu ui not set");
            }

            state = GameState.MENU;
            player.gameObject.SetActive(false);
        }

        public void NewGame()
        {
            state = GameState.PLAYING;
            player.gameObject.SetActive(true);
            ResetState();
        }

        void ResetState()
        {
            Debug.Log(this.name + ": reset state");
            player.transform.position = Vector2.zero;
            player.transform.rotation = Quaternion.identity;
            Fuel = startingFuel;
            maxFuelIncrease = 0;
            Heat = startingHeat;
            minCrossSectionReduction = 0;
            BayDoorsOpen = false;
        }

        public void ContinueGame()
        {
            state = GameState.PLAYING;
            player.gameObject.SetActive(true);            
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void IncreaseHeat()
        {
            Game.Instance.Heat += Game.Instance.heatGainPerSecond * Time.fixedDeltaTime;
            if (Game.Instance.Heat > Game.Instance.maxHeat)
            {
                Game.Instance.Heat = Game.Instance.maxHeat;
            }
        }

        public void BleedHeat()
        {
            Game.Instance.Heat -= Game.Instance.heatBleedPerSecond * Time.fixedDeltaTime;
            if (Game.Instance.Heat < 0)
            {
                Game.Instance.Heat = 0;
            }
        }

        void Update()
        {
             
            switch (state)
            {
                case GameState.MENU:
                    Time.timeScale = 0;
                    break;

                case GameState.PLAYING:
                    Time.timeScale = 1;

                    if(Input.GetButtonUp("Start"))
                    {
                        state = GameState.MENU;
                    }
                    break;
            }
        }


        void FixedUpdate()
        {
            if (state == GameState.PLAYING)
            {
                player.UpdateMovement();
                BleedHeat();
            }
        }

        void LateUpdate()
        {
            menuUI.UpdateUI();
            gameUI.UpdateUI();
        }
    }

    public enum GameState
    {
        MENU,
        PLAYING
    }
}