using UnityEngine;

namespace HackedDesign
{
    public class CargoShipAlert : MonoBehaviour
    {
        void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                // Send out a distress beacon
                Debug.Log(this.name + ": distress beacon");
                Game.Instance.AlertShip(); 
            }
        }        
    }
}