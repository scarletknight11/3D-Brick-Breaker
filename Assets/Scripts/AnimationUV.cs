using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUV : MonoBehaviour {

    bool Y = true;
    public float scrollSpeed = 0.5f;
    Material mat;

    float offset;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        StartCoroutine(ScrollUV());
    }

    IEnumerator ScrollUV()
    {
        while(true)
        {
            offset = Time.time * scrollSpeed % 1;
            if(Y)
            {
                mat.mainTextureOffset = new Vector2(0, offset);
            }
            yield return null;
        }
    }

   
}
