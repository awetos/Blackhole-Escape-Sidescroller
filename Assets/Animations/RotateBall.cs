using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBall : MonoBehaviour
{
    Quaternion rotate;
    float z;
    Sprite sprite;
    
    // Start is called before the first frame update
    void Start()
    {
         
        z = Mathf.Round(Random.Range(0f, 360f));
        rotate = new Quaternion(0f, 0f, z, -1f);
        sprite = transform.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        z += Time.deltaTime;
        rotate.z = z;
        transform.rotation = rotate;
    }
}
