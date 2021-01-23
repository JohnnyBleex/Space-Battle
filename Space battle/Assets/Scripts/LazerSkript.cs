using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSkript : MonoBehaviour
{

    [SerializeField] float Speed; //задаем скорость выстрела
    [SerializeField] float angle;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(angle, 0, Speed); //скорость выстрела
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
