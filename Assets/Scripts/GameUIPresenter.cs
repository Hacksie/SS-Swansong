using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class GameUIPresenter : MonoBehaviour
    {
        [SerializeField]
        private Text xcoord;

        [SerializeField]
        private Text ycoord;

        [SerializeField]
        private Text fuel;

        [SerializeField]
        private Text maxFuel;

        [SerializeField]
        private Text heat;

        [SerializeField]
        private Text maxHeat;

        [SerializeField]
        private Text cargo;

        [SerializeField]
        private Text maxCargo;

        [SerializeField]
        private Text credits;

        [SerializeField]
        private Text velocity;

        public void Start()
        {
            if (fuel == null)
            {
                Debug.LogError(this.name + ": fuel not set");
            }
            if (maxFuel == null)
            {
                Debug.LogError(this.name + ": maxFuel not set");
            }
            if (heat == null)
            {
                Debug.LogError(this.name + ": heat not set");
            }
            if (maxHeat == null)
            {
                Debug.LogError(this.name + ": maxHeat not set");
            }
            if (xcoord == null)
            {
                Debug.LogError(this.name + ": xcoord not set");
            }
            if (ycoord == null)
            {
                Debug.LogError(this.name + ": ycoord not set");
            }
            if (velocity == null)
            {
                Debug.LogError(this.name + ": velocity not set");
            }
            if (cargo == null)
            {
                Debug.LogError(this.name = ": cargo not set");
            }
            if(maxCargo == null)
            {
                Debug.LogError(this.name = ": max cargo not set");
            }
            if(credits == null)
            {
                Debug.LogError(this.name = ": credits not set");
            }
        }


        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.PLAYING)
            {
                this.gameObject.SetActive(true);
            }

            if (Game.Instance.state == GameState.MENU)
            {
                this.gameObject.SetActive(false);
                return;
            }

            xcoord.text = ((int)Game.Instance.player.transform.position.x).ToString();
            ycoord.text = ((int)Game.Instance.player.transform.position.y).ToString();
            velocity.text = ((int)(Game.Instance.player.Velocity * 100.0f / Game.Instance.player.MaxThrust)).ToString() + "%";
            fuel.text = ((int)Game.Instance.Fuel).ToString();
            maxFuel.text = ((int)Game.Instance.MaxFuel).ToString();
            heat.text = ((int)Game.Instance.CrossSection).ToString();
            maxHeat.text = ((int)Game.Instance.maxHeat).ToString();
            credits.text = "$" + Game.Instance.Credits.ToString();
            cargo.text = Game.Instance.Cargo.ToString();
            maxCargo.text = Game.Instance.maxCargo.ToString();

        }

    }
}