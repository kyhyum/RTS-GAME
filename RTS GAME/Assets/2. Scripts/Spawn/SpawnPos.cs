using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    public void SelectPos(int n)
    {
        UnitSpawn.instance.SpawnUnit(n);
    }
}
