using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class MarketPresenter : MonoBehaviour
    {
        [SerializeField]
        Text fuelBuyPriceText;
        [SerializeField]
        Text fuelAllBuyPriceText;

        [SerializeField]
        Text fuelSellPriceText;
        [SerializeField]
        Text fuel100SellPriceText;


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
            if(fuelBuyPriceText == null)
            {
                Debug.LogError(this.name + ": fuelBuyPrice text is not set");
            }
            if(fuelSellPriceText == null)
            {
                Debug.LogError(this.name + ": fuel sell price text not set");
            }
            if(fuel100SellPriceText == null)
            {
                Debug.LogError(this.name + ": fuel100 sell price text not set");
            }            
        
        }



        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.MARKET) // && !this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(true);
            }
            else if (Game.Instance.state != GameState.MARKET) // FIXME: Handle return && this.gameObject.activeInHierarchy
            {
                this.gameObject.SetActive(false);
                return;
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

            Game.Instance.CheckMarketRefresh();


            UpdateBays();
            UpdatePrices();
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
            fuelBuyPriceText.text = "$" + (Game.Instance.currentFuelBuyPrice *10);
            fuelAllBuyPriceText.text = "$" + Mathf.CeilToInt(remainingFuel * Game.Instance.currentFuelBuyPrice);
            fuelSellPriceText.text = "$" + (Game.Instance.currentFuelSellPrice * 10);
            fuel100SellPriceText.text = "$" + (Game.Instance.currentFuelSellPrice * 100);

        }



        public void BuyFuelEvent()
        {
            Debug.Log(this.name + ": buy fuel");
  
            if(Game.Instance.MaxFuel - Game.Instance.fuel > 10 && (Game.Instance.currentFuelBuyPrice *10 <= Game.Instance.credits))
            {
                Game.Instance.credits -= Game.Instance.currentFuelBuyPrice *10;
                Game.Instance.fuel += 10;
            }

        }

        public void BuyFuelAllEvent()
        {
            Debug.Log(this.name + ": buy fuel all");
            float remainingFuel = Game.Instance.MaxFuel - Game.Instance.fuel;
            int price = Mathf.CeilToInt(remainingFuel * Game.Instance.currentFuelBuyPrice);
            if(Game.Instance.currentFuelBuyPrice *price <= Game.Instance.credits)
            {
                Game.Instance.credits -= price;
                Game.Instance.fuel = Game.Instance.MaxFuel;
            }

        }        

        public void BuyMissile1Event()
        {
            Debug.Log(this.name + ": buy missile 1");
        }

        public void BuyMissile2Event()
        {
            Debug.Log(this.name + ": buy missile 2");
        }

        public void BuyMissile3Event()
        {
            Debug.Log(this.name + ": buy missile 3");
        }

        public void BuyMissile4Event()
        {
            Debug.Log(this.name + ": buy missile 4");
        }

        public void SellFuelEvent()
        {
            //Check 0
            Debug.Log(this.name + ": sell fuel");
        }

        public void SellFuel100Event()
        {
            //Check 0
            Debug.Log(this.name + ": sell fuel 100");
        }  

        public void SellCargo100Event()
        {
            Debug.Log(this.name + ": sell cargo");
        }

        public void SellCargoAllEvent()
        {
            Debug.Log(this.name + ": sell cargo all");
        }
           
        public void SellBay1Event()
        {
            Debug.Log(this.name + ": sell bay1");
        }

        public void SellBay2Event()
        {
            Debug.Log(this.name + ": sell bay2");
        }

        public void SellBay3Event()
        {
            Debug.Log(this.name + ": sell bay3");
        }

        public void SellBay4Event()
        {
            Debug.Log(this.name + ": sell bay4");
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