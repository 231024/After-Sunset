using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "InputData", menuName = "Data/InputData")]
public sealed class InputData : ScriptableObject
{
    [FormerlySerializedAs("_singleFire")] [SerializeField] private KeyCode _Fire;
    [SerializeField] private KeyCode _reload;

    public KeyCode Fire => _Fire;
    public KeyCode Reload => _reload;
}