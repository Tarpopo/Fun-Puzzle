using System.Collections.Generic;
using ColoringProject;
using UnityEngine;

public class OneSoundPlay : MonoBehaviour
{
    [SerializeField] private AudioSource source = null;

    [SerializeField] protected List<AudioClip> clips;
    [SerializeField] private bool randomizePitch = false;

    protected virtual void Awake()
    { 
        if (source == null)
        {
            source = GetComponent<AudioSource>();
            if (source == null)
            {
                source = gameObject.AddComponent<AudioSource>();
                source.playOnAwake = false;
            }
        }
    } 

    public virtual void Play(int sound = 0)
    {
        if (sound >= clips.Count) return;
        if(SingletoneGameLogic.Instance!=null) source.volume = SingletoneGameLogic.Instance.SoundVolime;
        source.clip = clips[sound];
        if (randomizePitch) source.pitch = Random.Range(0.7f, 1.3f);
        source.Play();
    }

}
