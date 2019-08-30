using UnityEngine;

namespace HackedDesign
{
    public class Missile : MonoBehaviour
    {
        [SerializeField]
        public float thrust;

        [SerializeField]
        public GameObject target;
    }
}