using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DefaultPickupBehavior : MonoBehaviour, IPickup
{
    [SerializeField] private GameObject pickupPFX;
    [SerializeField] private string pickupSoundName;
    [SerializeField] private AudioMixerGroup mixerGroup;
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

        AudioManager.Instance.PlaySound(pickupSoundName, mixerGroup.name);

        AppearOnMap appearOnMap = GetComponent<AppearOnMap>();
        if (appearOnMap != null)
        {
            MiniMap.Instance.RemoveItemFromMap(appearOnMap);
        }

        Destroy(gameObject, 1.0f);
    }
}
