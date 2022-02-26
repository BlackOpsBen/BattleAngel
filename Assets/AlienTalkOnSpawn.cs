using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTalkOnSpawn : MonoBehaviour
{
    [SerializeField] private string talkSoundName;

    [SerializeField] private bool is3D = false;

    // Start is called before the first frame update
    void Start()
    {

        if (is3D)
        {
            AudioManager.Instance.PlaySound(talkSoundName);
        }
        else
        {
            AudioManager.Instance.PlaySound(talkSoundName, transform);
        }
    }
}
