using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    [SerializeField] GameObject[] asteroids;
    [SerializeField] GameObject shipEnemy;
    [SerializeField] GameObject bonus;
    [SerializeField] float minDelay, maxDelay; //задержка созданя астероидов
    float timeLeft = 90;
    float timeLeftSpawn = 15;
    float nextLaunchTime;
    [SerializeField] float score;

    private void Start()
    {
        timeLeft += 0.02f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // уменьшение задержки создания астероидов 
        while (maxDelay >= (minDelay + 0.5f) && ControllerScript.score >= (score + 500))
        {
            maxDelay -= 0.6f;
            score += 500;
        }

        if (Time.time > nextLaunchTime)
        {
            //вибираем случайную точку по оси X (слева или справа)
            float left = -transform.localScale.x / 2;
            float right = transform.localScale.x / 2;
            float posX = Random.Range(left, right);
            float posY = transform.position.y;
            float posZ = transform.position.z;

            // Рандомизация астероидов
            int i = Random.Range(0, asteroids.Length);

            //создаем астероид
            Instantiate(asteroids[i], new Vector3(posX, posY, posZ), Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }

        //появление противника каждые 15 секунд
        timeLeftSpawn -= Time.deltaTime;
        if (timeLeftSpawn <= 0)
        {
            Debug.Log("Прошло " + timeLeftSpawn);
            timeLeftSpawn = 15;
            float left = -transform.localScale.x / 2;
            float right = transform.localScale.x / 2;
            float posX = Random.Range(left, right);
            float posY = transform.position.y;
            float posZ = transform.position.z;
            Instantiate(shipEnemy, new Vector3(posX, posY, posZ), Quaternion.Euler(0, 180, 0));
        }

        //появление бонуса через каждые 90 секунд
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            timeLeft  = 60;
            float left = -transform.localScale.x / 2;
            float right = transform.localScale.x / 2;
            float posX = Random.Range(left, right);
            float posY = transform.position.y;
            float posZ = transform.position.z;
            //Debug.Log("ПРОШЛО " + timeLeft + " СЕКУНД");
            Instantiate(bonus, new Vector3(posX, posY, posZ), Quaternion.identity);
        }
    }
}
