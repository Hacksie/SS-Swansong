using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class MissionPresenter : MonoBehaviour
    {
        [SerializeField]
        private Color activeColour = Color.white;

        [SerializeField]
        private Color completedColour = Color.grey;

        [SerializeField]
        private Color notCompletedColour = new Color(0, 0, 0, 0);

        [SerializeField]
        private Text[] missionShortDescription = new Text[10];

        [SerializeField]
        private Text missionLongDescription;

        void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                if (missionShortDescription[i] == null)
                {
                    Debug.LogError(this.name + ": short desc " + i + " not set");
                }

                if (missionLongDescription == null)
                {
                    Debug.LogError(this.name + ": long desc not set");
                }
            }
        }

        public void UpdateUI()
        {
            if (Game.Instance.state == GameState.MISSIONS && !this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(true);
            }
            else if (Game.Instance.state != GameState.MISSIONS && this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(false);

            }

            for (int i = 0; i < 10; i++)
            {
                if (i < Game.Instance.currentMission)
                {
                    missionShortDescription[i].text = Game.Instance.missionDescriptions[i].shortDescription;
                    missionShortDescription[i].color = completedColour;
                }
                else if (i == Game.Instance.currentMission)
                {
                    missionShortDescription[i].text = Game.Instance.missionDescriptions[i].shortDescription;
                    missionShortDescription[i].color = activeColour;
                }
                else if (i > Game.Instance.currentMission)
                {
                    missionShortDescription[i].text = "";
                    missionShortDescription[i].color = notCompletedColour;
                }

            }

        }

        public void ContinueButtonEvent()
        {
            Game.Instance.ContinueGame();
        }
    }
}