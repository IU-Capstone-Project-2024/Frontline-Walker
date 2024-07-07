using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestProjectileShooter))]
[RequireComponent(typeof(TestRotatonController))]
public class TestCannon : MonoBehaviour
{
    private TestRotatonController _rotatonController;
    private TestProjectileShooter _projectileShooter;
    
    // Start is called before the first frame update
    void Start()
    {
        _projectileShooter = GetComponent<TestProjectileShooter>();
        _rotatonController = GetComponent<TestRotatonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Up()
    {
        _rotatonController.Up();
    }

    public void Down()
    {
        _rotatonController.Down();
    }

    public void Fire()
    {
        _projectileShooter.Shoot();
    }
}
