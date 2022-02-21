using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    /// <summary>
    /// Use for low priority dialog that will never happen if ANY character is already talking.
    /// </summary>
    public const int INTERRUPT_NONE = 0;
    /// <summary>
    /// Use for low priority dialog that will not play if the character is currently speaking, but will still play concurrently with other speaking characters.
    /// </summary>
    public const int INTERRUPT_OVERLAP = 1;
    /// <summary>
    /// Use for medium priority dialog that will stop any previous dialog from the character, but all other characters will continue speaking.
    /// </summary>
    public const int INTERRUPT_SELF = 2;
    /// <summary>
    /// Use for high priority dialog which will cause all currently speaking characters to stop and then the new dialog will play. All other interrupt modes will not be allowed to play while this dialog is playing.
    /// </summary>
    public const int INTERRUPT_ALL = 3;

    // If a character speaks with INTERRUPT_ALL, this string will be set to their name.
    private string priorityCharacter;

    private SoundCue[] soundCues;
    private AudioClip[] audioClips;
    private AudioMixer[] audioMixers;
    private SourcePool sourcePool2D;
    private SourcePool sourcePool3D;
    private Dictionary<string, AudioSource> loopInstances = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> charactersSpeaking = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        SingletonPattern();
        LoadResources();
        sourcePool2D = new SourcePool(gameObject, true);
        sourcePool3D = new SourcePool(gameObject, false);

        DontDestroyOnLoad(gameObject);
    }

    private void LoadResources()
    {
        soundCues = Resources.LoadAll<SoundCue>("");

        audioClips = Resources.LoadAll<AudioClip>("");

        audioMixers = Resources.LoadAll<AudioMixer>("");
    }

    private void SingletonPattern()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #region PlaySound Overloads

    // 2D
    /// <summary>
    /// Plays the named AudioClip or SoundCue as a 2D sound.
    /// </summary>
    public void PlaySound(string name)
    {
        if (ProcessPlaySound(name, is3D: false, isSoundCue: false, specificMixerGroup: false, isLooping: false) == null)
        {
            ProcessPlaySound(name, is3D: false, isSoundCue: true, specificMixerGroup: false, isLooping: false);
        }
    }
    // 2D LOOPING
    /// <summary>
    /// Plays and loops the named AudioClip or SoundCue as a 2D sound. The loopId is an arbitrary string used to identify the loop if you want to stop it later.
    /// </summary>
    public void PlaySoundLoop(string name, string loopId)
    {
        if (ProcessPlaySound(name, is3D: false, isSoundCue: false, specificMixerGroup: false, isLooping: true, uniqueId:loopId) == null)
        {
            ProcessPlaySound(name, is3D: false, isSoundCue: true, specificMixerGroup: false, isLooping: true, uniqueId: loopId);
        }
    }

    // 3D
    /// <summary>
    /// Plays the named AudioClip or SoundCue as a 3D sound attached to the provided parent.
    /// </summary>
    public void PlaySound(string name, Transform parent)
    {
        if (ProcessPlaySound(name, is3D: true, isSoundCue: false, specificMixerGroup: false, isLooping: false, parent:parent) == null)
        {
            ProcessPlaySound(name, is3D: true, isSoundCue: true, specificMixerGroup: false, isLooping: false, parent:parent);
        }
    }

    // 3D LOOPING
    /// <summary>
    /// Plays and loops the named AudioClip or SoundCue as a 3D sound attached to the provided parent. The loopId is an arbitrary string used to identify the loop if you want to stop it later.
    /// </summary>
    public void PlaySoundLoop(string name, Transform parent, string loopId)
    {
        if (ProcessPlaySound(name, is3D: true, isSoundCue: false, specificMixerGroup: false, isLooping: true, uniqueId:loopId, parent:parent) == null)
        {
            ProcessPlaySound(name, is3D: true, isSoundCue: true, specificMixerGroup: false, isLooping: true, uniqueId: loopId, parent: parent);
        }
    }

    // 2D OVERRIDE MIXERGROUP
    /// <summary>
    /// Plays the named AudioClip or SoundCue as a 2D sound through the specified AudioMixerGroup.
    /// </summary>
    public void PlaySound(string name, string mixerGroupOverride = "")
    {
        if (ProcessPlaySound(name, is3D: false, isSoundCue: false, specificMixerGroup: true, isLooping: false, mixerGroupOverride) == null)
        {
            ProcessPlaySound(name, is3D: false, isSoundCue: true, specificMixerGroup: true, isLooping: false, mixerGroupOverride);
        }
    }

    // 2D LOOPING OVERRIDE MIXERGROUP
    /// <summary>
    /// Plays and loops the named AudioClip or SoundCue as a 2D sound through the specified AudioMixerGroup. The loopId is an arbitrary string used to identify the loop if you want to stop it later.
    /// </summary>
    public void PlaySoundLoop(string name, string loopId, string mixerGroupOverride = "")
    {
        if (ProcessPlaySound(name, is3D: false, isSoundCue: false, specificMixerGroup: false, isLooping: true, uniqueId: loopId, mixerGroupName: mixerGroupOverride) == null)
        {
            ProcessPlaySound(name, is3D: false, isSoundCue: true, specificMixerGroup: false, isLooping: true, uniqueId: loopId, mixerGroupName: mixerGroupOverride);
        }
    }

    // 3D OVERRIDE MIXERGROUP
    /// <summary>
    /// Plays the named AudioClip or SoundCue as a 3D sound attached to the provided parent and through the specified AudioMixerGroup.
    /// </summary>
    public void PlaySound(string name, Transform parent, string mixerGroupOverride = "")
    {
        if (ProcessPlaySound(name, is3D: true, isSoundCue: false, specificMixerGroup: false, isLooping: false, mixerGroupName: mixerGroupOverride, parent: parent) == null)
        {
            ProcessPlaySound(name, is3D: true, isSoundCue: true, specificMixerGroup: false, isLooping: false, mixerGroupName: mixerGroupOverride, parent: parent);
        }
    }

    // 3D LOOPING OVERRIDE MIXERGROUP
    /// <summary>
    /// Plays and loops the named AudioClip or SoundCue as a 3D sound attached to the provided parent and through the specified AudioMixerGroup. The loopId is an arbitrary string used to identify the loop if you want to stop it later.
    /// </summary>
    public void PlaySoundLoop(string name, Transform parent, string loopId, string mixerGroupOverride = "")
    {
        if (ProcessPlaySound(name, is3D: true, isSoundCue: false, specificMixerGroup: false, isLooping: true, mixerGroupName: mixerGroupOverride, uniqueId: loopId, parent: parent) == null)
        {
            ProcessPlaySound(name, is3D: true, isSoundCue: true, specificMixerGroup: false, isLooping: true, mixerGroupName: mixerGroupOverride, uniqueId: loopId, parent: parent);
        }
    }

    #endregion

    #region Play Dialog

    /// <summary>
    /// Attempt to play dialog from a specific character. Plays as a 2D sound unless is3D is set to true, in which case it is attached to the provided parent. The INTERRUPT_MODE determines whether the character can interrupt itself and/or other currently speaking characters. Uses the AudioMixerGroup of the specified SoundCue. Returns true/false if the dialog actually played.
    /// </summary>
    public bool PlayDialog(string characterName, string soundName, bool is3D = false, Transform parent = null, int INTERRUPT_MODE = INTERRUPT_OVERLAP)
    {

        switch (INTERRUPT_MODE)
        {
            case INTERRUPT_NONE:
                if (!GetAnyCharacterSpeaking())
                {
                    PlayAndAddCharacterDialog(characterName, soundName, is3D, parent);
                    return true;
                }
                return false;
            case INTERRUPT_OVERLAP:
                if (!GetThisCharacterSpeaking(characterName) && !GetPriorityDialogSpeaking())
                {
                    PlayAndAddCharacterDialog(characterName, soundName, is3D, parent);
                    return true;
                }
                return false;
            case INTERRUPT_SELF:
                if (!GetPriorityDialogSpeaking())
                {
                    PlayAndAddCharacterDialog(characterName, soundName, is3D, parent);
                    return true;
                }
                return false;
            case INTERRUPT_ALL:
                foreach (AudioSource charSource in charactersSpeaking.Values)
                {
                    charSource.Stop();
                }
                charactersSpeaking.Clear();
                PlayAndAddCharacterDialog(characterName, soundName, is3D, parent);
                priorityCharacter = characterName;
                return true;
            default:
                if (!GetAnyCharacterSpeaking())
                {
                    PlayAndAddCharacterDialog(characterName, soundName, is3D, parent);
                    return true;
                }
                return false;
        }
    }

    #endregion

    #region Helper Functions

    /// <summary>
    /// Returns true if there is a high-priority (interrupt mode: INTERRUPT_ALL) dialog currently speaking.
    /// </summary>
    private bool GetPriorityDialogSpeaking()
    {
        if (priorityCharacter == null)
        {
            return false;
        }
        else
        {
            return charactersSpeaking.ContainsKey(priorityCharacter) && charactersSpeaking[priorityCharacter].isPlaying;
        }
    }

    /// <summary>
    /// Returns true if this character is currently speaking.
    /// </summary>
    private bool GetThisCharacterSpeaking(string characterName)
    {
        if (charactersSpeaking.ContainsKey(characterName))
        {
            if (charactersSpeaking[characterName].isPlaying)
            {
                Debug.Log(characterName + " is speaking!");
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns true if there is any character currently speaking.
    /// </summary>
    private bool GetAnyCharacterSpeaking()
    {
        foreach (AudioSource character in charactersSpeaking.Values)
        {
            if (character.isPlaying)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Actually plays the dialog and saves a reference to the AudioSource in the Dictionary so it can be interrupted later if needed.
    /// </summary>
    private void PlayAndAddCharacterDialog(string characterName, string soundName, bool is3D, Transform parent)
    {
        if (charactersSpeaking.ContainsKey(characterName))
        {
            charactersSpeaking[characterName].Stop();
            charactersSpeaking.Remove(characterName);
        }
        AudioSource source = ProcessPlaySound(soundName, is3D, isSoundCue: false, false, false, parent: parent);
        if (source == null)
        {
            source = ProcessPlaySound(soundName, is3D, isSoundCue: true, false, false, parent: parent);
        }

        charactersSpeaking.Add(characterName, source);
    }

    /// <summary>
    /// Processes an AudioSource from a pool to play the sound.
    /// </summary>
    private AudioSource ProcessPlaySound(string name, bool is3D, bool isSoundCue, bool specificMixerGroup, bool isLooping, string mixerGroupName = "", string uniqueId = "", Transform parent = null)
    {
        AudioClip clip;
        SoundCue cue;
        AudioSource source;

        #region Setup Source (2D vs 3D)
        if (!is3D)
        {
            source = sourcePool2D.GetAudioSource();
        }
        else
        {
            source = sourcePool3D.GetAudioSource();
            source.transform.parent = parent;
            source.transform.localPosition = Vector3.zero;
        }
        #endregion

        #region Process and Play (AudioClip vs SoundCue)
        if (!isSoundCue)
        {
            clip = Array.Find(audioClips, sound => sound.name == name);
            if (clip == null)
            {
                return null;
            }
            source.volume = 1.0f;
            source.pitch = 1.0f;

            #region Set Clip AudioMixerGroup
            if (specificMixerGroup)
            {
                source.outputAudioMixerGroup = FindAudioMixerGroup(mixerGroupName);
            }
            else
            {
                source.outputAudioMixerGroup = null;
            }
            #endregion

            #region Play Clip (OneShot vs Loop)
            if (!isLooping)
            {
                source.PlayOneShot(clip);
            }
            else
            {
                StartLooping(uniqueId, clip, source);
            }
            #endregion
        }
        else
        {
            cue = Array.Find(soundCues, sound => sound.name == name);
            if (cue == null)
            {
                Debug.LogWarning("No SoundCue found in Resources folder with the name, \"" + name + ".\"");
                return source;
            }
            source.pitch = cue.GetPitch();
            source.volume = cue.GetVolume();

            #region Set Cue AudioMixerGroup
            if (specificMixerGroup)
            {
                AudioMixerGroup specifiedGroup = FindAudioMixerGroup(mixerGroupName);
                if (specifiedGroup == null)
                {
                    specifiedGroup = cue.GetAudioMixerGroup();
                }
                source.outputAudioMixerGroup = specifiedGroup;
            }
            else
            {
                source.outputAudioMixerGroup = cue.GetAudioMixerGroup();
            }
            #endregion

            #region Play Cue (OneShot vs Loop)
            if (!isLooping)
            {
                source.PlayOneShot(cue.GetRandomClip());
            }
            else
            {
                StartLooping(uniqueId, cue.GetRandomClip(), source);
            }
            #endregion
        }
        #endregion

        return source;
    }

    /// <summary>
    /// Plays the sound as a loop and saves a reference to the AudioSource in loopInstances.
    /// </summary>
    private void StartLooping(string uniqueId, AudioClip clip, AudioSource source)
    {
        if (!loopInstances.ContainsKey(uniqueId))
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
            loopInstances.Add(uniqueId, source);
        }
    }

    /// <summary>
    /// Stops the loop instance with the uniqueId.
    /// </summary>
    public void StopLooping(string uniqueId)
    {
        if (loopInstances.ContainsKey(uniqueId))
        {
            AudioSource source = loopInstances[uniqueId];
            loopInstances.Remove(uniqueId);
            source.Stop();
            source.loop = false;
            source.clip = null;
        }
    }

    /// <summary>
    /// Finds the named AudioMixerGroup from the AudioMixers from the Resources folder.
    /// </summary>
    private AudioMixerGroup FindAudioMixerGroup(string mixerGroupName)
    {
        AudioMixerGroup specifiedGroup = null;
        foreach (AudioMixer mixer in audioMixers)
        {
            AudioMixerGroup[] groups = mixer.FindMatchingGroups(mixerGroupName);

            if (groups.Length > 0)
            {
                specifiedGroup = groups[0];
                break;
            }
        }

        if (specifiedGroup == null)
        {
            Debug.LogWarning("No mixer group named \"" + mixerGroupName + "\" was found.");
        }

        return specifiedGroup;
    }

    /// <summary>
    /// The RemoveSourceOnDestroy component of any 3D sounds played will automatically call this when their parent object is destroyed. This removes them from the list of AudioSources.
    /// </summary>
    public void RemoveDestroyed3DSound(AudioSource source)
    {
        sourcePool3D.RemoveAudioSource(source);
    }

    #endregion
}

/// <summary>
/// A pool of AudioSources.
/// </summary>
public class SourcePool
{
    private List<AudioSource> sources = new List<AudioSource>();

    private int index = 0;

    private GameObject owner;

    private bool is2D = false;

    public SourcePool(GameObject owner, bool is2D)
    {
        this.owner = owner;
        this.is2D = is2D;
    }

    public AudioSource GetAudioSource()
    {
        for (int i = index; i < sources.Count; i++)
        {
            if (!sources[i].isPlaying)
            {
                index = i + 1;
                if (index >= sources.Count)
                {
                    index = 0;
                }
                return sources[i];
            }
        }

        AudioSource newSource;
        if (is2D)
        {
            newSource = owner.AddComponent<AudioSource>();
            newSource.spatialBlend = 0.0f;
        }
        else
        {
            GameObject newGameObject = new GameObject("Sound3D", typeof(RemoveSourceOnDestroy));
            newSource = newGameObject.AddComponent<AudioSource>();
            newSource.spatialBlend = 1.0f;
        }
        sources.Add(newSource);
        return newSource;
    }

    public void RemoveAudioSource(AudioSource source)
    {
        sources.Remove(source);
    }
}

// TODO add copyright and donation info and webpage

// TODO make PlayDialog Clip/Cue agnostic