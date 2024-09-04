using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneController : MonoBehaviour {

    static public SceneController instance;
    public GUISkin skin;

    public float gravityAmplifier = 5f;
    public Vector3 gravity;

    public GameObject leftWall, rightWall;

    public GameObject whiteBallPrefab;
    public GameObject redBallPrefab;
    public GameObject blueBallPrefab;
    public GameObject yellowBallPrefab;

    public int whiteBalls = 10;
    public int redBalls = 1;
    public int blueBalls = 1;
    public int yellowBalls = 1;

    public bool replay = false;

    public HoleScript[] holes;

    public LinkedList<BallScript> balls = new LinkedList<BallScript>();

    public Camera mainCam;

    void Awake()
    {
        instance = this;
    }

    float maxX, maxY;

    float touchKoeff;

	// Use this for initialization
	void Start () {
        replay = false;
        touchKoeff = 15f/Mathf.Min((float)Screen.width, (float)Screen.height);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            float sideWallPos = 5f * (float)Screen.width / (float)Screen.height;
            leftWall.transform.position = Vector3.left * (sideWallPos + 0.5f);
            rightWall.transform.position = Vector3.right * (sideWallPos + 0.5f);
            maxY = 5f;
            maxX = 5f * (float)Screen.width / (float)Screen.height;
        }
        else
        {
            mainCam.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            mainCam.orthographicSize = 5f * (float)Screen.height / (float)Screen.width;
            float sideWallPos = 5f * (float)Screen.height / (float)Screen.width;
            leftWall.transform.position = Vector3.left * (sideWallPos + 0.5f);
            rightWall.transform.position = Vector3.right * (sideWallPos + 0.5f);
            maxX = 5f;
            maxY = 5f * (float)Screen.height / (float)Screen.width;
        }

        gravityAmplifier = 40f;

		holes = FindObjectsOfType<HoleScript>();

        //InvokeRepeating("CheckCountBalls", 0.5f, 0.5f);

    }

    public GameObject winUI;
    public GameObject[] nonWinUI;

    public void CheckCountBalls()
    {
       // BallScript[] balls = GameObject.FindObjectsOfType<BallScript>();
        if (balls.Count == 0 && winUI!=null && !winUI.activeSelf )
        {
//            GameRules.instance.ScoreWin();  
            GameRules.instance.SaveProgress();
            winUI.SetActive(true);
            for( int k=0; k<nonWinUI.Length; k++ ) nonWinUI[k].SetActive(false);

        }
    }


    public void AddBall(BallScript ball)
    {
        balls.AddLast(ball);
    }

    public Vector3 GetRandomBallPos()
    {
        Vector3 v = Vector3.zero;
		// BallScript[] tempBalls = GameObject.FindObjectsOfType<BallScript>();
		/*
		 for ( int k=0; k<100; k++ )
		 {
			 v = new Vector3(UnityEngine.Random.Range(-maxX + 0.6f, maxX - 0.6f), UnityEngine.Random.Range(-maxY + 0.6f, maxY - 0.6f), -0.25f);
			 bool flagOK = true;
			 for( int i=0; i<holes.Length; i++ )
			 {
				 if( Vector3.Distance(holes[i].transform.position,v)<1.2f )
				 {
					 flagOK = false;
					 break;
				 }
			 }
			 for (int i = 0; i < balls.Count; i++)
			 {
				 if (Vector3.Distance(balls.ElementAt(i).transform.position, v) < 0.7f)
				 {
					 flagOK = false;
					 break;
				 }
			 }
			 if (flagOK) break;
		 }*/

		for (int k = 0; k < 100; k++)
		{
			v = new Vector3(UnityEngine.Random.Range(-maxX + 0.6f, maxX - 0.6f), UnityEngine.Random.Range(-maxY + 0.6f, maxY - 0.6f), -0.25f);
			bool flagOK = true;
			for (int i = 0; i < holes.Length; i++)
			{
				if (Vector3.Distance(holes[i].transform.position, v) < 1.2f)
				{
					flagOK = false;
					break;
				}
			}
			if (flagOK) break;
		}

        return v;
    }

    public void RegenerateBalls()
    {
        //BallScript[] balls = GameObject.FindObjectsOfType<BallScript>();
        for( int k=0; k<balls.Count; k++ )
        {
            Destroy(balls.ElementAt(k).gameObject);
        }
        holes = FindObjectsOfType<HoleScript>();
        for (int k = 0; k < whiteBalls; k++ )  Instantiate(whiteBallPrefab, GetRandomBallPos(), Quaternion.identity);
        for (int k = 0; k < redBalls; k++) Instantiate(redBallPrefab, GetRandomBallPos(), Quaternion.identity);
        for (int k = 0; k < blueBalls; k++) Instantiate(blueBallPrefab, GetRandomBallPos(), Quaternion.identity);
        for (int k = 0; k < yellowBalls; k++) Instantiate(yellowBallPrefab, GetRandomBallPos(), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
//#if UNITY_EDITOR
        //gravity = new Vector3(0f,-1f,0f);
//#else
        Vector3 gravityR = GameRules.instance.accQuat * Input.acceleration;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            if (GameRules.instance.controlBallsType == 0)
            {
                gravity = new Vector3(gravityR.y, -gravityR.x, 0.05f) * gravityAmplifier;
            }
            else if (GameRules.instance.controlBallsType == 1)
            {
                Vector2 swipeSpeed = Vector2.zero;
                for (int k = 0; k < Input.touchCount; k++)
                {
                    Touch t = Input.GetTouch(k);
                    if (t.deltaTime > 0.001 && t.deltaPosition.magnitude > 0.1f)
                    {
                        swipeSpeed += touchKoeff * GameRules.instance.ballSpeed * t.deltaPosition / t.deltaTime;
                    }
                }
                gravity = new Vector3(swipeSpeed.x, swipeSpeed.y, 1f);
            }
            else if (GameRules.instance.controlBallsType == 2)
            {
                gravity = new Vector3(gravityR.y, -gravityR.x, 0.05f) * gravityAmplifier;
                Vector2 swipeSpeed = Vector2.zero;
                for (int k = 0; k < Input.touchCount; k++)
                {
                    Touch t = Input.GetTouch(k);
                    if (t.deltaTime > 0.001 && t.deltaPosition.magnitude > 0.1f)
                    {
                        swipeSpeed += touchKoeff * GameRules.instance.ballSpeed * t.deltaPosition / t.deltaTime;
                    }
                }
                gravity += new Vector3(swipeSpeed.x, swipeSpeed.y, 1f);
            }
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            if (GameRules.instance.controlBallsType == 0)
            {
                gravity = new Vector3(gravityR.x, gravityR.y, 0.05f) * gravityAmplifier;
            }
            else if (GameRules.instance.controlBallsType == 1)
            {
                Vector2 swipeSpeed = Vector2.zero;
                for (int k = 0; k < Input.touchCount; k++)
                {
                    Touch t = Input.GetTouch(k);
                    if (t.deltaTime > 0.001 && t.deltaPosition.magnitude > 0.1f)
                    {
                        swipeSpeed += touchKoeff * GameRules.instance.ballSpeed * t.deltaPosition / t.deltaTime;
                    }
                }
                gravity = new Vector3(swipeSpeed.x, swipeSpeed.y, 1f);
            }
            else if (GameRules.instance.controlBallsType == 2)
            {
                gravity = new Vector3(gravityR.x, gravityR.y, 0.05f) * gravityAmplifier;
                Vector2 swipeSpeed = Vector2.zero;
                for (int k = 0; k < Input.touchCount; k++)
                {
                    Touch t = Input.GetTouch(k);
                    if (t.deltaTime > 0.001 && t.deltaPosition.magnitude > 0.1f)
                    {
                        swipeSpeed += touchKoeff * GameRules.instance.ballSpeed * t.deltaPosition / t.deltaTime;
                    }
                }
                gravity += new Vector3(swipeSpeed.x, swipeSpeed.y, 1f);
            }
        }
        
        if( gravity.magnitude>50f*GameRules.instance.ballSpeed)
        {
            gravity = gravity.normalized * 50f * GameRules.instance.ballSpeed;
        }


        if( Application.isEditor )
        {
            gravity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.05f) * gravityAmplifier * 0.05f;
        }

//#endif
    }

    private void OnGUI()
    {
        //GUI.skin = skin;
        //GUI.Box(new Rect(0f, 0f, 400f, 100f), "" + gravity);
    }
}
