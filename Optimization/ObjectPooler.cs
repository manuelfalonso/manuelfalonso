using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Single Object Pooler class with variable amount to pool
/// GetPooledObject takes the first Not Active object of the Pool
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;    
    public GameObject objectToPool;
    public int amountToPool;
	
    [SerializeField] private bool canGrow = true;
	
	private List<GameObject> pooledObjects;
	
    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }
    }

    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (canGrow)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager

            return obj;
        } 
        else 
            // otherwise, return null   
            return null;
    }
}
