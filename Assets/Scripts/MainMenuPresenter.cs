using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class MainMenuPresenter : MonoBehaviour
    {


        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.PLAYING  || Game.Instance.state == GameState.WARPZONE)
            {
                this.gameObject.SetActive(false);
            }

            if (Game.Instance.state == GameState.MENU)
            {
                this.gameObject.SetActive(true);
            }
        }

        public void NewGameEvent()
        {
            Debug.Log(this.name + ": new game");
            Game.Instance.NewGame();
        }

        public void ContinueGameEvent()
        {
            Debug.Log(this.name + ": continue game");
        }        

        public void OptionsEvent()
        {
            Debug.Log(this.name + ": options");
        }                

        public void CreditsEvent()
        {
            Debug.Log(this.name + ": options");
        }                

        public void ExitGameEvent()
        {
            Debug.Log(this.name + ": quitting");
            Application.Quit();
        }

    }
}