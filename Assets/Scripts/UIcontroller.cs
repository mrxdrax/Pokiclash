using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UIcontroller : MonoBehaviour
{
    public static UIcontroller instance;
    [SerializeField] private Slider playerslider;
    [SerializeField] private TMP_Text healthtext;
    [SerializeField] private TMP_Text killText;
    public GameObject gameoverpannel;
    public GameObject pausemenu;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        UpdateKillCount();
    }
    public void UpdateHealth() 
    {
        
        playerslider.maxValue = playercontroller.instance.maxhealth;
        playerslider.value = playercontroller.instance.playerhealth;
        healthtext.text = playerslider.value + "/" + playerslider.maxValue;
    }
    void UpdateKillCount()
    {
        killText.text = "Kills: " + playercontroller.instance.killcount;
    }
}
