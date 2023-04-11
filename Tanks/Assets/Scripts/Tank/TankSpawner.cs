using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TankSpawner : MonoBehaviour
{
    [Header("Gizmo controls")]
    //slider
    [Range(0.5f, 5f)]
    public float SpawnGizmoSize = 1f;
    //public variable with the item we want to duplicate, enemy tank in this case

    [Header("other shit")]
    public GameObject m_tankPrefab;

    //Makes a list called ChildList that contains can transforms, its currently empty
    List<Transform> ChildList = new List<Transform>();

    //function that is called when tank is alive/ when game starts
    public void Awake()
    {
        //for loop that continues until the amount of iterations is not less than amount of children inside 
        //the children of the spawn controller
        for (int i = 0; i < transform.childCount; i++)
        {
            //ads the Child that has the same value as i
            ChildList.Add(this.transform.GetChild(i));
            
        }
    }

    //when tank dies, this is called in tank health
    public void spawnTank()
    {
        //vector 3 is the type of variable, spawn pos is name, it = a the position of a random child in child list
        Vector3 SpawnPosition = this.ChildList[Random.Range(0,ChildList.Count)].position;
         
        //spawns spawnposition,
        Instantiate(m_tankPrefab, SpawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (Transform spawn in ChildList)
        {
            Gizmos.DrawWireSphere(spawn.position, SpawnGizmoSize);
        }
    }



}

