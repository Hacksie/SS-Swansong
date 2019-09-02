using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HackedDesign
{
    public class GameOverPresenter : MonoBehaviour
    {

        [SerializeField]
        private Button continueButton = null;   

        [SerializeField]
        private Text gameOverText = null;
        [SerializeField]
        [TextArea]
        private string collision = "";
        [SerializeField]
        [TextArea]
        private string mine = "";
        [SerializeField]
        [TextArea]
        private string fuel = "";
        [SerializeField]
        [TextArea]
        private string missile = "";  
        [SerializeField]
        [TextArea]
        private string cargo = "";               

        void Start()
        {
            if(continueButton == null)
            {
                Debug.LogError(this.name +": continue button is not set");
            }            
            if (gameOverText == null)
            {
                Debug.LogError(this.name + ": game over text not set");
            }
        }

        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.GAMEOVERCOLLISION)
            {
                gameOverText.text = collision;
                Input.ResetInputAxes();
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }
            else if (Game.Instance.state == GameState.GAMEOVERFUEL)
            {
                gameOverText.text = fuel;
                Input.ResetInputAxes();
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }
            else if (Game.Instance.state == GameState.GAMEOVERMINE)
            {
                gameOverText.text = mine;
                Input.ResetInputAxes();
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }
            else if (Game.Instance.state == GameState.GAMEOVERMISSILE)
            {
                gameOverText.text = missile;
                Input.ResetInputAxes();
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }     
            else if (Game.Instance.state == GameState.GAMEOVERCARGOSHIP)
            {
                gameOverText.text = cargo;
                Input.ResetInputAxes();
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
            }                     
            else
            {
                this.gameObject.SetActive(false);
                return;
            }
        }

        public void EndButtonEvent()
        {
            Game.Instance.state = GameState.MENU;
        }
    }
}