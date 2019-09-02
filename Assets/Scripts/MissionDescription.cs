using UnityEngine;

namespace HackedDesign
{
    [CreateAssetMenu(fileName = "Mission", menuName = "Swansong/MissionDescription")]
    public class MissionDescription : ScriptableObject
    {
        [TextArea]
        public string shortDescription;
        [TextArea]
        public string description;
    }
}