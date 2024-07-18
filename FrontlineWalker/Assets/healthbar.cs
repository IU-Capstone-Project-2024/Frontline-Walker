using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    [SerializeField] public Image body;
    [SerializeField] public Image fuel;
    [SerializeField] public Image gun;
    [SerializeField] public Image cannon;
    [SerializeField] public Image rightUpLeg;
    [SerializeField] public Image rightDownLeg;
    [SerializeField] public Image rightStep;
    [SerializeField] public Image leftUpLeg;
    [SerializeField] public Image leftDownLeg;
    [SerializeField] public Image leftStep;

    [SerializeField] public Color damageColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        body.color = damageColor;
        body.fillAmount = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
