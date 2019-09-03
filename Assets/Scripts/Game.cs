using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }

        public new Camera camera = null;

        [Header("UI")]

        [SerializeField]
        private MainMenuPresenter menuUI = null;

        [SerializeField]
        private GameUIPresenter gameUI = null;
        [SerializeField]
        private GameOverPresenter gameOverUI = null;
        [SerializeField]
        private IntroPresenter introUI = null;
        [SerializeField]
        private MarketPresenter marketUI = null;
        [SerializeField]
        private TutorialPresenter tutorialUI = null;
        [SerializeField]
        private DialoguePresenter dialogueUI = null;

        [SerializeField]
        private RadarArrow radarArrow = null;

        [SerializeField]
        private MissionArrow missionArrow = null;

        [Header("EntityPools")]
        [SerializeField]
        private SpriteRenderer targetingSquare = null;
        [SerializeField]
        private GameObject market = null;
        [SerializeField]
        private GameObject world = null;
        [SerializeField]
        private GameObject planetParent = null;
        [SerializeField]
        private GameObject asteroidBigParent = null;
        [SerializeField]
        private GameObject asteroidParent = null;
        [SerializeField]
        private GameObject radarParent = null;
        [SerializeField]
        private GameObject mineParent = null;
        [SerializeField]
        private GameObject cargoShipParent = null;
        [SerializeField]
        private GameObject fighterParent = null;
        [SerializeField]
        private GameObject pirateParent = null;
        [SerializeField]
        private GameObject missileParent = null;
        [SerializeField]
        private GameObject laserParent = null;
        [SerializeField]
        private GameObject explosionsParent = null;
        [SerializeField]
        private GameObject empParent = null;


        [Header("Information")]
        public List<MissileDescription> missileDescriptions = new List<MissileDescription>();

        [Header("Missions")]
        public List<MissionDescription> missionDescriptions = new List<MissionDescription>();

        public List<GameObject> missionTargets = new List<GameObject>();

        public GameObject[] mission7Targets = new GameObject[4];
        //public GameObject mission8Target = null;
        //public GameObject mission9Target = null;

        [SerializeField]
        private Sparrow sparrow = null;
        [SerializeField]
        private Hawke hawke = null;


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
        public float maxFuelIncrease;
        [SerializeField]
        public float minCrossSectionReduction;
        [SerializeField]
        public float bayDoorsCrossSection = 30.0f;

        [SerializeField]
        public float missileHeat = 10.0f;
        [SerializeField]
        public float laserHeat = 6.0f;

        [SerializeField]
        public int asteroidBigCargoMin = 0;
        [SerializeField]
        public int asteroidBigCargoMax = 20;

        [SerializeField]
        public int asteroidCargoMin = 20;
        [SerializeField]
        public int asteroidCargoMax = 100;

        [SerializeField]
        public int maxCargo;

        [SerializeField]
        public int fuelBuyMin = 20;
        [SerializeField]
        public int fuelBuyMax = 30;

        [SerializeField]
        public int fuelSellMin = 10;
        [SerializeField]
        public int fuelSellMax = 20;

        [SerializeField]
        public int cargoSellMin = 11;
        [SerializeField]
        public int cargoSellMax = 33;

        [SerializeField]
        public int startingCredits = 0;

        [SerializeField]
        public float marketRefreshRate = 120;

        [Header("State")]
        public GameState state;

        [SerializeField]
        public bool gameStarted = false;

        [SerializeField]
        public bool gameFinished = false;

        [SerializeField]
        public PlayerController player;

        [SerializeField]
        public bool bayDoorsOpen = false;

        [SerializeField]
        public string[] bay = new string[4];

        [SerializeField]
        public int currentBay = 0;

        [SerializeField]
        public int cargo = 0;

        [SerializeField]
        public int currentExplosionIndex = 0;

        [SerializeField]
        public int currentEMPIndex = 0;

        [SerializeField]
        public float lastMarketRefresh;

        [SerializeField]
        public int currentMission = 0;




        [SerializeField]
        public float fuel;

        [SerializeField]
        public int currentFuelBuyPrice;

        [SerializeField]
        public int currentFuelSellPrice;

        [SerializeField]
        public int currentCargoSellPrice;

        [SerializeField]
        public int currentMissile1SellPrice;

        [SerializeField]
        public int currentMissile2SellPrice;

        [SerializeField]
        public int currentMissile3SellPrice;

        [SerializeField]
        public int currentMissile4SellPrice;

        [SerializeField]
        public int currentMissile1BuyPrice;

        [SerializeField]
        public int currentMissile2BuyPrice;

        [SerializeField]
        public int currentMissile3BuyPrice;

        [SerializeField]
        public int currentMissile4BuyPrice;

        [SerializeField]
        public float heat;

        [SerializeField]
        public int credits;

        [SerializeField]
        public Vector3? radarTarget = null;

        public float highestRadarPulse;
        public Radar highestRadar;

        public List<GameObject> currentTargets;

        public int mission7targetCount = 0;

        private GameObject currentTarget = null;

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
                    currentTarget = value;
                }
                else
                {
                    currentTarget = null;
                    targetingSquare.gameObject.SetActive(false);
                }
            }
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
                return (int)Mathf.Clamp(minCrossSection + minCrossSectionReduction + heat + (bayDoorsOpen ? bayDoorsCrossSection : 0), 0, 100);
            }
        }

        public float Track
        {
            get; private set;

        }

        private float lastPulse = 0;
        private float pulseSpeed = 3.0f;


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
            if (introUI == null)
            {
                Debug.LogError(this.name + ": intro ui not set");
            }
            if (gameOverUI == null)
            {
                Debug.LogError(this.name + ": game over collision ui not set");
            }
            // if (missionUI == null)
            // {
            //     Debug.LogError(this.name + ": mission ui not set");
            // }
            if (marketUI == null)
            {
                Debug.LogError(this.name + ": market ui not set");
            }
            if (tutorialUI == null)
            {
                Debug.LogError(this.name + ": market ui not set");
            }
            if (dialogueUI == null)
            {
                Debug.LogError(this.name + ": dialogue ui not set");
            }

            if (market == null)
            {
                Debug.LogError(this.name + ": market not set");
            }
            if (world == null)
            {
                Debug.LogError(this.name + ": world parent not set");
            }

            if (planetParent == null)
            {
                Debug.LogError(this.name + ": planet parent objects not set");
            }
            if (asteroidBigParent == null)
            {
                Debug.LogError(this.name + ": asteroids big parent not set");
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
            if (cargoShipParent == null)
            {
                Debug.LogError(this.name + ": cargo ship parent not set");
            }
            if (fighterParent == null)
            {
                Debug.LogError(this.name + ": fighter parent not set");
            }
            if (pirateParent == null)
            {
                Debug.LogError(this.name + ": pirate parent not set");
            }
            if (missileParent == null)
            {
                Debug.LogError(this.name + ": missile parent not set");
            }
            if (laserParent == null)
            {
                Debug.LogError(this.name + ": laser parent not set");
            }
            if (explosionsParent == null)
            {
                Debug.LogError(this.name + ": explosion parent not set");
            }
            if (empParent == null)
            {
                Debug.LogError(this.name + ": explosion parent not set");
            }
            if (sparrow == null)
            {
                Debug.LogError(this.name + ": sparrow not set");
            }
            if (hawke == null)
            {
                Debug.LogError(this.name + ": hawke not set");
            }

            if (targetingSquare == null)
            {
                Debug.LogError(this.name + ": target square not set");
            }

            if (radarArrow == null)
            {
                Debug.LogError(this.name + ": radar arrow not set");
            }
            if (missionArrow == null)
            {
                Debug.LogError(this.name + ": mission arrow not set");
            }

            if (missionDescriptions.Count == 0)
            {
                Debug.LogError(this.name + ": missions not set");
            }



            state = GameState.MENU;
            player.gameObject.SetActive(false);
            targetingSquare.gameObject.SetActive(false);
            radarArrow.gameObject.SetActive(false);
            missionArrow.gameObject.SetActive(false);
            world.gameObject.SetActive(false);
        }

        public void NewGame()
        {
            //player.gameObject.SetActive(true);
            ResetState();
            state = GameState.INTRO;
            gameStarted = true;
        }

        // public void StartGame()
        // {
        //     gameStarted = true;
        //     ContinueGame();
        // }

        public void ShowTutorial()
        {
            state = GameState.TUTORIAL;
            player.gameObject.SetActive(true);
            world.gameObject.SetActive(true);
        }


        public void ContinueGame()
        {
            state = GameState.PLAYING;
            player.gameObject.SetActive(true);
            world.gameObject.SetActive(true);
            Input.ResetInputAxes();

        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void IncreaseHeat()
        {
            heat += heatGainPerSecond * Time.deltaTime;
            if (heat > maxHeat)
            {
                heat = maxHeat;
            }
        }

        public void BleedHeat()
        {
            heat -= heatBleedPerSecond * Time.deltaTime;
            if (heat < 0)
            {
                heat = 0; // Move this into the property?
            }
        }

        public void ConsumeFuel()
        {
            fuel -= fuelConsumptionPerSecond * Time.deltaTime;
            if (fuel <= 0)
            {
                fuel = 0;
                GameOverFuel();
            }
        }


        public void ConsumeFuel(float amount)
        {
            fuel -= amount;
            if (fuel <= 0)
            {
                fuel = 0;
                GameOverFuel();
            }
        }

        public void IncreaseHeat(float amount)
        {
            heat += amount;
            if (heat > maxHeat)
            {
                heat = maxHeat;
            }
        }

        public void IncreaseCargo(int amount)
        {
            cargo += amount;
            if (cargo > maxCargo)
            {
                cargo = maxCargo;
            }
        }


        public void GameOverCollision()
        {
            Debug.Log(this.name + ": Game Over via Collision");
            state = GameState.GAMEOVERCOLLISION;
            gameStarted = false;
        }

        public void GameOverMine()
        {
            Debug.Log(this.name + ": Game Over via Proximity Mine");
            state = GameState.GAMEOVERMINE;
            gameStarted = false;
        }

        public void GameOverFuel()
        {
            Debug.Log(this.name + ": Game Over via Fuel");
            state = GameState.GAMEOVERFUEL;
            gameStarted = false;
        }

        public void GameOverMissile()
        {
            Debug.Log(this.name + ": Game Over via Missile");
            state = GameState.GAMEOVERMISSILE;
            gameStarted = false;
        }

        public Missile FireMissile(Vector3 start, Vector3 direction, GameObject source, GameObject target, string type, bool hostile)
        {
            // Use the first missile without a target
            for (int i = 0; i < missileParent.transform.childCount; i++)
            {
                Missile m = missileParent.transform.GetChild(i).GetComponent<Missile>(); // FIXME: Make this more efficient
                if (m.source == null)
                {
                    IncreaseHeat(missileHeat);
                    m.Launch(start, direction, source, target, type, hostile);
                    return m;
                }
            }

            return null;
        }

        public Laser FireLaser(Vector3 start, Vector3 direction, GameObject source, bool hostile)
        {
            // Use the first missile without a target
            for (int i = 0; i < laserParent.transform.childCount; i++)
            {
                Laser l = laserParent.transform.GetChild(i).GetComponent<Laser>(); // FIXME: Make this more efficient
                if (l.source == null)
                {
                    IncreaseHeat(laserHeat);
                    ConsumeFuel(1);
                    l.Launch(start, direction, source, hostile);
                    return l;
                }
            }

            return null;
        }

        void ResetState()
        {
            Debug.Log(this.name + ": reset state");
            gameStarted = false;
            player.transform.position = Vector2.zero;
            player.transform.rotation = Quaternion.Euler(0, 0, 45.0f);
            fuel = startingFuel;
            maxFuelIncrease = 0;
            heat = startingHeat;
            credits = startingCredits;
            minCrossSectionReduction = 0;
            bayDoorsOpen = false;

            bay[0] = "AS-07 Swallow";
            bay[1] = "AS-07 Swallow";
            bay[2] = "AS-07 Swallow";
            bay[3] = "AS-07 Swallow";
            // bay[2] = "";
            // bay[3] = "";

            CurrentTarget = null;
            RefreshPrices();
            //SpawnPlanets();
            SpawnAsteroidsBig();
            //SpawnAsteroids();
            SpawnRadarSatellites();
            SpawnProxMines();
            SpawnCargoShips();
            SpawnFighterShips();
            SpawnPirateShips();
            SpawnMissiles();
            SpawnLasers();
            SpawnExplosions();
            SpawnEMPExplosions();
            SpawnStoryChars();
        }

        void SpawnStoryChars()
        {
            //float magnitude = Random.Range(10, 20);
            float magnitude = Random.Range(500, 600);
            float angle = Random.Range(0, 360.0f);
            Vector3 position = Quaternion.Euler(0, 0, angle) * (Vector2.up * magnitude);

            sparrow.transform.position = position;
            sparrow.transform.up = new Vector3(0, 0, 0) - position;
            sparrow.gameObject.SetActive(false);
            hawke.gameObject.SetActive(false);
            hawke.transform.position = position + (position.normalized * 5.0f);

            //hawke.transform.position = sparrow.transform.up - ;

            //planetParent.transform.GetChild(i).transform.position = position;
            //planetParent.transform.GetChild(i).gameObject.SetActive(false); // FIXME: deal with planets later            
        }

        // void SpawnPlanets()
        // {
        //     int planets = planetParent.transform.childCount;

        //     float angle = 360.0f / planets;
        //     for (int i = 0; i < planets; i++)
        //     {
        //         float magnitude = Random.Range(100, 1000);
        //         Vector2 position = Quaternion.Euler(0, 0, (i * angle)) * (Vector2.up * magnitude);

        //         planetParent.transform.GetChild(i).transform.position = position;
        //         planetParent.transform.GetChild(i).gameObject.SetActive(false); // FIXME: deal with planets later
        //     }
        // }

        void SpawnAsteroidsBig()
        {
            int asteroids = asteroidBigParent.transform.childCount;

            float angle = 360.0f / asteroids;
            float offset = Random.Range(0, angle);

            for (int i = 0; i < asteroids; i++)
            {
                float magnitude = Random.Range(30, 1000);

                // FIXME: this is a hack for the first mission, put this in a mission setup function
                if (i == 0)
                {
                    magnitude = 25.0f;
                }
                AsteroidBig ab = asteroidBigParent.transform.GetChild(i).GetComponent<AsteroidBig>();

                Vector2 position = Quaternion.Euler(0, 0, (i * angle) + offset) * (Vector2.up * magnitude);

                ab.exploded = false;
                ab.transform.position = position;
                ab.transform.Rotate(0, 0, Random.Range(0, 360), Space.World);
                ab.gameObject.SetActive(true);
                ab.asteroid1 = asteroidParent.transform.GetChild(2 * i).gameObject.GetComponent<Asteroid>();
                ab.asteroid1.parent = ab; // FIXME: is this circular reference an issue?
                ab.asteroid1.exploded = false;
                ab.asteroid1.gameObject.SetActive(false);
                ab.asteroid1.transform.Rotate(0, 0, Random.Range(0, 360), Space.World);
                ab.asteroid2 = asteroidParent.transform.GetChild(2 * i + 1).gameObject.GetComponent<Asteroid>();
                ab.asteroid2.parent = ab;
                ab.asteroid2.exploded = false;
                ab.asteroid2.transform.Rotate(0, 0, Random.Range(0, 360), Space.World);
                ab.asteroid2.gameObject.SetActive(false);
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnAsteroids()
        {

            int asteroids = asteroidParent.transform.childCount;


            for (int i = 0; i < asteroids; i++)
            {
                // float magnitude = Random.Range(50, 1000);
                // Vector2 position = Quaternion.Euler(0, 0, (i * angle) + offset) * (Vector2.up * magnitude);

                float rotation = Random.Range(0, 360);
                // asteroidBigParent.transform.GetChild(i).transform.position = position;
                asteroidParent.transform.GetChild(i).transform.Rotate(0, 0, rotation, Space.World);
                asteroidParent.transform.GetChild(i).gameObject.SetActive(false);
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnRadarSatellites()
        {
            int radars = radarParent.transform.childCount;

            float angle = 360.0f / radars;
            float offset = Random.Range(0, angle);

            for (int i = 0; i < radars; i++)
            {
                float magnitude = Random.Range(50, 1000);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle) + offset) * (Vector2.up * magnitude);

                //float rotation = Random.Range(0, 360);
                radarParent.transform.GetChild(i).transform.position = position;
                radarParent.transform.GetChild(i).gameObject.SetActive(true);
                //asteroidParent.transform.GetChild(i).transform.Rotate(0, 0, rotation, Space.World);
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnProxMines()
        {
            int mines = mineParent.transform.childCount;

            float angle = 360.0f / mines;
            float offset = Random.Range(0, angle);

            for (int i = 0; i < mines; i++)
            {
                ProxMine pm = mineParent.transform.GetChild(i).GetComponent<ProxMine>();
                if (pm == null)
                {
                    Debug.LogError(pm.name + ": is not set as a Prox Mine");
                    continue;
                }
                pm.Reset();
                float magnitude = Random.Range(30, 1000);
                //float magnitude = Random.Range(40, 50);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle) + offset) * (Vector2.up * magnitude);
                mineParent.transform.GetChild(i).transform.position = position;
                mineParent.transform.GetChild(i).gameObject.SetActive(true);
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnCargoShips()
        {
            int ships = cargoShipParent.transform.childCount;

            float angle = 360.0f / ships;
            float offset = Random.Range(0, angle);

            for (int i = 0; i < ships; i++)
            {
                CargoShip cargo = cargoShipParent.transform.GetChild(i).GetComponent<CargoShip>();
                if (cargo == null)
                {
                    Debug.LogError(cargo.name + ": is not set as a CargoShip");
                    continue;
                }
                float magnitude = Random.Range(70, 1000);
                //float magnitude = Random.Range(20, 50);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle) + offset) * (Vector2.up * magnitude);
                cargo.transform.up = (-1 * position).normalized;
                cargo.transform.position = position;

                float randomAngle = Random.Range(-45, 45);

                cargo.transform.Rotate(new Vector3(0, 0, randomAngle));
                cargo.patrol = new Vector2[2];
                cargo.patrol[0] = cargo.transform.up * magnitude;
                cargo.patrol[1] = cargo.transform.position;
                cargo.gameObject.SetActive(true);
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnFighterShips()
        {
            int ships = fighterParent.transform.childCount;

            float angle = 360.0f / ships;
            float offset = Random.Range(0, angle);

            for (int i = 0; i < ships; i++)
            {
                FighterShip fighter = fighterParent.transform.GetChild(i).GetComponent<FighterShip>();
                if (fighter == null)
                {
                    Debug.LogError(fighter.name + ": is not set as a FighterShip");
                    continue;
                }
                fighter.Reset();

                float magnitude = Random.Range(80, 1000);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle) + offset) * (Vector2.up * magnitude);
                fighter.transform.up = (-1 * position).normalized;
                fighter.transform.position = position;
                fighter.transform.position = position;

                float randomAngle = Random.Range(-45, 45);

                fighter.transform.Rotate(new Vector3(0, 0, randomAngle));

                fighter.patrol = new Vector2[2];
                fighter.patrol[0] = fighter.transform.up * magnitude;
                fighter.patrol[1] = fighter.transform.position;

                fighter.gameObject.SetActive(true);
                
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnPirateShips()
        {
            int ships = pirateParent.transform.childCount;

            float angle = 360.0f / ships;
            float offset = Random.Range(0, angle);

            for (int i = 0; i < ships; i++)
            {
                FighterShip fighter = pirateParent.transform.GetChild(i).GetComponent<FighterShip>();
                if (fighter == null)
                {
                    Debug.LogError(fighter.name + ": is not set as a FighterShip");
                    continue;
                }
                fighter.Reset();

                float magnitude = Random.Range(200, 1000);
                //float magnitude = Random.Range(20, 30);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle) + offset) * (Vector2.up * magnitude);
                fighter.transform.up = (-1 * position).normalized;
                fighter.transform.position = position;
                fighter.transform.position = position;

                float randomAngle = Random.Range(-45, 45);

                fighter.transform.Rotate(new Vector3(0, 0, randomAngle));

                fighter.patrol = new Vector2[2];
                fighter.patrol[0] = fighter.transform.up * magnitude;
                fighter.patrol[1] = fighter.transform.position;

                fighter.gameObject.SetActive(true);
                
                //fighter.disabled = true;
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnMissiles()
        {
            int missiles = missileParent.transform.childCount;
            for (int i = 0; i < missiles; i++)
            {
                Missile m = missileParent.transform.GetChild(i).GetComponent<Missile>();
                if (m != null)
                {
                    m.Reset();
                    m.gameObject.SetActive(false);
                    
                }
            }
        }

        void SpawnLasers()
        {
            int missiles = laserParent.transform.childCount;
            for (int i = 0; i < missiles; i++)
            {
                Laser l = laserParent.transform.GetChild(i).GetComponent<Laser>();
                if (l != null)
                {
                    l.gameObject.SetActive(false);
                    l.Reset();
                }
            }
        }

        void SpawnExplosions()
        {

            // int explosions = explosionsParent.transform.childCount;
            // for (int i = 0; i < explosions; i++)
            // {
            //     Explosion e = explosionsParent.transform.GetChild(i).GetComponent<Explosion>();
            //     if (e != null)
            //     {

            //         e.gameObject.SetActive(false);
            //         //e.Reset();
            //     }
            // }
        }

        void SpawnEMPExplosions()
        {

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
                    // If it doesn't exist anymore then it's exploded, skip to the next one
                    if (!radarParent.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        continue;
                    }

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

                if (Track > 1)
                {
                    AlertShip();
                }
            }
        }

        public void Explosion(Vector3 position)
        {
            int explosions = explosionsParent.transform.childCount;

            Explosion e = explosionsParent.transform.GetChild(currentExplosionIndex).GetComponent<Explosion>();

            currentExplosionIndex++;
            if (currentExplosionIndex >= explosions)
            {
                currentExplosionIndex = 0;
            }

            e.transform.position = position;

            float randomAngle = Random.Range(0, 360);

            e.transform.Rotate(new Vector3(0, 0, randomAngle));

            e.gameObject.SetActive(true);
            e.Play();
        }

        public void EMPExplosion(Vector3 position)
        {
            int emps = empParent.transform.childCount;

            Explosion e = empParent.transform.GetChild(currentEMPIndex).GetComponent<Explosion>();

            currentEMPIndex++;
            if (currentEMPIndex >= emps)
            {
                currentEMPIndex = 0;
            }

            e.transform.position = position;

            float randomAngle = Random.Range(0, 360);

            e.transform.Rotate(new Vector3(0, 0, randomAngle));

            e.gameObject.SetActive(true);
            e.Play();
        }

        public void AlertShip()
        {
            float distance = 100000.0f;
            radarTarget = player.transform.position;
            FighterShip selectedShip = null;

            int ships = fighterParent.transform.childCount;

            for (int i = 0; i < ships; i++)
            {
                FighterShip fighter = fighterParent.transform.GetChild(i).GetComponent<FighterShip>();

                // fighter is dead, ignore it
                if (fighter == null || !fighter.gameObject.activeInHierarchy)
                {
                    continue;
                }

                float dis = (fighter.transform.position - player.transform.position).sqrMagnitude;

                if (dis < distance)
                {
                    distance = dis;
                    selectedShip = fighter;
                }
            }

            if (selectedShip != null)
            {
                selectedShip.Hunt(player.transform.position);
                Debug.Log(this.name + ": alerting ship " + selectedShip.name);
            }
        }


        void UpdateTargetingSquare()
        {
            if (CurrentTarget != null)
            {
                targetingSquare.transform.position = CurrentTarget.transform.position;
                Renderer r = CurrentTarget.gameObject.GetComponent<Renderer>();
                targetingSquare.size = r.bounds.size;
                targetingSquare.gameObject.SetActive(true);
            }
        }

        public void RefreshPrices()
        {
            Debug.Log(this.name + ": refresh prices");
            currentFuelBuyPrice = Random.Range(fuelBuyMin, fuelBuyMax + 1);
            currentFuelSellPrice = Random.Range(fuelSellMin, fuelSellMax + 1);
            currentCargoSellPrice = Random.Range(cargoSellMin, cargoSellMax + 1);
            currentMissile1BuyPrice = Random.Range(missileDescriptions[0].buyMin, missileDescriptions[0].buyMax + 1);
            currentMissile2BuyPrice = Random.Range(missileDescriptions[1].buyMin, missileDescriptions[1].buyMax + 1);
            currentMissile3BuyPrice = Random.Range(missileDescriptions[2].buyMin, missileDescriptions[2].buyMax + 1);
            currentMissile4BuyPrice = Random.Range(missileDescriptions[3].buyMin, missileDescriptions[3].buyMax + 1);
            currentMissile1SellPrice = Random.Range(missileDescriptions[0].buyMin, missileDescriptions[0].buyMax + 1);
            currentMissile2SellPrice = Random.Range(missileDescriptions[1].buyMin, missileDescriptions[1].buyMax + 1);
            currentMissile3SellPrice = Random.Range(missileDescriptions[2].buyMin, missileDescriptions[2].buyMax + 1);
            currentMissile4SellPrice = Random.Range(missileDescriptions[3].buyMin, missileDescriptions[3].buyMax + 1);
        }


        public void CheckMarketRefresh()
        {
            if ((Time.time - lastMarketRefresh) > marketRefreshRate)
            {
                lastMarketRefresh = Time.time;
                RefreshPrices();
            }
        }


        void Update()
        {


            switch (state)
            {
                case GameState.PLAYING:
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    player.gameObject.SetActive(true);
                    //targetingSquare.gameObject.SetActive(true);
                    radarArrow.gameObject.SetActive(true);
                    missionArrow.gameObject.SetActive(true);
                    world.gameObject.SetActive(true);
                    player.UpdateActions();
                    BleedHeat();
                    UpdateRadars();
                    radarArrow.UpdatePosition();
                    missionArrow.UpdatePosition();
                    UpdateTargetingSquare();


                    break;
                case GameState.MENU:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    world.gameObject.SetActive(false);
                    break;

                case GameState.INTRO:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    player.gameObject.SetActive(true);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    world.gameObject.SetActive(true);
                    break;
                case GameState.TUTORIAL:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    player.gameObject.SetActive(true);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    world.gameObject.SetActive(true);
                    break;
                case GameState.MARKET:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    //player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    world.gameObject.SetActive(false);
                    break;

                case GameState.DIALOGUE1:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    //player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    //world.gameObject.SetActive(false);
                    break;

                case GameState.DIALOGUE2:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    //player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    //world.gameObject.SetActive(false);
                    break;

                case GameState.DIALOGUE3:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    //player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    //world.gameObject.SetActive(false);
                    break;
                case GameState.DIALOGUE4:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    //player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    //world.gameObject.SetActive(false);
                    break;
                case GameState.DIALOGUE5:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    //player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    //world.gameObject.SetActive(false);
                    break;

                case GameState.GAMEOVERSUCCESS:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    //player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    missionArrow.gameObject.SetActive(false);
                    //world.gameObject.SetActive(false);
                    break;


                case GameState.GAMEOVERCOLLISION:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    break;

                case GameState.GAMEOVERMINE:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    break;

                case GameState.GAMEOVERFUEL:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    break;
                case GameState.GAMEOVERMISSILE:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    break;
            }

            UpdateMissions();
        }


        void UpdateMissions()
        {
            switch (currentMission)
            {
                case 0:
                    UpdateMission1();
                    break;
            }
        }

        void ResetMission7Targets()
        {

            int ships = pirateParent.transform.childCount;

            for (int i = 0; i < ships; i++)
            {
                FighterShip pirate = pirateParent.transform.GetChild(i).GetComponent<FighterShip>();
                pirate.Reset();
            }
        }

        public void NextMission()
        {
            Debug.Log(this.name + ": next mission " + currentMission);
            currentMission++;
            switch (currentMission)
            {
                case 2:
                    ProxMine m = missionTargets[2].GetComponent<ProxMine>();
                    if (m != null)
                    {
                        m.Reset();
                    }
                    break;
                case 3:
                    CargoShip c = missionTargets[3].GetComponent<CargoShip>();
                    if (c != null)
                    {
                        c.Reset();
                    }
                    break;
                case 4:
                    Radar r = missionTargets[4].GetComponent<Radar>();
                    if (r != null)
                    {
                        r.Reset();
                    }
                    break;
                case 7:
                    sparrow.gameObject.SetActive(true);
                    break;
                case 8:

                    hawke.gameObject.SetActive(true);
                    var direction = hawke.transform.position - player.transform.position;
                    hawke.transform.up = direction.normalized; // Look away from the player to give them a fair chance
                    Debug.Log("Show dialogue");

                    state = GameState.DIALOGUE1;
                    break;
                case 9:
                    sparrow.gameObject.SetActive(false);
                    hawke.gameObject.SetActive(true);
                    fuel = 10;
                    cargo = 0;
                    credits = 0;
                    state = GameState.DIALOGUE4;
                    break;

            }
            //In case we want to do pre mission setup
        }

        public bool CheckMissions()
        {
            switch (currentMission)
            {
                case 0:
                    return CheckMission1();
                case 1:
                    return CheckMission2();
                case 2:
                    return CheckMission3();
                case 3:
                    return CheckMission4();
                case 4:
                    return CheckMission5();
                case 5:
                    return CheckMission6();
                case 6:
                    return CheckMission7();
                case 7:
                    return CheckMission8();
                case 8:
                    return CheckMission9();
                case 9:
                    return CheckMission10();

            }

            return false;
        }

        bool CheckMission1()
        {
            Asteroid asteroidSmall = missionTargets[0].GetComponent<Asteroid>();

            // Good start
            if (asteroidSmall != null)
            {
                AsteroidBig parent = asteroidSmall.parent;
                if (parent.exploded && parent.asteroid1.exploded && parent.asteroid2.exploded)
                {
                    //Debug.Log("Mission 1 completed");
                    return true;
                }
            }

            return false;
        }

        bool CheckMission2()
        {
            return (Game.Instance.cargo == 0);
        }

        bool CheckMission3()
        {
            ProxMine mine = missionTargets[2].GetComponent<ProxMine>();
            if (mine.exploded)
            {
                return true;
            }
            return false;
        }
        bool CheckMission4()
        {
            CargoShip c = missionTargets[3].GetComponent<CargoShip>();

            //Did we accidentally kill it earlier?
            if (!c.gameObject.activeInHierarchy)
            {
                c.gameObject.SetActive(true);

            }

            return c.disabled;
        }

        bool CheckMission5()
        {
            Radar r = missionTargets[4].GetComponent<Radar>();

            if (r != null)
            {
                return r.exploded;
            }

            return false;
        }

        bool CheckMission6()
        {
            CargoShip c = missionTargets[5].GetComponent<CargoShip>();

            //Did we accidentally kill it earlier?
            if (!c.gameObject.activeInHierarchy)
            {
                c.gameObject.SetActive(true);

            }

            return c.disabled;
            //return (credits > 30000);
            //return false;
        }

        bool CheckMission7()
        {
            FighterShip pirate1 = mission7Targets[0].GetComponent<FighterShip>();
            FighterShip pirate2 = mission7Targets[1].GetComponent<FighterShip>();
            FighterShip pirate3 = mission7Targets[2].GetComponent<FighterShip>();
            FighterShip pirate4 = mission7Targets[3].GetComponent<FighterShip>();

            return (pirate1.exploded && pirate2.exploded && pirate3.exploded && pirate4.exploded);

        }

        bool CheckMission8()
        {
            return false;
        }

        bool CheckMission9()
        {
            Hawke h = hawke.GetComponent<Hawke>();

            if (h.exploded)
            {
                return true;
            }
            return false;
        }

        bool CheckMission10()
        {
            if (!gameFinished && (player.transform.position - new Vector3(0, 0, 0)).sqrMagnitude <= 25.0f)
            {
                gameFinished = true;
                state = GameState.GAMEOVERSUCCESS; // Show the success dialogue
            }
            return (player.transform.position - new Vector3(0, 0, 0)).sqrMagnitude <= 25.0f;
        }

        void UpdateMission1()
        {
            AsteroidBig asteroidBig = missionTargets[0].GetComponent<AsteroidBig>();
            Asteroid asteroidSmall = missionTargets[0].GetComponent<Asteroid>();

            if (asteroidBig != null)
            {
                // if the current target is the big asteroid, but it's been exploded, set the current target to the first small asteroid
                if (asteroidBig.exploded)
                {
                    missionTargets[0] = asteroidBig.asteroid1.gameObject;
                }

            }

            else if (asteroidSmall != null && asteroidSmall.exploded) // currently targeting a small asteroid
            {
                // FIXME: This code makes me cry
                if (asteroidSmall == asteroidSmall.parent.asteroid1) // are we small asteroid 1
                {
                    if (asteroidSmall.parent.asteroid2.exploded)
                    {
                        // Mission complete
                        return;
                    }
                    else
                    {
                        // Set target to asteroid 2
                        missionTargets[0] = asteroidSmall.parent.asteroid2.gameObject;
                    }

                }
                else if (asteroidSmall == asteroidSmall.parent.asteroid2) // are we small asteroid 2
                {
                    if (asteroidSmall.parent.asteroid1.exploded)
                    {
                        // Mission complete
                        return;
                    }
                    else
                    {
                        // Set target to asteroid 2
                        missionTargets[0] = asteroidSmall.parent.asteroid1.gameObject;
                    }
                }

            }
        }

        void UpdateMission7()
        {

            FighterShip pirate1 = mission7Targets[0].GetComponent<FighterShip>();
            FighterShip pirate2 = mission7Targets[1].GetComponent<FighterShip>();
            FighterShip pirate3 = mission7Targets[2].GetComponent<FighterShip>();
            FighterShip pirate4 = mission7Targets[3].GetComponent<FighterShip>();

            if (!pirate1.exploded)
            {
                missionTargets[6] = pirate1.gameObject;
            }

            if (pirate1.exploded)
            {
                missionTargets[6] = pirate2.gameObject;
            }
            if (pirate2.exploded)
            {
                missionTargets[6] = pirate3.gameObject;
            }
            if (pirate3.exploded)
            {
                missionTargets[6] = null;
            }

        }


        void UpdateMissiles()
        {
            int missiles = missileParent.transform.childCount;
            for (int i = 0; i < missiles; i++)
            {
                Missile missile = missileParent.transform.GetChild(i).GetComponent<Missile>(); // FIXME: create a list at spawn
                if (missile != null)
                {
                    missile.UpdateMissile();
                }
            }
        }

        void UpdateLasers()
        {
            int lasers = laserParent.transform.childCount;
            for (int i = 0; i < lasers; i++)
            {
                Laser laser = laserParent.transform.GetChild(i).GetComponent<Laser>(); // FIXME: create a list at spawn
                if (laser != null)
                {
                    laser.UpdateMissile();
                }
            }
        }

        void UpdateFighters()
        {
            int ships = fighterParent.transform.childCount;
            for (int i = 0; i < ships; i++)
            {
                FighterShip fighter = fighterParent.transform.GetChild(i).GetComponent<FighterShip>(); // FIXME: create a list at spawn
                if (fighter != null)
                {
                    fighter.UpdateMovement();
                }
            }
        }

        void UpdatePirates()
        {
            int ships = pirateParent.transform.childCount;
            for (int i = 0; i < ships; i++)
            {
                FighterShip fighter = pirateParent.transform.GetChild(i).GetComponent<FighterShip>(); // FIXME: create a list at spawn
                if (fighter != null)
                {
                    fighter.UpdateMovement();
                }
            }
        }

        void UpdateShips()
        {
            int ships = cargoShipParent.transform.childCount;
            for (int i = 0; i < ships; i++)
            {
                CargoShip cargo = cargoShipParent.transform.GetChild(i).GetComponent<CargoShip>(); // FIXME
                if (cargo != null)
                {
                    cargo.UpdateMovement();
                }
            }
        }

        void FixedUpdate()
        {
            switch (state)
            {
                case GameState.PLAYING:
                    player.UpdateMovement();
                    UpdateMissiles();
                    UpdateLasers();
                    UpdateFighters();
                    UpdatePirates();
                    UpdateShips();
                    sparrow.UpdateMovement();
                    hawke.UpdateMovement();
                    break;
            }
        }

        void LateUpdate()
        {
            menuUI.UpdateUI();
            introUI.UpdateUI();
            gameUI.UpdateUI();
            gameOverUI.UpdateUI();
            tutorialUI.UpdateUI();
            marketUI.UpdateUI();
            dialogueUI.UpdateUI();
        }
    }

    public enum GameState
    {
        MENU,
        INTRO,
        TUTORIAL,
        MARKET,
        DIALOGUE1,
        DIALOGUE2,
        DIALOGUE3,
        DIALOGUE4,
        DIALOGUE5,
        PLAYING,
        GAMEOVERCOLLISION,
        GAMEOVERMINE,
        GAMEOVERFUEL,
        GAMEOVERMISSILE,
        GAMEOVERCARGOSHIP, // Don't blow up the cargo ship!
        GAMEOVERSUCCESS
    }
}