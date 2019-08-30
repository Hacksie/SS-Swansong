using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class MainMenuPresenter : MonoBehaviour
    {
        public MainMenuState menuState = MainMenuState.OTHER;

        public GameObject creditsPanel;
        public GameObject optionsPanel;

        void Start()
        {
            if (optionsPanel == null)
            {
                Debug.LogError(this.name + ": options panel not set");
            }

            if (creditsPanel == null)
            {
                Debug.LogError(this.name + ": credits panel not set");
            }
        }

        public void UpdateUI()
        {
            if (Game.Instance.state != GameState.MENU && this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(false);
                return;
            }

            if (Game.Instance.state == GameState.MENU && !this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(true);
            }

            if (menuState == MainMenuState.CREDITS && (!creditsPanel.activeInHierarchy || optionsPanel.activeInHierarchy))
            {
                creditsPanel.SetActive(true);
                optionsPanel.SetActive(false);
            }
            else if (menuState == MainMenuState.OPTIONS && (creditsPanel.activeInHierarchy || !optionsPanel.activeInHierarchy))
            {
                creditsPanel.SetActive(false);
                optionsPanel.SetActive(true);
            }
            else
            {
                if ((creditsPanel.activeInHierarchy || optionsPanel.activeInHierarchy))
                {
                    creditsPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                }
            }
        }

        public void NewGameEvent()
        {
            Debug.Log(this.name + ": new game");
            Game.Instance.NewGame();
            menuState = MainMenuState.OTHER;
        }

        public void ContinueGameEvent()
        {
            Debug.Log(this.name + ": continue game");
            menuState = MainMenuState.OTHER;
        }

        public void OptionsEvent()
        {
            Debug.Log(this.name + ": options");
            menuState = MainMenuState.OPTIONS;
        }

        public void CreditsEvent()
        {
            Debug.Log(this.name + ": credits");
            menuState = MainMenuState.CREDITS;
        }

        public void ExitGameEvent()
        {
            Debug.Log(this.name + ": quitting");
            menuState = MainMenuState.OTHER;
            Application.Quit();
        }

    }

    public enum MainMenuState
    {
        OTHER,
        OPTIONS,
        CREDITS
    }
}