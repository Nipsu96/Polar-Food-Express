using UnityEngine;

namespace Polar
{
    [CreateAssetMenu(fileName = "GameSpeed", menuName = "ScriptableObjects/Game Speed")]
    public class SOGameSpeed : ScriptableObject
    {
        [SerializeField] internal float gameSpeed;
    }
}
