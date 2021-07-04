using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public Boundary boundary;
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate = 0.5f;

    public float fireRate2 = 0.5f;

    public GameObject super_shot;

    public float nextFire = 0.0f;

    //Обнавляем приватную переменую
    public Quaternion calibrationQuaternion;

    public SimpleTouchPad touchPad;

    public SimpleTouchAreaButton areaButton;


    public void Start()
    {
        CalibrateAccelerometr();
    }

    public void CalibrateAccelerometr()
    {
        Vector3 accelerationSnapshot = Input.acceleration;

        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);

        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);

    }

    public Vector3 FixAcceleration (Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }



    public void Update()
    {
        if (areaButton.CantFire() && Time.time > nextFire)
        {

            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();

        }
        // если нажата правая кнопка мыши или левый alt и значение текущего времяни больше времяни когда нам можно стрелять
        if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            // то  в переменную nextFire записывается текущее время, к которому прибавляется значение fireRate2 служащее в качестве паузы
            nextFire = Time.time + fireRate2;

            // в этой строке мы создаем клон префаба супер-снаряды, назовем его super_shot
            Instantiate(super_shot, shotSpawn.position, shotSpawn.rotation);

            // звук выстрела
            GetComponent<AudioSource>().Play();
        }
    }

    private void FixedUpdate()
    {
        //Для аксилирометра
        //Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(accelerationRaw);


        //Для работы с точпада
        Vector2 direction = touchPad.GetDirection();


        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(
            0f,
            0f,
            GetComponent<Rigidbody>().velocity.x * -tilt
            );
        //Для работы с клавы
        //GetComponent<Rigidbody>().velocity = new Vector3(moveHorizontal, 0f, moveVertical) * Speed;
        GetComponent<Rigidbody>().velocity = new Vector3(direction.x, 0f, direction.y) * Speed;

        GetComponent<Rigidbody>().position = new Vector3
            (

            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

    }
}
