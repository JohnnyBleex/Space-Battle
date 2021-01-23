using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerBladeScript : MonoBehaviour
{
    [SerializeField] GameObject LazrShot; //��������
    [SerializeField] GameObject lazerShotBLeft; //��������
    [SerializeField] GameObject lazerShotBRight;
    [SerializeField] Transform LazerGun1; //�����1
    [SerializeField] Transform LaserGun2; //�����2
    [SerializeField] Transform miniGun1;
    [SerializeField] Transform miniGun2;
    [SerializeField] Rigidbody StarShip;
    public GameObject shild;
    float shotDelay = 0.35f; // �������� ��������
    float Speed = 50; //���������� �������� ������������
    float Tilt = 0.6f; // ���������� �������
    float xMax = 39, xMin = -39, zMax = 50, zMin = -55;
    float nextShotTime = 0;
    float nextShotTime2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        StarShip = GetComponent<Rigidbody>();
        shild.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); //���� ������ �� �����������
        float moveVertical = Input.GetAxis("Vertical"); //���� ������ �� ���������

        StarShip.velocity = new Vector3(moveHorizontal, 0, moveVertical) * Speed; //�������� ������������

        //����� �������� ����
        float clampedX = Mathf.Clamp(StarShip.position.x, xMin, xMax);
        float clampedZ = Mathf.Clamp(StarShip.position.z, zMin, zMax);
        StarShip.position = new Vector3(clampedX, 0, clampedZ);

        StarShip.rotation = Quaternion.Euler(StarShip.velocity.z * Tilt, 0, -StarShip.velocity.x * Tilt);//������ ��� ��������

        // ������ �������
        if (Time.time> nextShotTime && Input.GetButton("Fire1"))
        {
            Instantiate(LazrShot, LazerGun1.position, Quaternion.identity);
            Instantiate(LazrShot, LaserGun2.position, Quaternion.identity);

            nextShotTime = Time.time + (shotDelay * 1.8f);
        }
        if (Time.time > nextShotTime2 && Input.GetButton("Fire2"))
        {
            Instantiate(lazerShotBLeft, miniGun1.position, Quaternion.Euler(0, -45, 0));
            Instantiate(lazerShotBRight, miniGun2.position, Quaternion.Euler(0, 45, 0));
            nextShotTime2 = Time.time + (shotDelay * 0.9f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bonus")
        {
            shild.SetActive(true);
        }
    }
}
