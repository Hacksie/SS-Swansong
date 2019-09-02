using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HackedDesign
{
    public class IntroPresenter : MonoBehaviour
    {
        IntroState state = IntroState.INTRO1;

        [SerializeField]
        private Button continueButton;        

        [SerializeField]
        private Text introText;

        [SerializeField]
        [TextArea]
        private string intro1;

        [SerializeField]
        [TextArea]
        private string intro2;


        void Start()
        {


            if (introText == null)
            {
                Debug.LogError(this.name + ": intro text is not set");
            }
            if(continueButton == null)
            {
                Debug.LogError(this.name +": continue button is not set");
            }
        }

        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.INTRO && !this.gameObject.activeInHierarchy)
            {
                Input.ResetInputAxes();
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }
            else if (Game.Instance.state != GameState.INTRO && this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(false);
                return;
            }

            if (state == IntroState.INTRO1)
            {
                introText.text = intro1;
            }
            if (state == IntroState.INTRO2)
            {
                introText.text = intro2;
            }
        }

        public void ContinueButtonEvent()
        {
            if (state == IntroState.INTRO1)
            {
                state = IntroState.INTRO2;
            }
            else
            {
                Game.Instance.ShowTutorial();
            }
        }
    }

    public enum IntroState
    {
        INTRO1,
        INTRO2
    }
}