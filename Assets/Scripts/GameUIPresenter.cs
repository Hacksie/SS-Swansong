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

        [SerializeField]
        private Text bay1 = null;

        [SerializeField]
        private Text bay2 = null;

        [SerializeField]
        private Text bay3 = null;

        [SerializeField]
        private Text bay4 = null;

        [SerializeField]
        private Text cargo = null;

        [SerializeField]
        private Text maxCargo = null;

        [SerializeField]
        private Text target = null;

        [SerializeField]
        private Color notCurrentBayColour = Color.white;

        [SerializeField]
        private Color currentBayColour = Color.red;

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
            if (credits == null)
            {
                Debug.LogError(this.name + ": credits not set");
            }

            if (xsectionBar == null)
            {
                Debug.LogError(this.name + ": xsectionbar not set");
            }
            if (radarBar == null)
            {
                Debug.LogError(this.name + ": radarbar not set");
            }
            if (tracked == null)
            {
                Debug.LogError(this.name + ": tracked not set");
            }
            if (bayDoors == null)
            {
                Debug.LogError(this.name + ": baydoors not set");
            }
            if (bay1 == null)
            {
                Debug.LogError(this.name + ": bay1 not set");
            }
            if (bay2 == null)
            {
                Debug.LogError(this.name + ": bay2 not set");
            }
            if (bay3 == null)
            {
                Debug.LogError(this.name + ": bay3 not set");
            }
            if (bay4 == null)
            {
                Debug.LogError(this.name + ": bay4 not set");
            }
            if (cargo == null)
            {
                Debug.LogError(this.name + ": cargo not set");
            }
            if (maxCargo == null)
            {
                Debug.LogError(this.name + ": cargo max not set");
            }

            if (target == null)
            {
                Debug.LogError(this.name = ": target not set");
            }
        }


        public void UpdateUI()
        {
            if (Game.Instance.state != GameState.PLAYING && Game.Instance.state != GameState.MISSIONS && Game.Instance.state != GameState.MARKET)
            {
                this.gameObject.SetActive(false);
                return;
            }

            if (Game.Instance.state == GameState.PLAYING || Game.Instance.state == GameState.MISSIONS || Game.Instance.state == GameState.MARKET)
            {
                this.gameObject.SetActive(true);
            }


            xcoord.text = ((int)Game.Instance.player.transform.position.x).ToString();
            ycoord.text = ((int)Game.Instance.player.transform.position.y).ToString();
            velocity.text = ((int)(Game.Instance.player.Velocity * 100.0f / Game.Instance.player.MaxThrust)).ToString() + "%";
            fuel.text = ((int)Game.Instance.fuel).ToString();
            maxFuel.text = ((int)Game.Instance.MaxFuel).ToString();
            cargo.text = Game.Instance.cargo.ToString();
            maxCargo.text = Game.Instance.maxCargo.ToString();
            // heat.text = ((int)Game.Instance.CrossSection).ToString();
            // maxHeat.text = ((int)Game.Instance.maxHeat).ToString();
            credits.text = "$" + Game.Instance.credits.ToString();
            bayDoors.text = Game.Instance.bayDoorsOpen ? "OPEN" : "CLOSED";
            bay1.text = !string.IsNullOrWhiteSpace(Game.Instance.bay[0]) ? Game.Instance.bay[0] : "empty";
            bay1.color = Game.Instance.currentBay == 0 ? currentBayColour : notCurrentBayColour;
            bay2.text = !string.IsNullOrWhiteSpace(Game.Instance.bay[1]) ? Game.Instance.bay[1] : "empty";
            bay2.color = Game.Instance.currentBay == 1 ? currentBayColour : notCurrentBayColour;
            bay3.text = !string.IsNullOrWhiteSpace(Game.Instance.bay[2]) ? Game.Instance.bay[2] : "empty"; ;
            bay3.color = Game.Instance.currentBay == 2 ? currentBayColour : notCurrentBayColour;
            bay4.text = !string.IsNullOrWhiteSpace(Game.Instance.bay[3]) ? Game.Instance.bay[3] : "empty";
            bay4.color = Game.Instance.currentBay == 3 ? currentBayColour : notCurrentBayColour;
            target.text = (Game.Instance.CurrentTarget == null) ? "" : Game.Instance.CurrentTarget.name;

            xsectionBar.sizeDelta = new Vector2((int)(Game.Instance.CrossSection * 70 / 100), 10.0f);

            if (Game.Instance.Track > 1)
            {
                tracked.color = trackedColour;
            }
            else
            {
                tracked.color = notTrackedColour;
            }

            if (Game.Instance.highestRadar != null)
            {
                //Debug.Log("set radar bar");
                radarBar.gameObject.SetActive(true);
                float distance = Mathf.Clamp(100 - (Game.Instance.highestRadar.transform.position - Game.Instance.player.transform.position).magnitude, 0, 100);
                radarBar.sizeDelta = new Vector2((int)(distance * 70 / 100), 4.0f);
            }
            else
            {
                radarBar.gameObject.SetActive(false);

            }


        }
    }
}