using UnityEngine;

namespace HackedDesign
{
    public class Missile : MonoBehaviour
    {
        [SerializeField]
        public float thrust;

        [SerializeField]
        public GameObject target;

        [SerializeField]
        public string type;

        void OnCollisionEnter2D(Collision2D other)
        { 
            
            if(other.gameObject.tag == "Hostile" || (other.transform.parent !=null && other.transform.parent.tag == "Hostile"))
            {
                Debug.Log(this.name + ": explode!");
            }
        }
    }
}