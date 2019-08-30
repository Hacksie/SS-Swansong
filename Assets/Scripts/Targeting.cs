using UnityEngine;

namespace HackedDesign
{
    public class Targeting : MonoBehaviour
    {
        void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Scanner") 
            {
                if (!Game.Instance.currentTargets.Contains(this.transform.parent.gameObject))
                {
                    Game.Instance.currentTargets.Add(this.transform.parent.gameObject);
                }

                if(Game.Instance.CurrentTarget == null)
                {
                    Game.Instance.CurrentTarget = this.transform.parent.gameObject;
                }

                //Game.Instance.CurrentTarget = other.gameObject;
            }


        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Scanner") 
            {
                if (Game.Instance.currentTargets.Contains(this.transform.parent.gameObject))
                {
                    Game.Instance.currentTargets.Remove(this.transform.parent.gameObject);
                }

                if(Game.Instance.CurrentTarget = this.transform.parent.gameObject)
                {
                    if(Game.Instance.currentTargets.Count > 0)
                    {
                        Game.Instance.CurrentTarget = Game.Instance.currentTargets[0];
                    }
                    else {
                        Game.Instance.CurrentTarget = null;
                    }
                }
                //Game.Instance.CurrentTarget = null;
            }
        }
    }
}