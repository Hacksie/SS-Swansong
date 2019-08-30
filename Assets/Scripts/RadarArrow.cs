using UnityEngine;

namespace HackedDesign
{
    public class RadarArrow : MonoBehaviour
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

            if (Game.Instance.highestRadar != null)
            {
                if (!this.gameObject.activeInHierarchy)
                {
                    this.gameObject.SetActive(true);
                }
                Vector2 direction = (Game.Instance.highestRadar.transform.position - Game.Instance.camera.transform.position).normalized * magnitude;
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