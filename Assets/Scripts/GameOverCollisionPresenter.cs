using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class GameOverCollisionPresenter : MonoBehaviour
    {
        
        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.GAMEOVERCOLLISION)
            {
                this.gameObject.SetActive(true);
            }
            else {
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