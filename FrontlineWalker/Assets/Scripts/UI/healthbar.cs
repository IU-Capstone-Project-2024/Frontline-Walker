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
    [SerializeField] public GameObject distabilizeIcon;

    [Header("Colors")]

    [SerializeField] public Color bodyColor = Color.white;
    [SerializeField] public Color fuelColor = Color.white;
    [SerializeField] public Color backLegColor = Color.white;
    [SerializeField] public Color damageColor = Color.red;
    [SerializeField] public Color notWorkingColor = Color.white;

    [Header("Walker Parts")]

    public GameObject GO_body;
    public GameObject GO_fuel;
    public GameObject GO_gun;
    public GameObject GO_cannon;
    public GameObject GO_rightUpLeg;
    public GameObject GO_rightDownLeg;
    public GameObject GO_rightStep;
    public GameObject GO_leftUpLeg;
    public GameObject GO_leftDownLeg;
    public GameObject GO_leftStep;

    private TestCharacterPart TCP_body;
    private TestCharacterPart TCP_fuel;
    private TestCharacterPart TCP_gun;
    private TestCharacterPart TCP_cannon;
    private TestCharacterPart TCP_rightUpLeg;
    private TestCharacterPart TCP_rightDownLeg;
    private TestCharacterPart TCP_rightStep;
    private TestCharacterPart TCP_leftUpLeg;
    private TestCharacterPart TCP_leftDownLeg;
    private TestCharacterPart TCP_leftStep;

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
        TCP_fuel = GO_fuel.GetComponent<TestCharacterPart>();
        TCP_gun = GO_gun.GetComponent<TestCharacterPart>();
        TCP_cannon = GO_cannon.GetComponent<TestCharacterPart>();
        TCP_rightUpLeg = GO_rightUpLeg.GetComponent<TestCharacterPart>();
        TCP_rightDownLeg = GO_rightDownLeg.GetComponent<TestCharacterPart>();
        TCP_rightStep = GO_rightStep.GetComponent<TestCharacterPart>();
        TCP_leftUpLeg = GO_leftUpLeg.GetComponent<TestCharacterPart>();
        TCP_leftDownLeg = GO_leftDownLeg.GetComponent<TestCharacterPart>();
        TCP_leftStep = GO_leftStep.GetComponent<TestCharacterPart>();

    }

    void Update()
    {
        Coloring(fuel, TCP_fuel, fuelColor, damageColor, notWorkingColor);

        Coloring(body, TCP_body, bodyColor, damageColor, notWorkingColor);
        Coloring(gun, TCP_gun, bodyColor, damageColor, notWorkingColor);
        Coloring(cannon, TCP_cannon, bodyColor, damageColor, notWorkingColor);
        Coloring(rightUpLeg, TCP_rightUpLeg, bodyColor, damageColor, notWorkingColor);
        Coloring(rightDownLeg, TCP_rightDownLeg, bodyColor, damageColor, notWorkingColor);
        Coloring(rightStep, TCP_rightStep, bodyColor, damageColor, notWorkingColor);

        Coloring(leftUpLeg, TCP_leftUpLeg, backLegColor, damageColor, notWorkingColor);
        Coloring(leftDownLeg, TCP_leftDownLeg, backLegColor, damageColor, notWorkingColor);
        Coloring(leftStep, TCP_leftStep, backLegColor, damageColor, notWorkingColor);

        if (GO_body.GetComponent<TestTorsoController>().isDistabilazed())
        {
            distabilizeIcon.SetActive(true);
        }
        else
        {
            distabilizeIcon.SetActive(false);
        }
    }

    public void Coloring(Image part, TestCharacterPart TCP, Color ok, Color damage, Color notWorking)
    {
        if (TCP.IsWorking())
        {
            part.color = Color.Lerp(ok, damage, (TCP.GetMaxHealth() - TCP.GetHealth()) / TCP.GetMaxHealth());
        }
        else
        {
            part.color = notWorking;
        }
    }
}
