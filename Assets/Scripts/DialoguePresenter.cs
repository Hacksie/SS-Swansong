using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HackedDesign
{
    public class DialoguePresenter : MonoBehaviour
    {

        [SerializeField]
        private Button continueButton = null;

        [SerializeField]
        private Text dialogueTitleText = null;
        [SerializeField]
        private Text dialogueText = null;
        [SerializeField]
        [TextArea]
        private string dialogueTitle1 = "";
        [SerializeField]
        [TextArea]
        private string dialogue1 = "";
        [SerializeField]
        [TextArea]
        private string dialogueTitle2 = "";
        [SerializeField]
        [TextArea]
        private string dialogue2 = "";
        [SerializeField]
        [TextArea]
        private string dialogueTitle3 = "";
        [SerializeField]
        [TextArea]
        private string dialogue3 = "";
        [SerializeField]
        [TextArea]
        private string dialogueTitle4 = "";
        [SerializeField]
        [TextArea]
        private string dialogue4 = "";
        [SerializeField]
        [TextArea]
        private string dialogueTitle5 = "";
        [SerializeField]
        [TextArea]
        private string dialogue5 = "";
        [SerializeField]
        [TextArea]
        private string dialogueTitleSuccess = "";
        [SerializeField]
        [TextArea]
        private string dialogueSuccess = "";
        [SerializeField]
        [TextArea]
        private string dialogueTitleCargo = "";
        [SerializeField]
        [TextArea]
        private string dialogueCargo = "";

        void Start()
        {
            if (continueButton == null)
            {
                Debug.LogError(this.name + ": continue button is not set");
            }
            if (dialogueText == null)
            {
                Debug.LogError(this.name + ": dialogue text not set");
            }
            if (dialogueTitleText == null)
            {
                Debug.LogError(this.name + ": dialogue title text not set");
            }
        }

        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.DIALOGUE1)
            {
                dialogueText.text = dialogue1;
                dialogueTitleText.text = dialogueTitle1;

                if (!this.gameObject.activeInHierarchy)
                {


                    Input.ResetInputAxes();
                    this.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
                }
            }
            else if (Game.Instance.state == GameState.DIALOGUE2)
            {
                dialogueText.text = dialogue2;
                dialogueTitleText.text = dialogueTitle2;
                if (!this.gameObject.activeInHierarchy)
                {

                    Input.ResetInputAxes();
                    this.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
                }
            }
            else if (Game.Instance.state == GameState.DIALOGUE3)
            {
                dialogueText.text = dialogue3;
                dialogueTitleText.text = dialogueTitle3;
                if (!this.gameObject.activeInHierarchy)
                {

                    Input.ResetInputAxes();
                    this.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
                }
            }
            else if (Game.Instance.state == GameState.DIALOGUE4)
            {
                dialogueText.text = dialogue4;
                dialogueTitleText.text = dialogueTitle4;
                if (!this.gameObject.activeInHierarchy)
                {

                    Input.ResetInputAxes();
                    this.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
                }
            }
            else if (Game.Instance.state == GameState.DIALOGUE5)
            {
                dialogueText.text = dialogue5;
                dialogueTitleText.text = dialogueTitle5;
                if (!this.gameObject.activeInHierarchy)
                {

                    Input.ResetInputAxes();
                    this.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
                }
            }
            else if (Game.Instance.state == GameState.GAMEOVERSUCCESS)
            {
                dialogueText.text = dialogueSuccess;
                dialogueTitleText.text = dialogueTitleSuccess;
                if (!this.gameObject.activeInHierarchy)
                {

                    Input.ResetInputAxes();
                    this.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
                }
            }
            else if (Game.Instance.state == GameState.DIALOGUECARGOSHIP)
            {
                dialogueText.text = dialogueCargo;
                dialogueTitleText.text = dialogueTitleCargo;
                if (!this.gameObject.activeInHierarchy)
                {
                    //gameOverText.text = cargo;
                    Input.ResetInputAxes();
                    this.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
                }
            }

            else
            {
                this.gameObject.SetActive(false);
                return;
            }
        }

        public void EndButtonEvent()
        {
            Debug.Log(this.name + ": dialogue continue");
            // This code makes me cry
            if (Game.Instance.state == GameState.GAMEOVERSUCCESS)
            {
                Game.Instance.ContinueGame();
            }

            if (Game.Instance.state == GameState.DIALOGUECARGOSHIP)
            {
                Game.Instance.ContinueGame();
            }


            if (Game.Instance.state == GameState.DIALOGUE5)
            {
                Game.Instance.ContinueGame();
            }
            if (Game.Instance.state == GameState.DIALOGUE4)
            {
                Game.Instance.state = GameState.DIALOGUE5;
            }
            if (Game.Instance.state == GameState.DIALOGUE3)
            {
                Game.Instance.ContinueGame();
                //Game.Instance.state = GameState.DIALOGUE4;
            }
            if (Game.Instance.state == GameState.DIALOGUE2)
            {
                Game.Instance.state = GameState.DIALOGUE3;
            }
            if (Game.Instance.state == GameState.DIALOGUE1)
            {
                //Game.Instance.ContinueGame();
                Game.Instance.state = GameState.DIALOGUE2;
            }
        }
    }
}