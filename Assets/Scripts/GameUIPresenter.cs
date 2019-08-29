using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class GameUIPresenter : MonoBehaviour
    {
        [SerializeField]
        private Text fuel;

        [SerializeField]
        private Text maxFuel;

        [SerializeField]
        private Text heat;

        [SerializeField]
        private Text maxHeat;    

        public void Start()
        {
            if(fuel == null)
            {
                Debug.LogError(this.name + ": fuel not set");
            }

            if(maxFuel == null)
            {
                Debug.LogError(this.name + ": maxFuel not set");
            }

            if(heat == null)
            {
                Debug.LogError(this.name + ": heat not set");
            }

            if(maxHeat == null)
            {
                Debug.LogError(this.name + ": maxHeat not set");
            }                        
            
        }


        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.PLAYING)
            {
                this.gameObject.SetActive(true);
            }

            if (Game.Instance.state == GameState.MENU)
            {
                this.gameObject.SetActive(false);
            }

            fuel.text = ((int)Game.Instance.Fuel).ToString();
            maxFuel.text = ((int)Game.Instance.MaxFuel).ToString();
            heat.text = ((int)Game.Instance.CrossSection).ToString();
            maxHeat.text = ((int)Game.Instance.maxHeat).ToString();

        } 

    }
}