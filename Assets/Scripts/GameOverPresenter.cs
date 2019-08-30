using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class GameOverPresenter : MonoBehaviour
    {
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

        void Start()
        {
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
                this.gameObject.SetActive(true);
            }
            else if (Game.Instance.state == GameState.GAMEOVERFUEL)
            {
                gameOverText.text = fuel;
                this.gameObject.SetActive(true);
            }
            else if (Game.Instance.state == GameState.GAMEOVERMINE)
            {
                gameOverText.text = mine;
                this.gameObject.SetActive(true);
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