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
        private RadarArrow radarArrow = null;

        [Header("EntityPools")]
        [SerializeField]
        private SpriteRenderer targetingSquare = null;
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
        private GameObject missileParent = null;
        [SerializeField]
        private GameObject laserParent = null;
        [SerializeField]
        private GameObject explosionsParent = null;

        [Header("Information")]
        public List<MissileDescription> missileDescriptions = new List<MissileDescription>();

        [Header("Missions")]
        [SerializeField]
        private int currentMission = 0;


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

        [Header("State")]
        public GameState state;

        [SerializeField]
        public bool gameStarted = false;

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
        public float Fuel
        {
            get;
            private set;
        }

        [SerializeField]
        public float Heat
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
        public Vector3? radarTarget = null;

        public float highestRadarPulse;
        public Radar highestRadar;

        public List<GameObject> currentTargets;

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
                return (int)Mathf.Clamp(minCrossSection + minCrossSectionReduction + Heat + (bayDoorsOpen ? bayDoorsCrossSection : 0), 0, 100);
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
                Debug.LogError(this.name + ": fighters parent not set");
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
            radarArrow.gameObject.SetActive(false);
            world.gameObject.SetActive(false);
        }

        public void NewGame()
        {
            //player.gameObject.SetActive(true);
            ResetState();
            state = GameState.INTRO;
        }

        public void StartGame()
        {
            gameStarted = true;
            ContinueGame();
        }

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
            Heat += heatGainPerSecond * Time.deltaTime;
            if (Heat > maxHeat)
            {
                Heat = maxHeat;
            }
        }

        public void BleedHeat()
        {
            Heat -= heatBleedPerSecond * Time.deltaTime;
            if (Heat < 0)
            {
                Heat = 0; // Move this into the property?
            }
        }

        public void ConsumeFuel()
        {
            Fuel -= fuelConsumptionPerSecond * Time.deltaTime;
            if (Fuel <= 0)
            {
                Fuel = 0;
                GameOverFuel();
            }
        }


        public void ConsumeFuel(float amount)
        {
            Fuel -= amount;
            if (Fuel <= 0)
            {
                Fuel = 0;
                GameOverFuel();
            }
        }

        public void IncreaseHeat(float amount)
        {
            Heat += amount;
            if (Heat > maxHeat)
            {
                Heat = maxHeat;
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
            Fuel = startingFuel;
            maxFuelIncrease = 0;
            Heat = startingHeat;
            Credits = 0;
            minCrossSectionReduction = 0;
            bayDoorsOpen = false;

            bay[0] = "AS-07 Sparrow";
            bay[1] = "AS-07 Sparrow";
            bay[2] = "AS-07 Sparrow";
            bay[3] = "AS-07 Sparrow";
            // bay[2] = "";
            // bay[3] = "";

            CurrentTarget = null;
            SpawnPlanets();
            SpawnAsteroidsBig();
            SpawnAsteroids();
            SpawnRadarSatellites();
            SpawnProxMines();
            SpawnCargoShips();
            SpawnFighterShips();
            SpawnMissiles();
            SpawnLasers();
            SpawnExplosions();
        }

        void SpawnPlanets()
        {
            int planets = planetParent.transform.childCount;

            float angle = 360.0f / planets;
            for (int i = 0; i < planets; i++)
            {
                float magnitude = Random.Range(100, 1000);
                Vector2 position = Quaternion.Euler(0, 0, (i * angle)) * (Vector2.up * magnitude);

                planetParent.transform.GetChild(i).transform.position = position;
                planetParent.transform.GetChild(i).gameObject.SetActive(false); // FIXME: deal with planets later
            }
        }

        void SpawnAsteroidsBig()
        {
            int asteroids = asteroidBigParent.transform.childCount;

            float angle = 360.0f / asteroids;
            float offset = Random.Range(0, angle);

            for (int i = 0; i < asteroids; i++)
            {
                //float magnitude = Random.Range(10, 20);
                float magnitude = Random.Range(20, 1000);
                //float magnitude = Random.Range(10, 100);
                AsteroidBig ab = asteroidBigParent.transform.GetChild(i).GetComponent<AsteroidBig>();

                Vector2 position = Quaternion.Euler(0, 0, (i * angle) + offset) * (Vector2.up * magnitude);

                float rotation = Random.Range(0, 360);
                ab.transform.position = position;
                ab.transform.Rotate(0, 0, rotation, Space.World);
                ab.gameObject.SetActive(true);
                ab.asteroid1 = asteroidParent.transform.GetChild(2 * i).gameObject.GetComponent<Asteroid>();
                ab.asteroid2 = asteroidParent.transform.GetChild(2 * i + 1).gameObject.GetComponent<Asteroid>();
                // Check if there is a planet there and move if need be
            }
        }

        void SpawnAsteroids()
        {

            int asteroids = asteroidParent.transform.childCount;

            // float angle = 360.0f / asteroids;
            // float offset = Random.Range(0, angle);

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
                float magnitude = Random.Range(40, 1000);
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
                fighter.Reset();
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
                    m.gameObject.SetActive(false);
                    m.Reset();
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

            /* 
                        for (int i = 0; i < explosions; i++)
                        {
                            Explosion e = explosionsParent.transform.GetChild(i).GetComponent<Explosion>();

                            if(e.gameObject.activeInHierarchy)
                            {
                                continue;
                            }

                            e.transform.position = position;

                            float randomAngle = Random.Range(0, 360);

                            e.transform.Rotate(new Vector3(0, 0, randomAngle));                

                            e.gameObject.SetActive(true);
                            e.Play();
                            break;
                        }*/


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
                    world.gameObject.SetActive(true);
                    player.UpdateActions();
                    BleedHeat();
                    UpdateRadars();
                    radarArrow.UpdatePosition();
                    UpdateTargetingSquare();


                    break;
                case GameState.MENU:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    world.gameObject.SetActive(false);
                    break;

                case GameState.INTRO:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    world.gameObject.SetActive(false);
                    break;
                case GameState.TUTORIAL:
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    player.gameObject.SetActive(false);
                    targetingSquare.gameObject.SetActive(false);
                    radarArrow.gameObject.SetActive(false);
                    world.gameObject.SetActive(false);
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
                    UpdateShips();
                    break;
            }
        }

        void UpdateMissiles()
        {
            int missiles = missileParent.transform.childCount;
            for (int i = 0; i < missiles; i++)
            {
                Missile m = missileParent.transform.GetChild(i).GetComponent<Missile>(); // FIXME: create a list at spawn
                m.UpdateMissile();
                //missileParent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        void UpdateLasers()
        {
            int lasers = laserParent.transform.childCount;
            for (int i = 0; i < lasers; i++)
            {
                Laser l = laserParent.transform.GetChild(i).GetComponent<Laser>(); // FIXME: create a list at spawn
                l.UpdateMissile();
            }
        }

        void UpdateFighters()
        {
            int ships = fighterParent.transform.childCount;
            for (int i = 0; i < ships; i++)
            {
                FighterShip fighter = fighterParent.transform.GetChild(i).GetComponent<FighterShip>(); // FIXME
                fighter.UpdateMovement();
            }
        }

        void UpdateShips()
        {
            int ships = cargoShipParent.transform.childCount;
            for (int i = 0; i < ships; i++)
            {
                CargoShip cargo = cargoShipParent.transform.GetChild(i).GetComponent<CargoShip>(); // FIXME
                cargo.UpdateMovement();
            }
        }

        void LateUpdate()
        {
            menuUI.UpdateUI();
            introUI.UpdateUI();
            gameUI.UpdateUI();
            gameOverUI.UpdateUI();

        }
    }

    public enum GameState
    {
        MENU,
        INTRO,
        TUTORIAL,
        PLAYING,
        GAMEOVERCOLLISION,
        GAMEOVERMINE,
        GAMEOVERFUEL,
        GAMEOVERMISSILE
    }
}