using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBG : MonoBehaviour
{
  

    public void ResizeSpriteToScreen()
    {
        var sr = GetComponent<MeshRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        var width = sr.bounds.size.x;
        var height = sr.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        double x = worldScreenWidth / width;
        double y = worldScreenHeight / height;
        transform.localScale = new Vector3((float)x,(float)y,1);
    }
}
