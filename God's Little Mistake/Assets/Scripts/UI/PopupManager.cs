using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singleton<PopupManager>
{
    [Header("Stat Pop up")]
    public GameObject popupPanel;
    public TMP_Text ItemName;
    public Image ItemIcon;
    public GameObject damagePanel;
    public GameObject damageBad;
    public GameObject damageGood;
    public GameObject damageNeutral;
    public TMP_Text damageText;
    public GameObject critPanel;
    public GameObject critBad;
    public GameObject critGood;
    public GameObject critNeutral;
    public TMP_Text critText;
    public GameObject speedPanel;
    public GameObject speedBad;
    public GameObject speedGood;
    public GameObject speedNeutral;
    public TMP_Text speedText;
    public GameObject headGlow;
    public GameObject torsoGlow;
    public GameObject legsGlow;

    [Header("Comparison Pop up")]
    public GameObject popupPanel2;
    public TMP_Text ItemName2;
    public Image ItemIcon2;
    public GameObject damagePanel2;
    public GameObject damageBad2;
    public GameObject damageGood2;
    public GameObject damageNeutral2;
    public TMP_Text damageText2;
    public GameObject critPanel2;
    public GameObject critBad2;
    public GameObject critGood2;
    public GameObject critNeutral2;
    public TMP_Text critText2;
    public GameObject speedPanel2;
    public GameObject speedBad2;
    public GameObject speedGood2;
    public GameObject speedNeutral2;
    public TMP_Text speedText2;
    public GameObject headGlow2;
    public GameObject torsoGlow2;
    public GameObject legsGlow2;

    // Start is called before the first frame update
    void Start()
    {
        popupPanel.SetActive(false);
        popupPanel2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateItemPopUp(Item _hoverItem)
    {
        ItemName.text = _hoverItem.itemName;
        ItemIcon.sprite = _hoverItem.icon;

        if (_hoverItem.segment == Item.Segment.Head)
        {
            damagePanel.SetActive(true);
            damageText.text = _hoverItem.dmg.ToString();
            critPanel.SetActive(true);
            damageText.text = _hoverItem.critChance.ToString();
            speedPanel.SetActive(false);

            //set glow indicator
            headGlow.SetActive(true);
            torsoGlow.SetActive(false);
            legsGlow.SetActive(false);
        }

        if (_hoverItem.segment == Item.Segment.Torso)
        {
            damagePanel.SetActive(true);
            damageText.text = _hoverItem.dmg.ToString();
            critPanel.SetActive(true);
            damageText.text = _hoverItem.critChance.ToString();
            speedPanel.SetActive(false);

            //set glow indicator
            headGlow.SetActive(false);
            torsoGlow.SetActive(true);
            legsGlow.SetActive(false);
        }

        if (_hoverItem.segment == Item.Segment.Legs)
        {
            damagePanel.SetActive(false);
            critPanel.SetActive(false);
            speedPanel.SetActive(true);

            //set glow indicator
            headGlow.SetActive(false);
            torsoGlow.SetActive(false);
            legsGlow.SetActive(true);
        }

    }

    //public void UpdateItemPopUp(Item _hoverItem)
    //{
    //    //ADD LATER FORMATTING FOR FLOATS

    //    popupName.text = _hoverItem.itemName;
    //    popupDmg.text = _hoverItem.dmg.ToString();
    //    //popupCritX.text = _hoverItem.critX.ToString();
    //    popupCritChance.text = _hoverItem.critChance.ToString();
    //    popupFirerate.text = _hoverItem.firerate.ToString();
    //    popupIcon.sprite = _hoverItem.icon;

    //    //segment check
    //    if (_hoverItem.segment == Item.Segment.Head)
    //    {
    //        topEye.SetActive(true);
    //        middleEye.SetActive(false);
    //        bottomEye.SetActive(false);
    //    }
    //    if (_hoverItem.segment == Item.Segment.Torso)
    //    {
    //        topEye.SetActive(false);
    //        middleEye.SetActive(true);
    //        bottomEye.SetActive(false);
    //    }
    //    if (_hoverItem.segment == Item.Segment.Legs)
    //    {
    //        topEye.SetActive(false);
    //        middleEye.SetActive(false);
    //        bottomEye.SetActive(true);
    //    }

    //    //range or melee check
    //    if (_hoverItem.projectile == true)
    //    {
    //        attackIcon.sprite = rangedIcon;
    //        attackPill.color = Color.blue;
    //        rangePillText.text = _hoverItem.projectileRange.ToString();
    //        attackPillText.text = "Ranged";
    //    }
    //    else
    //    {
    //        attackIcon.sprite = meleeIcon;
    //        attackPill.color = Color.red;
    //        rangePillText.text = _hoverItem.projectileRange.ToString();
    //        attackPillText.text = "Melee";

    //    }

    //    print("Update pop up");


    //}
}
