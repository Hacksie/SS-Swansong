using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class MarketPresenter : MonoBehaviour
    {

        void Start()
        {
        }

        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.MARKET && !this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(true);
            }
            else if (Game.Instance.state != GameState.MARKET && this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(false);

            }
        }

        public void ContinueButtonEvent()
        {
            Game.Instance.ContinueGame();
        }
    }
}