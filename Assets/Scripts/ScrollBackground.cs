using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

    public float scrollSpead;
    public float tileSize;
    private Transform currentObject;


    // Start is called before the first frame update
    void Start()
    {
        currentObject = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentObject.position = new Vector3(
            currentObject.position.x,
            currentObject.position.y,
            Mathf.Repeat(Time.time * scrollSpead, tileSize)
            );
        
    }
}
