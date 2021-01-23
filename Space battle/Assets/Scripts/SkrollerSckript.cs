using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrollerSckript : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float offset = Mathf.Repeat(Time.time * speed, 130);
        transform.position = startPosition + new Vector3(0, 0, -offset);
    }
}
