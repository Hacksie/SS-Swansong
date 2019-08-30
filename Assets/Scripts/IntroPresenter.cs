using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class IntroPresenter : MonoBehaviour
    {

        void Start()
        {
        }

        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.INTRO && !this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(true);
            }
            else if (Game.Instance.state != GameState.INTRO && this.gameObject.activeInHierarchy)
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