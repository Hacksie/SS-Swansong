using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [CreateAssetMenu(fileName = "Missile", menuName = "Swansong/MissileDescription")]
    public class MissileDescription : ScriptableObject
    {
        public string shortName;
        public string description;
        public float thrust;
        public float rotationSpeed;
        public float timeOut;
        public int credsMin;
        public int credsMax;
    }
}
