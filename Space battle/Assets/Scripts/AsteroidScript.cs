using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] GameObject asteroidExplosion;
    [SerializeField] GameObject shipExplosion;
    [SerializeField] float speed; //скорость астероида
    [SerializeField] float ratationSpeed; //скопость вращения

    float size; //размер в процентах

    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(0.8f, 2.0f); //случайный размер между 50% и 200%
        //вращение астероида
        Rigidbody Asteroid = GetComponent<Rigidbody>();
        Asteroid.angularVelocity = Random.insideUnitSphere * ratationSpeed;

        float speedX = 0;
        if (Random.Range(0, 100) <= 30) //в 30% случаев добавляем скорость по оси X
        {
            speedX = speed * Random.Range(-0.5f, 0.5f);
        }

        Asteroid.velocity = new Vector3(speedX, 0, -speed) / size; //скорость астероида
        Asteroid.transform.localScale *= size; //размер относительно скорости
    }

    // Вызывается при столкновении с другим коллайдером (other)
    private void OnTriggerEnter(Collider other)
    {   //теги объектов с которыми астероид не сталкивается
        if(other.tag == "Asteroid" || other.tag == "Bonus" || other.tag == "Enemy" || other.tag == "EnemyLazer" || other.tag == "EnemyLazer")
        {
            return;
        }
        else if(other.tag == "BonusShip")
        {
            other.gameObject.SetActive(false); //деактивируем объект с тэгом "BonusShip"
            Instantiate(asteroidExplosion, transform.position, Quaternion.identity); //показываем взрыв
            Destroy(gameObject);
            return;
        }

        ControllerScript.score += 10; //начисление очков за уничтожение
        Instantiate(asteroidExplosion, transform.position, Quaternion.identity); //показываем взрыв
        if (other.tag == "Player")
        {
            Instantiate(shipExplosion, other.transform.position, Quaternion.identity);
        }
            Destroy(gameObject); // уничтожаем астероид
            Destroy(other.gameObject); // уничтожаем другой объект
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
