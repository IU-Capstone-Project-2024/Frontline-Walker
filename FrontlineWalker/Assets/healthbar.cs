using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    [Header("UI Sprites")]

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

    [Header("Colors")]

    [SerializeField] public Color bodyColor = Color.white;
    [SerializeField] public Color fuelColor = Color.white;
    [SerializeField] public Color backLegColor = Color.white;
    [SerializeField] public Color damageColor = Color.red;
    [SerializeField] public Color criticalColor = Color.white;

    [Header("Walker Parts")]

    public GameObject GO_body;
    //public GameObject GO_fuel;
    //public GameObject GO_gun;
    public GameObject GO_cannon;
    public GameObject GO_rightUpLeg;
    public GameObject GO_rightDownLeg;
    public GameObject GO_rightStep;
    public GameObject GO_leftUpLeg;
    public GameObject GO_leftDownLeg;
    public GameObject GO_leftStep;

    private TestCharacterPart TCP_body;
    //private TestCharacterPart TCP_fuel;
    //private TestCharacterPart TCP_gun;
    private TestCharacterPart TCP_cannon;
    private TestCharacterPart TCP_rightUpLeg;
    private TestCharacterPart TCP_rightDownLeg;
    private TestCharacterPart TCP_rightStep;
    private TestCharacterPart TCP_leftUpLeg;
    private TestCharacterPart TCP_leftDownLeg;
    private TestCharacterPart TCP_leftStep;

    // Start is called before the first frame update
    void Start()
    {
        fuel.color = fuelColor;

        body.color = bodyColor;
        gun.color = bodyColor;
        cannon.color = bodyColor;
        rightUpLeg.color = bodyColor;
        rightDownLeg.color = bodyColor;
        rightStep.color = bodyColor;


        leftUpLeg.color = backLegColor;
        leftDownLeg.color = backLegColor;
        leftStep.color = backLegColor;

        TCP_body = GO_body.GetComponent<TestCharacterPart>();
        //TCP_fuel = GO_fuel.GetComponent<TestCharacterPart>();
        //TCP_gun = GO_gun.GetComponent<TestCharacterPart>();
        TCP_cannon = GO_cannon.GetComponent<TestCharacterPart>();
        TCP_rightUpLeg = GO_rightUpLeg.GetComponent<TestCharacterPart>();
        TCP_rightDownLeg = GO_rightDownLeg.GetComponent<TestCharacterPart>();
        TCP_rightStep = GO_rightStep.GetComponent<TestCharacterPart>();
        TCP_leftUpLeg = GO_leftUpLeg.GetComponent<TestCharacterPart>();
        TCP_leftDownLeg = GO_leftDownLeg.GetComponent<TestCharacterPart>();
        TCP_leftStep = GO_leftStep.GetComponent<TestCharacterPart>();

    }

    // Update is called once per frame
    void Update()
    {
        // fuel.color = Color.Lerp(fuelColor, damageColor, (TCP_fuel.GetMaxHealth() - TCP_fuel.GetHealth()) / TCP_fuel.GetMaxHealth());

        body.color = Color.Lerp(bodyColor, damageColor, (TCP_body.GetMaxHealth() - TCP_body.GetHealth()) / TCP_body.GetMaxHealth());
        // gun.color = Color.Lerp(bodyColor, damageColor, (TCP_gun.GetMaxHealth() - TCP_gun.GetHealth()) / TCP_gun.GetMaxHealth());
        cannon.color = Color.Lerp(bodyColor, damageColor, (TCP_cannon.GetMaxHealth() - TCP_cannon.GetHealth()) / TCP_cannon.GetMaxHealth());
        rightUpLeg.color = Color.Lerp(bodyColor, damageColor, (TCP_rightUpLeg.GetMaxHealth() - TCP_rightUpLeg.GetHealth()) / TCP_rightUpLeg.GetMaxHealth());
        rightDownLeg.color = Color.Lerp(bodyColor, damageColor, (TCP_rightDownLeg.GetMaxHealth() - TCP_rightDownLeg.GetHealth()) / TCP_rightDownLeg.GetMaxHealth());
        rightStep.color = Color.Lerp(bodyColor, damageColor, (TCP_rightStep.GetMaxHealth() - TCP_rightStep.GetHealth()) / TCP_rightStep.GetMaxHealth());


        leftUpLeg.color = Color.Lerp(backLegColor, damageColor, (TCP_leftUpLeg.GetMaxHealth() - TCP_leftUpLeg.GetHealth()) / TCP_leftUpLeg.GetMaxHealth());
        leftDownLeg.color = Color.Lerp(backLegColor, damageColor, (TCP_leftDownLeg.GetMaxHealth() - TCP_leftDownLeg.GetHealth()) / TCP_leftDownLeg.GetMaxHealth());
        leftStep.color = Color.Lerp(backLegColor, damageColor, (TCP_leftStep.GetMaxHealth() - TCP_leftStep.GetHealth()) / TCP_leftStep.GetMaxHealth());
        
    }
}
