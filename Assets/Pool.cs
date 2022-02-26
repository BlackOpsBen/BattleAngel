using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    private List<GameObject> availablePool = new List<GameObject>();

    [SerializeField] private int maxInPool = 10;

    private int currentItem = 0;

    public GameObject GetNext()
    {
        GameObject item;

        if (availablePool.Count > currentItem)
        {
            item = availablePool[currentItem];
            item.SetActive(false);
            item.SetActive(true);
        }
        else
        {
            item = Instantiate(itemPrefab, transform);
            availablePool.Add(item);
        }

        currentItem = (currentItem + 1) % maxInPool;

        return item;

        /*if (availablePool.Count > 0)
        {
            GameObject item = availablePool[0];
            availablePool.Remove(item);
            item.SetActive(true);
            return item;
        }
        else
        {
            GameObject item = Instantiate(itemPrefab, transform);
            availablePool.Add(item);
            return item;
        }*/
    }

    /*public void ReturnToAvailablePool(GameObject item)
    {
        availablePool.Add(item);
        item.SetActive(false);
    }*/
}
