using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _gunBarrelPosition;

    public Transform GunBarrelPosition => _gunBarrelPosition;
}