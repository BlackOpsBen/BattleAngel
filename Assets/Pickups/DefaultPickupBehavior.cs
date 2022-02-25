using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPickupBehavior : MonoBehaviour, IPickup
{
    [SerializeField] private GameObject pickupPFX;
    [SerializeField] private string pickupSoundName;
    [SerializeField] private MeshRenderer meshToHide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movement>())
        {
            OnPickup();
        }
    }

    public virtual void OnPickup()
    {
        meshToHide.enabled = false;

        pickupPFX.SetActive(true);

        AudioManager.Instance.PlaySound(pickupSoundName);

        Destroy(gameObject, 1.0f);
    }
}
