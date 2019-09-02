using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HackedDesign
{
    public class MainMenuPresenter : MonoBehaviour
    {
        private MainMenuState menuState = MainMenuState.OTHER;

        [SerializeField]
        private GameObject creditsPanel = null;
        [SerializeField]
        private GameObject optionsPanel = null;
        [SerializeField]
        private Button newButton  = null;
        [SerializeField]
        private Button continueButton = null;

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
            if (newButton == null)
            {
                Debug.LogError(this.name + ": newButton not set");
            }
            if (continueButton == null)
            {
                Debug.LogError(this.name + ": continueButton not set");
            }
        }

        public void UpdateUI()
        {
            //if()
            if (Game.Instance.state != GameState.MENU && this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(false);
                return;
            }

            if (Game.Instance.state == GameState.MENU && !this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(newButton.gameObject);
                //SetSelectedGameObject();

            }

            if (menuState == MainMenuState.CREDITS)
            {
                creditsPanel.SetActive(true);
                optionsPanel.SetActive(false);
            }
            else if (menuState == MainMenuState.OPTIONS)
            {
                creditsPanel.SetActive(false);
                optionsPanel.SetActive(true);
            }
            else
            {
                if (menuState == MainMenuState.OTHER && (creditsPanel.activeInHierarchy || optionsPanel.activeInHierarchy))
                {
                    creditsPanel.SetActive(false);
                    optionsPanel.SetActive(false);
                }
            }

            continueButton.interactable = Game.Instance.gameStarted;
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
            Game.Instance.ContinueGame();
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