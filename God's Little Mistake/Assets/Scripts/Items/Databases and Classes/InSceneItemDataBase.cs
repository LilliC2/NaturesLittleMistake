using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneItemDataBase : Singleton<InSceneItemDataBase>
{
    public List<Item> inSceneItemDataBase;

    /// <summary>
    /// Adds item to players inventory and removes it from scene
    /// </summary>
    /// <param name="_sceneID"></param>
    public void AddItemToInventory(int _sceneID)
    {
        

        //move item to inventory
        if (_PC.playerInventory.Count < 7)
        {

            _UI.statsPopUpPanel.SetActive(false);
            _PC.playerInventory.Add(inSceneItemDataBase[_sceneID]);
            print("added item");
            inSceneItemDataBase.Remove(inSceneItemDataBase[_sceneID]);

            //move all items in list downm (automatic)
            //make sure their ids stay the same!!!
            for (int i = 0; i < inSceneItemDataBase.Count; i++)
            {
                inSceneItemDataBase[i].inSceneID = inSceneItemDataBase[i].inSceneID - 1;
            }

            var index = _PC.playerInventory.Count - 1;
            _PC.playerInventory[index].ID = index;

            _UI.UpdateInventorySlotImages();

            //check if its passive, secondary, primary or symbiote
            switch(_PC.playerInventory[index].itemType)
            {
                //calls apprioriate function
                case Item.ItemType.Primary:
                    break;
                case Item.ItemType.Secondary:
                    break;
                case Item.ItemType.Passive:
                    AddPassiveItem(index);
                    break;
                case Item.ItemType.Symbiote:
                    break;
            }


            
        }



    }

    /// <summary>
    /// Removes item from player's invetory and model and add its to the scene
    /// </summary>
    /// <param name="_inventoryID"></param>
    public void RemoveItemFromInventory(int _inventoryID)
    {
        //remove item from player canvas
        for (int i = 0; i < _AVTAR.slotsOnPlayer.Length; i++)
        {
            //check if slot hhas child
            if (_AVTAR.slotsOnPlayer[i].transform.childCount != 0)
            {
                //check if item equipped is the item
                var obj = _AVTAR.slotsOnPlayer[i].transform.GetChild(0);
                if (obj.name.Contains(_inventoryID.ToString()))
                {
                    Destroy(obj.gameObject);
                }
            }
        }

        //check if its passive, secondary, primary or symbiote
        switch (_PC.playerInventory[_inventoryID].itemType)
        {
            //calls apprioriate function
            case Item.ItemType.Primary:
                break;
            case Item.ItemType.Secondary:
                break;
            case Item.ItemType.Passive:
                RemovePassiveItem(_inventoryID);
                break;
            case Item.ItemType.Symbiote:
                break;
        }


        //add item to scene and remove from inventory
        inSceneItemDataBase.Add(_PC.playerInventory[_inventoryID]);
        _PC.playerInventory.Remove(_PC.playerInventory[_inventoryID]);
        
        var item = Instantiate(_IG.itemTemp, GameObject.Find("Player").transform.position, Quaternion.identity);

        int index = _ISitemD.inSceneItemDataBase.Count - 1;
        inSceneItemDataBase[index].inSceneID = index;

        

        _UI.UpdateInventorySlotImages();
        //WHEN CURRENCY IS ADDED, PLAYER WOULD GAIN CURRENCY HERE

    }

    /// <summary>
    /// Add passive item buffs to player stats
    /// </summary>
    /// <param name="_index"></param>
    public void AddPassiveItem(int _inventoryID)
    {
        //if the items ever add HP
        //_PC.health += _PC.playerInventory[_index].health

        //if the items ever add speed
        //_PC.speed += _PC.playerInventory[_index].speed;
        
        _PC.dmg += _PC.playerInventory[_inventoryID].dmg;
        _PC.dps += _PC.playerInventory[_inventoryID].dps;
        _PC.range += _PC.playerInventory[_inventoryID].range;
        _PC.projectileSpeed += _PC.playerInventory[_inventoryID].projectileSpeed;


        _PC.firerate -= _PC.playerInventory[_inventoryID].fireRate;
    }

    /// <summary>
    /// Remove passive item buffs to player stats
    /// </summary>
    /// <param name="_index"></param>
    public void RemovePassiveItem(int _inventoryID)
    {
        //if the items ever add HP
        //_PC.health -= _PC.playerInventory[_index].health

        //if the items ever add speed
        //_PC.speed -= _PC.playerInventory[_index].speed;
        
        _PC.dmg -= _PC.playerInventory[_inventoryID].dmg;
        _PC.dps -= _PC.playerInventory[_inventoryID].dps;
        _PC.range -= _PC.playerInventory[_inventoryID].range;
        _PC.projectileSpeed -= _PC.playerInventory[_inventoryID].projectileSpeed;
        _PC.firerate -= _PC.playerInventory[_inventoryID].fireRate;
    }


}
