using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDrag : MonoBehaviour
{
    private GameObject img = null;
    public int n = 0;
   public void OnDragBegin(BaseEventData data)
    {
        img = UnitSpawn.instance.unit[n];
        
    }

    public void OnDrag(BaseEventData data)
    {
        if(img != null)
        {
            PointerEventData pd = (PointerEventData)data;
            img.transform.position = pd.position;
        }
    }

    public void OnDragEnd(BaseEventData data)
    {
        if(img != null)
        {
            img = null;
        }
    }
}
