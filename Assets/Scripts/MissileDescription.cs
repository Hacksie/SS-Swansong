using UnityEngine;

namespace HackedDesign
{
    [CreateAssetMenu(fileName = "Missile", menuName = "Swansong/MissileDescription")]
    public class MissileDescription : ScriptableObject
    {
        public string longName;

        public string description;
        public float thrust;
        public float rotationSpeed;
        public float timeOut;
        public int buyMin;
        public int buyMax;
        public int sellMin;
        public int sellMax;
    }
}
