using UnityEngine;

namespace HackedDesign
{
    [CreateAssetMenu(fileName = "Mission", menuName = "Swansong/MissionDescription")]
    public class MissionDescription : ScriptableObject
    {
        string shortDescription;
        string description;
    }
}