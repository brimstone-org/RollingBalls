using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour {

    public GameObject pathPoint;
    public float speed = 1f;

    public enum HoleType:int { white=0,red=1,blue=2,yellow=3}
    public HoleType holeType;

    float pathAlpha = 0;
    Vector3 initPos,pathPos;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.25f);
        initPos = transform.position;
        if (pathPoint == null) return;
        pathPos = new Vector3(pathPoint.transform.position.x, pathPoint.transform.position.y, -0.25f);
        pathPoint.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (pathPoint == null) return;
        pathAlpha += Time.deltaTime * speed * 0.5f * GameRules.instance.holeSpeed;
        if (pathAlpha >= 1f) pathAlpha -= 1f;
        if (pathAlpha < 0.5f)
        {
            float alpha = pathAlpha * 2f;
            transform.position = Vector3.Lerp(initPos, pathPos, alpha);
        }
        else
        {
            float alpha = (pathAlpha - 0.5f) * 2f;
            transform.position = Vector3.Lerp(pathPos, initPos, alpha);
        }
	}
}
