using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Tower playerTower;
    public Tower enemyTower;
    public End_Popup endPopup; 

    private void Update()
    {
        if (playerTower.IsDestroy)
        {
            endPopup.ActiveEndPopup(false);
        }
        else if(enemyTower.IsDestroy)
        {
            endPopup.ActiveEndPopup(true);
        }
    }
}
