using System.Collections;
using System.Collections.Generic;
using ColoringProject;
using UnityEngine;

public class ColorButtonOneSoundPlayAndParticles : OneSoundPlay
{
    [SerializeField] private ParticleSystem pressEffect;


     void Start()
    {
        base.Awake();
        Vector3 globalPosition = GetComponent<RectTransform>().position;
        //yield return new WaitForEndOfFrame();
        transform.SetParent(transform.parent.parent.parent.parent);
        //yield return new WaitForEndOfFrame();
        GetComponent<RectTransform>().position = globalPosition;
    }
     

    public override void Play(int sound = 0)
    {
        if (ColoringManager.Instance.IsColoring)
        {
            if (clips.Count > 0)
            {
                base.Play(1);
            }
        }
        else
        {
            base.Play(sound);
            if(pressEffect!=null) pressEffect.Play();
        }
    }
}
