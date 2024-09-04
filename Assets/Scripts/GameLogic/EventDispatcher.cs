using UnityEngine;

public class EventDispatcher : MonoBehaviour {
    public static EventDispatcher instance { get; set; }

    void Awake(){
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public delegate void GameEvent();

    public static GameEvent OnBallTouch;
    public static GameEvent OnBallEntersHole;

    public void OnEnable()
    {
        OnBallTouch += BallTouchSound;
        OnBallEntersHole += BallEntersHole;
    }

    public void OnDisable()
    {
        OnBallTouch -= BallTouchSound;
        OnBallEntersHole -= BallEntersHole;
    }

    public void BallTouchSound(){
        SoundManager.instance.PlayBallHit();
    }

    public void BallEntersHole(){
        SoundManager.instance.PlayBallHole();
    }
}
