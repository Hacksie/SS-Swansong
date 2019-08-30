using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class GameUIPresenter : MonoBehaviour
    {
        [SerializeField]
        private Text xcoord = null;

        [SerializeField]
        private Text ycoord = null;

        [SerializeField]
        private Text fuel = null;

        [SerializeField]
        private Text maxFuel = null;

        [SerializeField]
        private Text cargo = null;

        [SerializeField]
        private Text maxCargo = null;

        [SerializeField]
        private Text credits = null;

        [SerializeField]
        private Text velocity = null;

        [SerializeField]
        private RectTransform xsectionBar = null; 

        [SerializeField]
        private RectTransform radarBar = null;         

        [SerializeField]
        private Text tracked = null;      

        [SerializeField]
        private Color notTrackedColour = Color.gray;

        [SerializeField]
        private Color trackedColour = Color.red;        

        [SerializeField]
        private Text bayDoors = null;        


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
                Debug.LogError(this.name + ": cargo not set");
            }
            if(maxCargo == null)
            {
                Debug.LogError(this.name + ": max cargo not set");
            }
            if(credits == null)
            {
                Debug.LogError(this.name + ": credits not set");
            }

            if(xsectionBar == null)
            {
                Debug.LogError(this.name + ": xsectionbar not set");
            }
            if(radarBar == null)
            {
                Debug.LogError(this.name + ": radarbar not set");
            }
            if(tracked == null)
            {
                Debug.LogError(this.name + ": tracked not set");
            }
            if(bayDoors == null)
            {
                Debug.LogError(this.name + ": baydoors not set");
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
            // heat.text = ((int)Game.Instance.CrossSection).ToString();
            // maxHeat.text = ((int)Game.Instance.maxHeat).ToString();
            credits.text = "$" + Game.Instance.Credits.ToString();
            cargo.text = Game.Instance.cargo.ToString();
            maxCargo.text = Game.Instance.maxCargo.ToString();
            bayDoors.text = Game.Instance.bayDoorsOpen ? "OPEN" : "CLOSED";

            xsectionBar.sizeDelta = new Vector2((int)(Game.Instance.CrossSection * 70 / 100), 10.0f);

            if(Game.Instance.Track > 1)
            {
                tracked.color = trackedColour;
            }
            else
            {
                tracked.color = notTrackedColour;
            }

            if(Game.Instance.highestRadar != null)
            {
                //Debug.Log("set radar bar");
                radarBar.gameObject.SetActive(true);
                float distance = Mathf.Clamp(100 - (Game.Instance.highestRadar.transform.position - Game.Instance.player.transform.position).magnitude, 0, 100);
                radarBar.sizeDelta = new Vector2((int)(distance * 70 / 100), 4.0f);
            }
            else {
                radarBar.gameObject.SetActive(false);

            }

        }
    }
}