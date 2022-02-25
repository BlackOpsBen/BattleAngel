using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private GameObject listItemPrefab;

    private ObjectiveListItem[] objectiveListItems = new ObjectiveListItem[0];

    public ObjectiveListItem NewObjective(string objectiveName, string append)
    {
        List<ObjectiveListItem> listItems = new List<ObjectiveListItem>();
        foreach (ObjectiveListItem item in objectiveListItems)
        {
            listItems.Add(item);
        }

        GameObject newItem = Instantiate(listItemPrefab, transform);
        ObjectiveListItem listItem = newItem.GetComponent<ObjectiveListItem>();

        listItem.InitializeObjective(objectiveName, append);

        listItems.Add(listItem);

        objectiveListItems = listItems.ToArray();

        return listItem;
    }

    public ObjectiveListItem GetObjective(string objectiveName)
    {
        ObjectiveListItem listItem = Array.Find(objectiveListItems, item => item.name == objectiveName);

        if (listItem != null)
        {
            return listItem;
        }
        else
        {
            Debug.LogWarning("Invalid objective name!");
            return null;
        }
    }
    
    [System.Serializable]
    public struct ObjectiveText
    {
        public string name;
        public string append;
    }
}
