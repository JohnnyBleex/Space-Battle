using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusScript : MonoBehaviour
{

    [SerializeField] GameObject bonusBoom;
    [SerializeField] float speed; //�������� ������

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody bonus = GetComponent<Rigidbody>();
        bonus.velocity = new Vector3(0, 0, -speed); //�������� �� ��� Z
    }

    private void OnTriggerEnter(Collider other)
    {   //���� �������� � �������� ����� �� ������������
        if (other.tag == "Asteroid" || other.tag == "Lazer" || other.tag == "Enemy" || other.tag == "Bonus" || other.tag == "EnemyLazer" || other.tag == "EnemyLazer" || other.tag == "Lazer")
        {
            return;
        }
        Instantiate(bonusBoom, transform.position, Quaternion.identity); //���������� �����
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
