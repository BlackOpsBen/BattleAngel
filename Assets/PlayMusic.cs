using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private string songName;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySoundLoop(songName, "Music", "Music");
    }
}
