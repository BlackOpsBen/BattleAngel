using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 5f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GameManager.Instance.GetPlayerInstance().transform.position, Time.deltaTime * lerpSpeed);
    }
}
