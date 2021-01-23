using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform lazerGun;
    [SerializeField] GameObject lazerShot;
    [SerializeField] GameObject explosion;
    float nextShotTime;
    [SerializeField] float speed;
    [SerializeField] GameObject bonus;
    bool isHunter;

    Rigidbody enemyShip;
    Transform playerPosition;
    Vector3[] endPoint = new Vector3[3];

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerPosition)
        {
            enemyShip = GetComponent<Rigidbody>();
            endPoint[0] = new Vector3(-34, 0, -50);
            endPoint[1] = new Vector3(0, 0, -50);
            endPoint[2] = new Vector3(34, 0, -50);
            int i = Random.Range(0, 3);
            enemyShip.velocity = (endPoint[i] - enemyShip.transform.position).normalized * speed;
            isHunter = false;
        }
    }

    private void FixedUpdate()
    {
        if (playerPosition)
        {
            enemyShip.transform.LookAt(playerPosition);
            if (Time.time > nextShotTime)
            {
                GameObject gun = Instantiate(lazerShot, lazerGun.position, Quaternion.identity);
                gun.transform.LookAt(playerPosition);
                gun.GetComponent<Rigidbody>().velocity = (playerPosition.position - lazerGun.position).normalized * (2 * speed);

                nextShotTime = Time.time + 1;
            }

            if (isHunter)
                enemyShip.velocity = (playerPosition.position - enemyShip.transform.position).normalized * (speed * 0.5f);
            else
            {
                if (enemyShip.transform.position.z < -55) isHunter = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid" || other.tag == "Bonus" || other.tag == "Enemy" || other.tag == "EnemyLazer" || other.tag == "EnemyLazer")
        {
            return;
        }
        else if (other.tag == "BonusShip")
        {
            other.gameObject.SetActive(false);
            Instantiate(explosion, transform.position, Quaternion.identity); //показываем взрыв
            Destroy(gameObject);
            return;
        }

        ControllerScript.score += 20;
        Instantiate(explosion, transform.position, Quaternion.identity); //показываем взрыв
        Instantiate(explosion, other.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(other.gameObject);

        if (Random.Range(0, 100) <= 25)
        {
            Instantiate(bonus, transform.position, Quaternion.identity);
        }
    }
}
