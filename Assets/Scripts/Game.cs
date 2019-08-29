using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        [Header("UI")]

        [SerializeField]
        private MainMenuPresenter menuUI;

        [SerializeField]
        private GameUIPresenter gameUI;

        [Header("EntityPools")]
        [SerializeField]
        private SpriteRenderer targetingSquare;
        [SerializeField]
        private GameObject planetParent;

        [Header("State")]
        public GameState state;


        [SerializeField]
        public PlayerController player;



        [Header("Limits")]
        [SerializeField]
        public float worldBounds;

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
        public float fuelConsumptionPerSecond;

        [SerializeField]
        public float heatBleedPerSecond;

        [SerializeField]
        public float heatGainPerSecond;

        [SerializeField]
        public int maxCargo;

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
        public float Heat
        {
            get;
            private set;
        }

        [SerializeField]
        public int Cargo
        {
            get;
            private set;
        }        

        [SerializeField]
        public int Credits
        {
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

        private GameObject currentTarget;

        public GameObject CurrentTarget
        {
            get
            {
                return currentTarget;
            }
            set
            {
                if (value != null)
                {
                    Debug.Log(this.name + ": set target to " + value.name);
                    currentTarget = value;
                    targetingSquare.transform.position = value.transform.position;
                    Renderer r = value.gameObject.GetComponent<Renderer>();
                    targetingSquare.size = r.bounds.size;
                    targetingSquare.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log(this.name + ": clear target");
                    currentTarget = null;
                    targetingSquare.gameObject.SetActive(false);
                }
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

            if (planetParent == null)
            {
                Debug.LogError(this.name + ": planet parent objects not set");
            }
            if (targetingSquare == null)
            {
                Debug.LogError(this.name + ": target square not set");
            }



            state = GameState.MENU;
            player.gameObject.SetActive(false);
            targetingSquare.gameObject.SetActive(false);
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
            Credits = 0;
            minCrossSectionReduction = 0;
            BayDoorsOpen = false;
            SpawnPlanets();
        }

        void SpawnPlanets()
        {
            int planets = planetParent.transform.childCount;

            float angle = 360 / planets;

            for (int i = 0; i < planets; i++)
            {
                float magnitude = Random.Range(100, 1000);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle)) * (Vector2.up * magnitude);
                Debug.Log(position);
                planetParent.transform.GetChild(i).transform.position = position;
            }



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
                Game.Instance.Heat = 0; // Move this into the property?
            }
        }

        public void ConsumeFuel()
        {
            Game.Instance.Fuel -= Game.Instance.fuelConsumptionPerSecond * Time.fixedDeltaTime;
            if (Game.Instance.Fuel < 0)
            {
                Game.Instance.Fuel = 0;

                // GameOver state?
            }
        }

        public void GameOverCollision()
        {
            Game.Instance.state = GameState.GAMEOVERCOLLISION;
        }

        void Update()
        {

            switch (state)
            {
                case GameState.MENU:
                    Time.timeScale = 0;
                    break;

                // case GameState.WARPZONE:
                //     Time.timeScale = 1;
                //     break;

                case GameState.PLAYING:
                    Time.timeScale = 1;

                    if (Input.GetButtonUp("Start"))
                    {
                        state = GameState.MENU;
                    }
                    break;

                case GameState.GAMEOVERCOLLISION:
                    Time.timeScale = 0;
                    Debug.Log(this.name + ": Game Over via Collision");
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

            // if (state == GameState.WARPZONE)
            // {
            //     player.transform.position = Vector2.zero;
            //     player.transform.rotation = Quaternion.identity;
            //     BleedHeat(); // Continue to bleed heat
            // }
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
        PLAYING,
        GAMEOVERCOLLISION
    }
}