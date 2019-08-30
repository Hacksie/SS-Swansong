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
                this.gameObject.SetActive(false);
                return;
            }

            if (Game.Instance.highestRadar != null)
            {

                this.gameObject.SetActive(true);
                Vector3 direction = (Game.Instance.highestRadar.transform.position - Game.Instance.camera.transform.position).normalized * magnitude;
                this.transform.position = Game.Instance.camera.transform.position + direction;

                //FIXME: Speed this up
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                this.gameObject.SetActive(false);
            }

        }


    }
}