using UnityEngine;

namespace HackedDesign
{
    public class MissionArrow : MonoBehaviour
    {
        public float magnitude;

        public void UpdatePosition()
        {
            if (Game.Instance.state != GameState.PLAYING)
            {
                if (this.gameObject.activeInHierarchy)
                {
                    this.gameObject.SetActive(false);
                }
                return;
            }

            if (Game.Instance.missionTargets != null && Game.Instance.missionTargets.Count > Game.Instance.currentMission && Game.Instance.missionTargets[Game.Instance.currentMission] != null)
            {
                if (!this.gameObject.activeInHierarchy)
                {
                    this.gameObject.SetActive(true);
                }

                //Debug.Log(this.name + ": " + Game.Instance.CheckMissions());

                if (Game.Instance.CheckMissions())
                {

                    this.gameObject.SetActive(false);
                    return;
                }

                Vector2 direction = (Game.Instance.missionTargets[Game.Instance.currentMission].transform.position - Game.Instance.camera.transform.position).normalized * magnitude;
                this.transform.position = new Vector2(Game.Instance.camera.transform.position.x, Game.Instance.camera.transform.position.y) + direction;
                transform.up = direction;
            }
            else
            {
                if (this.gameObject.activeInHierarchy)
                {
                    this.gameObject.SetActive(false);
                }
            }

        }


    }
}