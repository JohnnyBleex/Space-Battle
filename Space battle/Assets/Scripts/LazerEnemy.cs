using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEnemy : MonoBehaviour
{
    [SerializeField] GameObject explosionShip;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid" || other.tag == "Bonus" || other.tag == "Enemy" || other.tag == "Lazer")
        {
            return;
        }
        else if (other.tag == "BonusShip")
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        Instantiate(explosionShip, other.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
