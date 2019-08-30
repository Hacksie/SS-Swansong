using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        public Camera camera;

        [Header("UI")]

        [SerializeField]
        private MainMenuPresenter menuUI;

        [SerializeField]
        private GameUIPresenter gameUI;

        [SerializeField]
        private GameOverCollisionPresenter gameOverCollisionUI;

        [SerializeField]
        private GameOverMinePresenter gameOverMineUI;

        [SerializeField]
        private RadarArrow radarArrow;

        [Header("EntityPools")]
        [SerializeField]
        private SpriteRenderer targetingSquare;
        [SerializeField]
        private GameObject planetParent;
        [SerializeField]
        private GameObject asteroidParent;
        [SerializeField]
        private GameObject radarParent;
        [SerializeField]
        private GameObject mineParent;

        [Header("State")]
        public GameState state;


        [SerializeField]
        public PlayerController player;

        public float highestRadarPulse;
        public Radar highestRadar;



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


        public float lastPulse;
        public float pulseSpeed;

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

        public float Track
        {
            get; private set;

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
            if (gameOverCollisionUI == null)
            {
                Debug.LogError(this.name + ": game over collision ui not set");
            }
            if (gameOverMineUI == null)
            {
                Debug.LogError(this.name + ": game over mine ui not set");
            }

            if (planetParent == null)
            {
                Debug.LogError(this.name + ": planet parent objects not set");
            }
            if (asteroidParent == null)
            {
                Debug.LogError(this.name + ": asteroids parent not set");
            }
            if (radarParent == null)
            {
                Debug.LogError(this.name + ": radar parent not set");
            }
            if (mineParent == null)
            {
                Debug.LogError(this.name + ": mine parent not set");
            }
            if (targetingSquare == null)
            {
                Debug.LogError(this.name + ": target square not set");
            }

            if (radarArrow == null)
            {
                Debug.LogError(this.name + ": radar arrow not set");
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
            SpawnAsteroids();
            SpawnRadarSatellites();
            SpawnProxMines();
        }

        void SpawnPlanets()
        {
            int planets = planetParent.transform.childCount;

            float angle = 360.0f / planets;
            Debug.Log(angle);


            for (int i = 0; i < planets; i++)
            {
                Debug.Log(i * angle);
                float magnitude = Random.Range(100, 1000);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle)) * (Vector2.up * magnitude);

                planetParent.transform.GetChild(i).transform.position = position;
            }
        }

        void SpawnAsteroids()
        {
            int asteroids = asteroidParent.transform.childCount;

            float angle = 360.0f / asteroids;

            for (int i = 0; i < asteroids; i++)
            {
                float magnitude = Random.Range(50, 1000);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle)) * (Vector2.up * magnitude);

                float rotation = Random.Range(0, 360);
                asteroidParent.transform.GetChild(i).transform.position = position;
                asteroidParent.transform.GetChild(i).transform.Rotate(0, 0, rotation, Space.World);
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnRadarSatellites()
        {
            int radars = radarParent.transform.childCount;

            float angle = 360.0f / radars;

            for (int i = 0; i < radars; i++)
            {
                float magnitude = Random.Range(50, 1000);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle)) * (Vector2.up * magnitude);

                //float rotation = Random.Range(0, 360);
                radarParent.transform.GetChild(i).transform.position = position;
                //asteroidParent.transform.GetChild(i).transform.Rotate(0, 0, rotation, Space.World);
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnProxMines()
        {
            int mines = mineParent.transform.childCount;

            float angle = 360.0f / mines;

            for (int i = 0; i < mines; i++)
            {
                float magnitude = Random.Range(40, 1000);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle)) * (Vector2.up * magnitude);


                mineParent.transform.GetChild(i).transform.position = position;
                // Check if there is a planet there and move if need be
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
            Heat += heatGainPerSecond * Time.fixedDeltaTime;
            if (Heat > maxHeat)
            {
                Heat = maxHeat;
            }
        }

        public void BleedHeat()
        {
            Heat -= heatBleedPerSecond * Time.fixedDeltaTime;
            if (Heat < 0)
            {
                Heat = 0; // Move this into the property?
            }
        }

        public void ConsumeFuel()
        {
            Fuel -= fuelConsumptionPerSecond * Time.fixedDeltaTime;
            if (Fuel < 0)
            {
                Fuel = 0;

                // GameOver state?
            }
        }

        public void GameOverCollision()
        {
            state = GameState.GAMEOVERCOLLISION;
        }

        public void GameOverMine()
        {
            state = GameState.GAMEOVERMINE;
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
                    //state = GameState.MENU;
                    break;

                case GameState.GAMEOVERMINE:
                    Time.timeScale = 0;
                    Debug.Log(this.name + ": Game Over via Proximity Mine");
                    //state = GameState.MENU;
                    break;
            }
        }


        void FixedUpdate()
        {
            if (state == GameState.PLAYING)
            {
                player.UpdateMovement();
                BleedHeat();
                UpdateRadars();
                radarArrow.UpdatePosition();
            }
        }

        void UpdateRadars()
        {
            if ((Time.time - lastPulse) > 1)
            {
                highestRadar = null;

            }

            if ((Time.time - lastPulse) > pulseSpeed)
            {
                Debug.Log(this.name + ": radar pulse");
                highestRadarPulse = 0;
                lastPulse = Time.time;
                int radars = radarParent.transform.childCount;

                for (int i = 0; i < radars; i++)
                {
                    Radar r = radarParent.transform.GetChild(i).GetComponent<Radar>();
                    if (r == null)
                    {
                        Debug.LogError(radarParent.transform.GetChild(i) + ": is not set as radar");
                        continue;
                    }

                    float inv = r.UpdateRadar();

                    if (inv > highestRadarPulse)
                    {
                        highestRadarPulse = inv;
                        highestRadar = r;
                    }
                }


                float distance = (highestRadar.transform.position - player.transform.position).magnitude;
                Track = highestRadarPulse * (CrossSection * CrossSection);
                //Debug.Log(this.name + ": " + highestRadarPulse);
                //Debug.Log(this.name + ": " + highestRadar.name + " " + distance + " " + Track);
            }
        }

        void LateUpdate()
        {
            menuUI.UpdateUI();
            gameUI.UpdateUI();
            gameOverCollisionUI.UpdateUI();
            gameOverMineUI.UpdateUI();
        }
    }

    public enum GameState
    {
        MENU,
        INTRO,
        TUTORIAL,
        PLAYING,
        GAMEOVERCOLLISION,
        GAMEOVERMINE
    }
}