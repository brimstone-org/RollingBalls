using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallScript : MonoBehaviour {



    public enum BallColor:int { white=0,red=1,blue=2,yellow=3}
    public BallColor color;
    ConstantForce cf;
    float ratioToReality = 1.0f;

    GameObject holeDrop;

    Quaternion rotatedDirection = Quaternion.identity;

    WaitForSeconds waitOneSecond = new WaitForSeconds(0.1f);

	// Use this for initialization
	void Start () {
        cf = GetComponent<ConstantForce>();
        intarctable = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.175f);
        GetComponent<Rigidbody>().mass = UnityEngine.Random.Range(0.85f, 1.15f) * 0.3f;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


        ratioToReality = SecondBrain.instance.BallRatioToReality;
        rotatedDirection = Quaternion.AngleAxis(-90, Vector3.forward);
        SceneController.instance.AddBall(this);
	}
    Rigidbody rb;

	// Update is called once per frame
	void Update () {
        if (destroyable)
            return;
        
        gameObject.transform.Rotate(( rotatedDirection * rb.velocity.normalized) * Time.smoothDeltaTime * rb.velocity.magnitude * ratioToReality, Space.World);
		cf.force = SceneController.instance.gravity * GameRules.instance.ballSpeed;
        if (transform.position.magnitude > 25f) Destroy(gameObject);
	}



    IEnumerator WrongHole()
    {
        intarctable = false;
        Renderer rend = GetComponentInChildren<Renderer>();
        Color c1 = rend.material.GetColor("_Color");
        Color c2 = new Color(c1.r * 0.1f, c1.g * 0.1f, c1.b * 0.1f);
        for( int k=0; k<5; k++ )
        {
            yield return waitOneSecond;
            //print(1);
            rend.material.SetColor("_Color", c2);
            yield return waitOneSecond;
            //print(2);
            rend.material.SetColor("_Color", c1);
        }
        //SceneController.instance.RegenerateBalls();
        intarctable = true;

        transform.position = SceneController.instance.GetRandomBallPos();

        // for (int k = 0; k < 8; k++)
        for (int k = 0; k < Mathf.Min(SecondBrain.instance.BallsLimit - SceneController.instance.balls.Count,9); k++)
            Instantiate(SceneController.instance.whiteBallPrefab, SceneController.instance.GetRandomBallPos(), Quaternion.identity).GetComponent<BallScript>();

    }

    bool intarctable = true;

    void TryCatchBall()
    {
        if (GameRules.instance.redOn)
        {
            //TODO:Move this
           // BallScript[] balls = GameObject.FindObjectsOfType<BallScript>();
            for (int k = 0; k < SceneController.instance.balls.Count; k++)
            {
                if (SceneController.instance.balls.ElementAt(k).color == BallColor.white)
                {
                    StartCoroutine(WrongHole());
                    return;
                }
            }
        }
		if (EventDispatcher.OnBallEntersHole != null)
			EventDispatcher.OnBallEntersHole();
       // Destroy(gameObject);
        StartCoroutine(ScaleAndDestroy());
    }


    bool destroyable = false;
    IEnumerator ScaleAndDestroy()
    {
        Vector3 initialPosition = gameObject.transform.localPosition;

        destroyable = true;
		intarctable = false;
        rb.isKinematic = true;
        rb.detectCollisions = false;
        float time = SecondBrain.instance.BallOutAnimDuration;
        Vector3 initialScale = gameObject.transform.localScale;
        yield return null;
        for (float t = 0.0f; t <= 1.0f;t+= Time.smoothDeltaTime/time)
        {

            transform.localPosition = Vector3.Lerp(initialPosition, holeDrop.transform.localPosition, t);
           
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
			yield return null;
        }

       Destroy(gameObject);
        yield break;
    }

    void TryCatchColorBallInWhiteHole()
    {
       // HoleScript[] holes = FindObjectsOfType<HoleScript>();
        bool myColorPresent = false;
        for( int k=0; k<SceneController.instance.holes.Length; k++ )
        {
            if( (int)SceneController.instance.holes[k].holeType == (int)color )
            {
                myColorPresent = true;
                break;
            }
        }
        if( myColorPresent ){
            StartCoroutine(WrongHole());
        } 
        else
        {
            TryCatchBall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!intarctable) return;
        HoleScript hs = other.GetComponent<HoleScript>();
        if (hs == null) return;
        holeDrop = other.gameObject;
       
        
        if (color == BallColor.white)
        {
			if (EventDispatcher.OnBallEntersHole != null)
				EventDispatcher.OnBallEntersHole();
            if (hs.holeType == HoleScript.HoleType.white)/*Destroy(gameObject);*/StartCoroutine(ScaleAndDestroy());
            if (hs.holeType == HoleScript.HoleType.red)/* Destroy(gameObject);*/StartCoroutine(ScaleAndDestroy());
            if (hs.holeType == HoleScript.HoleType.yellow) /*Destroy(gameObject);*/StartCoroutine(ScaleAndDestroy());
            if (hs.holeType == HoleScript.HoleType.blue) /* Destroy(gameObject);*/StartCoroutine(ScaleAndDestroy());

        }else if (color == BallColor.red)
        {
            if (hs.holeType == HoleScript.HoleType.white) TryCatchColorBallInWhiteHole(); ;
            if (hs.holeType == HoleScript.HoleType.red) TryCatchBall();
            if (hs.holeType == HoleScript.HoleType.yellow) StartCoroutine(WrongHole()); 
            if (hs.holeType == HoleScript.HoleType.blue) StartCoroutine(WrongHole()); ;
        }else if (color == BallColor.blue)
        {
            if (hs.holeType == HoleScript.HoleType.white) TryCatchColorBallInWhiteHole(); ;
            if (hs.holeType == HoleScript.HoleType.red) StartCoroutine(WrongHole()); ;
            if (hs.holeType == HoleScript.HoleType.yellow) StartCoroutine(WrongHole()); 
            if (hs.holeType == HoleScript.HoleType.blue) TryCatchBall();
        }else if (color == BallColor.yellow)
        {
            if (hs.holeType == HoleScript.HoleType.white) TryCatchColorBallInWhiteHole(); ;
            if (hs.holeType == HoleScript.HoleType.red) StartCoroutine(WrongHole()); ;
            if (hs.holeType == HoleScript.HoleType.yellow) TryCatchBall();
            if (hs.holeType == HoleScript.HoleType.blue) StartCoroutine(WrongHole()); 
        }
    }

  /*  void OnCollisionEnter(Collision otherCollider){
        if (otherCollider.collider.tag != "Ball")
            return;
        if(EventDispatcher.OnBallTouch!=null)
            EventDispatcher.OnBallTouch();
    }*/

    private void OnDisable()
    {
        if(!SceneController.instance.replay)
       // Debug.Log("BALL DESTORYED, NEW COUNT AT: " + (SceneController.instance.balls.Count() - 1).ToString());
        SceneController.instance.balls.Remove(this);
        SceneController.instance.CheckCountBalls();
    }
}
