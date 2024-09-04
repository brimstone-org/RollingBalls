using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource ballHitSound;
    public AudioSource music;
    public AudioSource ballHoleSound;


    WaitForSeconds ballHoleSoundDur;
    public AudioSource[] ballHoleSounds= new AudioSource[6];

    public Stack<int> holeSoundStack = new Stack<int>();
    static int soundState = 1; 
    public static int SoundState{
        get{
            return soundState;
        }
        set{
            soundState=value;
            instance.SoundStateChanged();
        }
    }

    public static SoundManager instance { get; set; }

    void Awake(){
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }

    void Start(){
        if (soundState == 0)
            return;
        music.Play();
        ballHoleSoundDur = new WaitForSeconds(ballHoleSound.clip.length);
        InitHolesSound();
    }


    public void SoundStateChanged(){
        if (soundState == 0)
            music.Stop();
        else
        {
            music.Play();
            InitHolesSound();
        }
    }

    public void InitHolesSound(){
        for (int i = 0; i < ballHoleSounds.Length; i++)
        {
            ballHoleSounds[i] = Instantiate(ballHoleSound, Vector3.zero, Quaternion.identity, gameObject.transform);
            holeSoundStack.Push(i);
        }
    }

    public void PlayBallHit(){
        if (soundState == 0)
            return;

        ballHitSound.Play();
    }

    public void PlayBallHole(){
        if (soundState == 0)
            return;

        int queueIndex;
        if(holeSoundStack.Count>0)
        {
            queueIndex = holeSoundStack.Pop();
            ballHoleSounds[queueIndex].Play();
            StartCoroutine(ResetBallSound(queueIndex));
        }
    }

    IEnumerator ResetBallSound(int i)
    {
        yield return ballHoleSoundDur;
        holeSoundStack.Push(i);
        yield break;
    }
}
