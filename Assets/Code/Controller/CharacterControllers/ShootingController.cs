using System;
using UnityEngine;

public class ShootingController : IExecute
{
    
    private GameObject _cursor;
    private Transform _gunBarrelPosition;
    
    private readonly ShotController _shotController;
    private readonly IUserInputKeyPress _pcInputFire;

    private bool _fire;
    private float _amountDamage;

    public ShootingController(InputUserModel input, ShotController shotController, Transform unit)
    {
        _pcInputFire = input.Fire;
        _pcInputFire.OnKeyPressed += Shoot;
        _shotController = shotController;
        _cursor = GameObject.Find(SupportObjectType.Cursor.ToString());
        _gunBarrelPosition = unit.GetComponentInChildren<PlayerView>().GunBarrelPosition;
        _amountDamage = 0.5f;
    }
    public void Execute(float deltaTime)
    {
        // if (_fire)
        // {
        //     var from = _gunBarrelPosition.position;
        //     var target = _cursor.transform.position;
        //     var to = new Vector3(target.x, from.y, target.z);
        //     var direction = (to - from).normalized;
        //
        //     RaycastHit hit;
        //     
        //     if (Physics.Raycast(from, to - from, out hit, 100))
        //         to = new Vector3(hit.point.x, from.y, hit.point.z);
        //     else
        //         to = from + direction * 100;
        //     
        //     _shotController.Show(from, to);
        //     
        //     if (hit.transform != null) {
        //         var zombie = hit.transform.GetComponent<EnemyProvider>();
        //         if (zombie != null)
        //             zombie.TakeDamage(_amountDamage);
        //     }
        // }
    }

    private void Shoot()
    {
        var from = _gunBarrelPosition.position;
        var target = _cursor.transform.position;
        var to = new Vector3(target.x, from.y, target.z);
        var direction = (to - from).normalized;

        RaycastHit hit;
            
        if (Physics.Raycast(from, to - from, out hit, 100))
            to = new Vector3(hit.point.x, from.y, hit.point.z);
        else
            to = from + direction * 100;
            
        _shotController.Show(from, to);
            
        if (hit.transform != null) {
            var zombie = hit.transform.GetComponent<EnemyProvider>();
            if (zombie != null)
                zombie.TakeDamage(_amountDamage);
        }
    }
    
    private void FireOnAxisOnChange(bool value)
    {
        _fire = value;
    }
}