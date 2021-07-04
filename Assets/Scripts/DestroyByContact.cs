using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;

    public GameObject explosionPlayer;

    private GameObject cloneExplosion;

    public int scoreValue;

    private GameController gameController;

    public GameObject mega_exp;


    private void Start()
    {
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null)
        {
            gameController = GameControllerObject.GetComponent<GameController>();

        }

        if (GameControllerObject == null)
        {
            Debug.Log("Скрипт 'GameController' не найден");

        } 
    }

    //функция OnParticleCollision обрабатывает столкновения
    //если любая частица Particle System столкнулась с обьектом на котором находится этот скрипт
    //то отрабатывает код внутри функции OnParticleCollision при условии, что в
    //обьекте Particle System который породил частицу, была включена опция Send Collision Messages.

    private void OnParticleCollision()
    {
        // клонируем префаб взрыва
        cloneExplosion = (GameObject)Instantiate(mega_exp, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);

        //уничтожаем обьект на котором весит этот скрипт, допустим это астероид
        Destroy(gameObject);

        // уничтожаем клон клон взрыва после того как он отработал
        Destroy(cloneExplosion, 0.7f);

        // насчитываем очки за уничтожение вражеского обьекта
        gameController.AddScore(scoreValue);

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cloneExplosion = Instantiate(explosionPlayer, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation) as GameObject;

            gameController.GameOver();

            Destroy(other.gameObject);
            Destroy(gameObject);

            Destroy(cloneExplosion, 1f);

        }


        if (other.tag == "Bolt")
        {
            cloneExplosion = Instantiate(explosion, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation) as GameObject;
             

            Destroy(other.gameObject);
            Destroy(gameObject);

            Destroy(cloneExplosion, 1f);

            gameController.AddScore(scoreValue);


        }
        

    }
}
