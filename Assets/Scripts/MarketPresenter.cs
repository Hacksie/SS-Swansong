using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HackedDesign
{
    public class MarketPresenter : MonoBehaviour
    {
        [SerializeField]
        private Button continueButton;

        [SerializeField]
        private Color activeColour = Color.white;

        [SerializeField]
        private Color completedColour = Color.grey;

        [SerializeField]
        private Color notCompletedColour = new Color(0, 0, 0, 0);

        [SerializeField]
        private Text[] missionShortDescription = new Text[10];

        [SerializeField]
        private Text missionLongDescription;


        [SerializeField]
        Text fuelBuyPriceText;
        [SerializeField]
        Text fuelAllBuyPriceText;

        [SerializeField]
        Text fuelSellPriceText;
        [SerializeField]
        Text fuel100SellPriceText;

        [SerializeField]
        Text cargoSellPriceText;
        [SerializeField]
        Text cargoAllSellPriceText;


        [SerializeField]
        private Text bay1SelectedText;

        [SerializeField]
        private Text bay2SelectedText;

        [SerializeField]
        private Text bay3SelectedText;

        [SerializeField]
        private Text bay4SelectedText;

        [SerializeField]
        private Text bay1MissileText;

        [SerializeField]
        private Text bay2MissileText;

        [SerializeField]
        private Text bay3MissileText;

        [SerializeField]
        private Text bay4MissileText;

        [SerializeField]
        Color baySelectedColour = Color.red;

        [SerializeField]
        Color bayUnselectedColour = Color.white;


        void Start()
        {
            if (continueButton == null)
            {
                Debug.LogError(this.name + ": continue button is not set");
            }


            if (bay1SelectedText == null)
            {
                Debug.LogError(this.name + ": bay1selected text is not set");
            }
            if (bay2SelectedText == null)
            {
                Debug.LogError(this.name + ": bay2selected text is not set");
            }
            if (bay3SelectedText == null)
            {
                Debug.LogError(this.name + ": bay3selected text is not set");
            }
            if (bay4SelectedText == null)
            {
                Debug.LogError(this.name + ": bay4selected text is not set");
            }
            if (bay1MissileText == null)
            {
                Debug.LogError(this.name + ": bay1 missile text is not set");
            }
            if (bay2MissileText == null)
            {
                Debug.LogError(this.name + ": bay2 missile text is not set");
            }
            if (bay3MissileText == null)
            {
                Debug.LogError(this.name + ": bay3 missile text is not set");
            }
            if (bay4MissileText == null)
            {
                Debug.LogError(this.name + ": bay4 missile text is not set");
            }
            if (fuelBuyPriceText == null)
            {
                Debug.LogError(this.name + ": fuelBuyPrice text is not set");
            }
            if (fuelSellPriceText == null)
            {
                Debug.LogError(this.name + ": fuel sell price text not set");
            }
            if (fuel100SellPriceText == null)
            {
                Debug.LogError(this.name + ": fuel100 sell price text not set");
            }
            if (cargoSellPriceText == null)
            {
                Debug.LogError(this.name + ": cargo sell price text not set");
            }
            if (cargoAllSellPriceText == null)
            {
                Debug.LogError(this.name + ": cargo all sell price text not set");
            }

            for (int i = 0; i < 10; i++)
            {
                if (missionShortDescription[i] == null)
                {
                    Debug.LogError(this.name + ": short desc " + i + " not set");
                }

                if (missionLongDescription == null)
                {
                    Debug.LogError(this.name + ": long desc not set");
                }
            }

        }



        public void UpdateUI()
        {
            if (Game.Instance.state != GameState.MARKET)
            {
                if (this.gameObject.activeInHierarchy)
                {
                    this.gameObject.SetActive(false);
                }
                return;
            }

            if (Game.Instance.state == GameState.MARKET && !this.gameObject.activeInHierarchy)
            {
                Input.ResetInputAxes();
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }


            if (Input.GetButtonUp("Next Weapon"))
            {
                Game.Instance.currentBay++;
                if (Game.Instance.currentBay >= 4)
                {
                    Game.Instance.currentBay = 0;
                }
            }
            if (Input.GetButtonUp("Previous Weapon"))
            {
                Game.Instance.currentBay--;
                if (Game.Instance.currentBay < 0)
                {
                    Game.Instance.currentBay = 3;
                }
            }

            if (Game.Instance.CheckMissions())
            {
                Game.Instance.currentMission++;
                //Show mission complete!
            }

            Game.Instance.CheckMarketRefresh();

            UpdateMissions();
            UpdateBays();
            UpdatePrices();
        }

        void UpdateMissions()
        {
            for (int i = 0; i < 10; i++)
            {

                if (i < Game.Instance.currentMission)
                {
                    missionShortDescription[i].text = Game.Instance.missionDescriptions[i].shortDescription;
                    missionShortDescription[i].color = completedColour;
                }
                else if (i == Game.Instance.currentMission)
                {
                    missionShortDescription[i].text = Game.Instance.missionDescriptions[i].shortDescription;
                    missionShortDescription[i].color = activeColour;
                }
                else if (i > Game.Instance.currentMission)
                {
                    missionShortDescription[i].text = "";
                    missionShortDescription[i].color = notCompletedColour;
                }

            }
        }

        void UpdateBays()
        {
            bay1SelectedText.color = Game.Instance.currentBay == 0 ? baySelectedColour : bayUnselectedColour;
            bay2SelectedText.color = Game.Instance.currentBay == 1 ? baySelectedColour : bayUnselectedColour;
            bay3SelectedText.color = Game.Instance.currentBay == 2 ? baySelectedColour : bayUnselectedColour;
            bay4SelectedText.color = Game.Instance.currentBay == 3 ? baySelectedColour : bayUnselectedColour;
            bay1MissileText.text = !string.IsNullOrWhiteSpace(Game.Instance.bay[0]) ? Game.Instance.bay[0] : "empty";
            bay2MissileText.text = !string.IsNullOrWhiteSpace(Game.Instance.bay[1]) ? Game.Instance.bay[1] : "empty";
            bay3MissileText.text = !string.IsNullOrWhiteSpace(Game.Instance.bay[2]) ? Game.Instance.bay[2] : "empty";
            bay4MissileText.text = !string.IsNullOrWhiteSpace(Game.Instance.bay[3]) ? Game.Instance.bay[3] : "empty";
        }

        void UpdatePrices()
        {
            float remainingFuel = Game.Instance.MaxFuel - Game.Instance.fuel;
            float totalCargo = Game.Instance.cargo;



            fuelBuyPriceText.text = "$" + (Game.Instance.currentFuelBuyPrice * 10);

            fuelSellPriceText.text = "$" + (Game.Instance.currentFuelSellPrice * 10);
            fuel100SellPriceText.text = "$" + (Game.Instance.currentFuelSellPrice * 100);
            cargoSellPriceText.text = "$" + (Game.Instance.currentCargoSellPrice * 100);
            cargoAllSellPriceText.text = "$" + (Game.Instance.currentCargoSellPrice * totalCargo);

            if (Mathf.CeilToInt(remainingFuel * Game.Instance.currentFuelBuyPrice) <= Game.Instance.credits)
            {
                fuelAllBuyPriceText.text = "$" + Mathf.CeilToInt(remainingFuel * Game.Instance.currentFuelBuyPrice);
            }
            else
            {
                fuelAllBuyPriceText.text = "$" + Game.Instance.credits;
            }

        }



        public void BuyFuelEvent()
        {
            Debug.Log(this.name + ": buy fuel");

            if (Game.Instance.MaxFuel - Game.Instance.fuel > 10 && (Game.Instance.currentFuelBuyPrice * 10 <= Game.Instance.credits))
            {
                Game.Instance.credits -= Game.Instance.currentFuelBuyPrice * 10;
                Game.Instance.fuel += 10;
            }

        }

        public void BuyFuelAllEvent()
        {
            Debug.Log(this.name + ": buy fuel all");
            float remainingFuel = Game.Instance.MaxFuel - Game.Instance.fuel;
            int price = Mathf.CeilToInt(remainingFuel * Game.Instance.currentFuelBuyPrice);
            if (Game.Instance.currentFuelBuyPrice * price <= Game.Instance.credits)
            {
                Game.Instance.credits -= price;
                Game.Instance.fuel = Game.Instance.MaxFuel;
            }
            else
            {
                Game.Instance.fuel += ((float)Game.Instance.credits) / Game.Instance.currentFuelBuyPrice;
                Game.Instance.credits = 0;

                //Debug.Log(this.name + ": " + Game.Instance.credits / Game.Instance.currentFuelBuyPrice);
            }

        }

        public void BuyMissile1Event()
        {
            Debug.Log(this.name + ": buy missile 1");
            if (string.IsNullOrWhiteSpace(Game.Instance.bay[Game.Instance.currentBay]))
            {
                Game.Instance.bay[Game.Instance.currentBay] = "AS-07 Swallow";
            }
        }

        public void BuyMissile2Event()
        {
            Debug.Log(this.name + ": buy missile 2");
            if (string.IsNullOrWhiteSpace(Game.Instance.bay[Game.Instance.currentBay]))
            {
                Game.Instance.bay[Game.Instance.currentBay] = "AS-39 Gyrfalcon";
            }
        }

        public void BuyMissile3Event()
        {
            Debug.Log(this.name + ": buy missile 3");
            if (string.IsNullOrWhiteSpace(Game.Instance.bay[Game.Instance.currentBay]))
            {
                Game.Instance.bay[Game.Instance.currentBay] = "ES-23 Harpoon";
            }
        }

        public void BuyMissile4Event()
        {
            Debug.Log(this.name + ": buy missile 4");
            if (string.IsNullOrWhiteSpace(Game.Instance.bay[Game.Instance.currentBay]))
            {
                Game.Instance.bay[Game.Instance.currentBay] = "RM-44 Rook";
            }
        }

        public void SellFuelEvent()
        {
            //Check 0
            Debug.Log(this.name + ": sell fuel");

            if (Game.Instance.fuel - 10 > 0)
            {
                Game.Instance.credits += Game.Instance.currentFuelSellPrice * 10;
                Game.Instance.fuel -= 10;
            }

            // if(Game.Instance.MaxFuel - Game.Instance.fuel > 10 && (Game.Instance.currentFuelBuyPrice *10 <= Game.Instance.credits))
            // {
            //     Game.Instance.credits -= Game.Instance.currentFuelBuyPrice *10;
            //     Game.Instance.fuel += 10;
            // }            
        }

        public void SellFuel100Event()
        {
            //Check 0
            Debug.Log(this.name + ": sell fuel 100");
            if (Game.Instance.fuel - 100 > 0)
            {
                Game.Instance.credits += Game.Instance.currentFuelSellPrice * 100;
                Game.Instance.fuel -= 100;
            }
        }

        public void SellCargo100Event()
        {
            Debug.Log(this.name + ": sell cargo");
            if (Game.Instance.cargo - 100 > 0)
            {
                Game.Instance.credits += Game.Instance.currentCargoSellPrice * 100;
                Game.Instance.cargo -= 100;
            }
        }

        public void SellCargoAllEvent()
        {
            Debug.Log(this.name + ": sell cargo all");
            Game.Instance.credits += Game.Instance.currentCargoSellPrice * Game.Instance.cargo;
            Game.Instance.cargo = 0;
        }

        public void SellBay1Event()
        {
            Debug.Log(this.name + ": sell bay1");
            if (!string.IsNullOrWhiteSpace(Game.Instance.bay[0]))
            {
                Game.Instance.bay[0] = "";
            }
        }

        public void SellBay2Event()
        {
            Debug.Log(this.name + ": sell bay2");
            if (!string.IsNullOrWhiteSpace(Game.Instance.bay[1]))
            {
                Game.Instance.bay[1] = "";
            }
        }

        public void SellBay3Event()
        {
            Debug.Log(this.name + ": sell bay3");
            if (!string.IsNullOrWhiteSpace(Game.Instance.bay[2]))
            {
                Game.Instance.bay[2] = "";
            }
        }

        public void SellBay4Event()
        {
            Debug.Log(this.name + ": sell bay4");
            if (!string.IsNullOrWhiteSpace(Game.Instance.bay[3]))
            {
                Game.Instance.bay[3] = "";
            }
        }

        public void SelectBay1Event()
        {
            Game.Instance.currentBay = 0;
        }

        public void SelectBay2Event()
        {
            Game.Instance.currentBay = 1;
        }

        public void SelectBay3Event()
        {
            Game.Instance.currentBay = 2;
        }

        public void SelectBay4Event()
        {
            Game.Instance.currentBay = 3;

        }




        public void ContinueButtonEvent()
        {
            Game.Instance.ContinueGame();
        }
    }
}