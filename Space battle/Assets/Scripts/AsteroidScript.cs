using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] GameObject asteroidExplosion;
    [SerializeField] GameObject shipExplosion;
    [SerializeField] float speed; //�������� ���������
    [SerializeField] float ratationSpeed; //�������� ��������

    float size; //������ � ���������

    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(0.8f, 2.0f); //��������� ������ ����� 50% � 200%
        //�������� ���������
        Rigidbody Asteroid = GetComponent<Rigidbody>();
        Asteroid.angularVelocity = Random.insideUnitSphere * ratationSpeed;

        float speedX = 0;
        if (Random.Range(0, 100) <= 30) //� 30% ������� ��������� �������� �� ��� X
        {
            speedX = speed * Random.Range(-0.5f, 0.5f);
        }

        Asteroid.velocity = new Vector3(speedX, 0, -speed) / size; //�������� ���������
        Asteroid.transform.localScale *= size; //������ ������������ ��������
    }

    // ���������� ��� ������������ � ������ ����������� (other)
    private void OnTriggerEnter(Collider other)
    {   //���� �������� � �������� �������� �� ������������
        if(other.tag == "Asteroid" || other.tag == "Bonus" || other.tag == "Enemy" || other.tag == "EnemyLazer" || other.tag == "EnemyLazer")
        {
            return;
        }
        else if(other.tag == "BonusShip")
        {
            other.gameObject.SetActive(false); //������������ ������ � ����� "BonusShip"
            Instantiate(asteroidExplosion, transform.position, Quaternion.identity); //���������� �����
            Destroy(gameObject);
            return;
        }

        ControllerScript.score += 10; //���������� ����� �� �����������
        Instantiate(asteroidExplosion, transform.position, Quaternion.identity); //���������� �����
        if (other.tag == "Player")
        {
            Instantiate(shipExplosion, other.transform.position, Quaternion.identity);
        }
            Destroy(gameObject); // ���������� ��������
            Destroy(other.gameObject); // ���������� ������ ������
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
