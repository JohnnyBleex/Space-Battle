using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerBladeScript : MonoBehaviour
{
    [SerializeField] GameObject LazrShot; //ВыстрелА
    [SerializeField] GameObject lazerShotBLeft; //ВыстрелБ
    [SerializeField] GameObject lazerShotBRight;
    [SerializeField] Transform LazerGun1; //Пушка1
    [SerializeField] Transform LaserGun2; //пушка2
    [SerializeField] Transform miniGun1;
    [SerializeField] Transform miniGun2;
    [SerializeField] Rigidbody StarShip;
    public GameObject shild;
    float shotDelay = 0.35f; // задержка выстрела
    float Speed = 50; //коэффицент скорости передвижения
    float Tilt = 0.6f; // коэффицент наклона
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
        float moveHorizontal = Input.GetAxis("Horizontal"); //Куда лететь по горизонтали
        float moveVertical = Input.GetAxis("Vertical"); //Куда лететь по вертикали

        StarShip.velocity = new Vector3(moveHorizontal, 0, moveVertical) * Speed; //Скорость передвижения

        //рамки игрового поля
        float clampedX = Mathf.Clamp(StarShip.position.x, xMin, xMax);
        float clampedZ = Mathf.Clamp(StarShip.position.z, zMin, zMax);
        StarShip.position = new Vector3(clampedX, 0, clampedZ);

        StarShip.rotation = Quaternion.Euler(StarShip.velocity.z * Tilt, 0, -StarShip.velocity.x * Tilt);//наклон при движении

        // делаем выстрел
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
