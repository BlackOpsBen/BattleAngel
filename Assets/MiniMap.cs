using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public static MiniMap Instance { get; private set; }

    [SerializeField] public AnimationCurve mapDistCurve;
    [SerializeField] public float maxDistSqr = 5000.0f;

    private List<AppearOnMap> itemsOnMap = new List<AppearOnMap>();

    private List<Image> dotPool = new List<Image>();

    [SerializeField] private Image dotImagePrefab;

    [SerializeField] private Transform dotContainer;

    private float mapSize;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        mapSize = GetComponent<RectTransform>().sizeDelta.x;
    }

    public void AddItemToMap(AppearOnMap item)
    {
        itemsOnMap.Add(item);
    }

    public void RemoveItemFromMap(AppearOnMap item)
    {
        itemsOnMap.Remove(item);
    }

    private void Update()
    {
        UpdateMapItems();
    }

    private void UpdateMapItems()
    {
        ResetAllDotsInPool();

        for (int i = 0; i < itemsOnMap.Count; i++)
        {
            if (itemsOnMap[i].GetShowOnMap())
            {
                Image image = GetImage(i);
                image.color = itemsOnMap[i].GetColor();
                image.rectTransform.localScale = image.rectTransform.localScale * itemsOnMap[i].GetScale();
                image.rectTransform.localPosition = itemsOnMap[i].GetMapPosition(mapSize);
            }
        }
    }

    private void ResetAllDotsInPool()
    {
        foreach (Image dot in dotPool)
        {
            dot.color = Color.clear;
        }
    }

    private Image GetImage(int i)
    {
        if (dotPool.Count > i)
        {
            return dotPool[i];
        }
        else
        {
            Image newImage = Instantiate(dotImagePrefab, dotContainer);
            dotPool.Add(newImage);
            return newImage;
        }
    }
}
