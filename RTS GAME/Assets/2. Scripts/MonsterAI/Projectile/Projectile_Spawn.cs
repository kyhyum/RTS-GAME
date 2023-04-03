using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Spawn : MonoBehaviour
{
    GameObject Projectile;

    public Queue<GameObject> projectiles_Queue = new Queue<GameObject>();

    private void Awake()
    {
        AIUnit aIUnit = this.GetComponent<AIUnit>();
        Projectile = aIUnit.longRangeWeapon;
        for (int i = 0; i < 3 ; i++)
        {
            Create_Projectile();
        }
    }
    public GameObject Spawn_Projectile(Transform unit_transform, Transform target_transform)
    {
        if(projectiles_Queue.Count == 0)
            Create_Projectile();

        Vector3 vec = new Vector3(unit_transform.position.x, unit_transform.position.y, unit_transform.position.z);
        Projectile.transform.position = vec;
        GameObject projectile = projectiles_Queue.Dequeue();
        projectile.SetActive(true);
        return projectile;
    }

    public void Unactive_Projectile(GameObject Projectile)
    {
        Projectile.SetActive(false);
        projectiles_Queue.Enqueue(Projectile);
    }

    public void Create_Projectile()
    {
        GameObject instance_projectile = Instantiate(Projectile);
        instance_projectile.SetActive(false);
        projectiles_Queue.Enqueue(instance_projectile);
    }
}
