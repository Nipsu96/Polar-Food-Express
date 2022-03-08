using UnityEngine;

namespace Polar
{
    [CreateAssetMenu(fileName = "CollectableValues_", menuName = "ScriptableObjects/Score and Carbon values")]
    public class SOCollectableValues : ScriptableObject
    {
        [SerializeField] internal float score;
        [SerializeField] internal float carbonImpact;
    }
}
