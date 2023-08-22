using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
    private Sound[] sounds;

    private Dictionary<string, Sound> soundDic;

    protected override void Awake()
    {
        base.Awake();

        soundDic = new Dictionary<string, Sound>();

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            soundDic.Add(s.name, s);
        }
    }

    private void Start()
    {
        Play("CrowdNormal");
    }

    public void Play(string name)
    {
        Sound playSound;
        bool keyExists = soundDic.TryGetValue(name, out playSound);

        if(keyExists)
        {
            playSound.source.Play();
        }
        else
        {
            Debug.LogWarning($"Sound:{name} not found!");
        }
    }

    public void Stop(string name)
    {
        Sound playSound;
        bool keyExists = soundDic.TryGetValue(name, out playSound);

        if (keyExists)
        {
            playSound.source.Stop();
        }
        else
        {
            Debug.LogWarning($"Sound:{name} not found!");
        }
    }
}
