using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemIdentifier : GameBehaviour
{
    bool inRange;
    public Item itemInfo;

    [Header("Animation")]
    public Animator anim;

    public GameObject statPop;


    public void Start()
    {
        statPop = GameObject.Find("Stat Popup");
        
        anim = statPop.GetComponent<Animator>();
    }

    private void Update()
    {
        if(inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                //pick up item
                if (_PC.playerInventory.Count < 5)//invenotry cap number here
                {
                    print("Destroy obj");
                    Destroy(gameObject);
                    _UI.CreateItemSelected(itemInfo);

                }  

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("player");
            inRange = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("player");
            inRange = false;
        }
    }

    //public void OnMouseEnter()
    //{
    //    print("ENTER");
    //    _UI.statsPopUpPanel.SetActive(true);
    //    _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    //    _UI.UpdateItemPopUp(itemInfo);
    //    anim.SetTrigger("Open");

    //}

    public void OnMouseOver()
    {
        print("ENTER");
        _UI.statsPopUpPanel.SetActive(true);
        _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        _UI.UpdateItemPopUp(itemInfo);
        anim.SetTrigger("Open");
    }

    public void OnMouseExit()
    {
        print("EXIT");
        anim.ResetTrigger("Open");
        anim.SetTrigger("Close");
        ExecuteAfterSeconds(1, () => _UI.statsPopUpPanel.SetActive(false));

    }
}
