using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPiston : MonoBehaviour
{

    [SerializeField] private GameObject _base;
    [SerializeField] private GameObject _piston;
    
    [SerializeField] private float _max_y_position;
    private float _min_y_position; 
    private float _current_y_position;

    private Rigidbody2D _piston_rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _current_y_position = _piston.transform.localPosition.y;
        _min_y_position = _piston.transform.localPosition.y;
        _max_y_position += _piston.transform.localPosition.y;

        Debug.Log(_min_y_position + " " + _max_y_position);
        
        _piston_rb = _piston.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Up()
    {
        Debug.Log(_max_y_position + " " + _current_y_position);

        _base.GetComponent<DistanceJoint2D>().distance += 0.1f * Time.deltaTime;
    }
    
    public void Down()
    {
        Debug.Log(_min_y_position + " " + _current_y_position);
        
        _base.GetComponent<DistanceJoint2D>().distance -= 0.1f * Time.deltaTime;
    }
}
