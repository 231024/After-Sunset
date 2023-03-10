using System;
using UnityEngine;

public class ShootingController : IExecute
{
    
    private GameObject _cursor;
    private Transform _gunBarrelPosition;
    
    private readonly ShotController _shotController;
    private readonly IUserInputProxy<float> _pcInputFire;

    private float _fire;
    
    public ShootingController(IUserInputProxy<float> inputFire, ShotController shotController, Transform unit)
    {
        _pcInputFire = inputFire;
        _pcInputFire.AxisOnChange += FireOnAxisOnChange;
        _shotController = shotController;
        _cursor = GameObject.Find(SupportObjectType.Cursor.ToString());
        _gunBarrelPosition = unit.GetComponentInChildren<PlayerView>().GunBarrelPosition;
    }
    public void Execute(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
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
                    zombie.Kill();
            }
        }
    }
    
    private void FireOnAxisOnChange(float value)
    {
        _fire = value;
    }
}