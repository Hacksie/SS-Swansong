using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HackedDesign
{
    public class TutorialPresenter : MonoBehaviour
    {
        [SerializeField]
        private Button continueButton = null;

        void Start()
        {
            if (continueButton == null)
            {
                Debug.LogError(this.name + ": continue button is not set");
            }
        }

        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.TUTORIAL && !this.gameObject.activeInHierarchy)
            {
                Input.ResetInputAxes();
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }
            else if (Game.Instance.state != GameState.TUTORIAL && this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(false);
                return;
            }

        }

        public void ContinueButtonEvent()
        {
            Game.Instance.ContinueGame();
        }
    }

}