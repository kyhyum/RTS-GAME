using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Tower Player_Tower;
    public Tower Enemy_Tower;
    public End_Popup end_Popup; 

    private void Update()
    {
        if (Player_Tower.IsDestroy)
        {
            end_Popup.Active_End_Popup(false);
        }
        else if(Enemy_Tower.IsDestroy)
        {
            end_Popup.Active_End_Popup(true);
        }
    }
}
