using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    [SerializeField] GameObject[] asteroids;
    [SerializeField] GameObject shipEnemy;
    [SerializeField] GameObject bonus;
    [SerializeField] float minDelay, maxDelay; //�������� ������� ����������
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
        // ���������� �������� �������� ���������� 
        while (maxDelay >= (minDelay + 0.5f) && ControllerScript.score >= (score + 500))
        {
            maxDelay -= 0.6f;
            score += 500;
        }

        if (Time.time > nextLaunchTime)
        {
            //�������� ��������� ����� �� ��� X (����� ��� ������)
            float left = -transform.localScale.x / 2;
            float right = transform.localScale.x / 2;
            float posX = Random.Range(left, right);
            float posY = transform.position.y;
            float posZ = transform.position.z;

            // ������������ ����������
            int i = Random.Range(0, asteroids.Length);

            //������� ��������
            Instantiate(asteroids[i], new Vector3(posX, posY, posZ), Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }

        //��������� ���������� ������ 15 ������
        timeLeftSpawn -= Time.deltaTime;
        if (timeLeftSpawn <= 0)
        {
            Debug.Log("������ " + timeLeftSpawn);
            timeLeftSpawn = 15;
            float left = -transform.localScale.x / 2;
            float right = transform.localScale.x / 2;
            float posX = Random.Range(left, right);
            float posY = transform.position.y;
            float posZ = transform.position.z;
            Instantiate(shipEnemy, new Vector3(posX, posY, posZ), Quaternion.Euler(0, 180, 0));
        }

        //��������� ������ ����� ������ 90 ������
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            timeLeft  = 60;
            float left = -transform.localScale.x / 2;
            float right = transform.localScale.x / 2;
            float posX = Random.Range(left, right);
            float posY = transform.position.y;
            float posZ = transform.position.z;
            //Debug.Log("������ " + timeLeft + " ������");
            Instantiate(bonus, new Vector3(posX, posY, posZ), Quaternion.identity);
        }
    }
}
