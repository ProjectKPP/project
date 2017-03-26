using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarScripts : MonoBehaviour {

    WheelJoint2D[] wheelJoints; //колесо
    JointMotor2D frontWheel; //переднє
    JointMotor2D backWheel; //заднє

    public float maxSpeed = -1000f; //максимальна швидкість
    private float maxBackSpeed = 1500f; //макс. від'ємна шв.
    private float acceleration = 400f; //прискорення
    private float deacceleration = -100f; //сповільнення
    public float brakeForce = 3000f; //тормоз
    private float gravity = 9.8f; //сила тяжіння
    private float angleCar = 0; //кут нахилу машини
    public bool grouned; //зчеплення машини з трасою
    public LayerMask map; //шар траси
    public Transform bWheel; //зчеплення задн. колеса

    public CkickScripts[] ControlCar; //керування машиною

	// Use this for initialization
	void Start () {
        wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        backWheel = wheelJoints[1].motor;
        frontWheel = wheelJoints[0].motor;
	}

    void Update()
    {
        grouned = Physics2D.OverlapCircle(bWheel.transform.position, 0.6f, map);
    }

    void FixedUpdate () {

        frontWheel.motorSpeed = backWheel.motorSpeed;

        angleCar = transform.localEulerAngles.z;

        if (angleCar >= 180)
        {
            angleCar = angleCar - 360;
        }

        if (grouned == true)
        {
            if (ControlCar[0].clickedIs == true) //газ нажатий
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar / 180) * 100) * Time.deltaTime, maxSpeed, maxBackSpeed);
            }
            else if ((backWheel.motorSpeed < 0) || (ControlCar[0].clickedIs == false && backWheel.motorSpeed == 0 && angleCar < 0))
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (deacceleration - gravity * Mathf.PI * (angleCar / 180) * 100) * Time.deltaTime, maxSpeed, 0);
            }
            if ((ControlCar[0].clickedIs == false && backWheel.motorSpeed > 0) || (ControlCar[0].clickedIs == false && backWheel.motorSpeed == 0 && angleCar > 0))
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (-deacceleration - gravity * Mathf.PI * (angleCar / 180) * 100) * Time.deltaTime, 0, maxBackSpeed);
            }
        }
        else if (ControlCar[0].clickedIs == false && backWheel.motorSpeed < 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - deacceleration * Time.deltaTime, maxSpeed, 0);
        }
        else if (ControlCar[0].clickedIs == false && backWheel.motorSpeed > 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + deacceleration * Time.deltaTime, 0, maxBackSpeed);
        }

        if(ControlCar[0].clickedIs==true && grouned == false)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar / 180) * 100) * Time.deltaTime, maxSpeed, maxBackSpeed);
        }

        if (ControlCar[1].clickedIs == true && backWheel.motorSpeed > 0) //тормоз нажатий
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - brakeForce * Time.deltaTime, 0, maxBackSpeed);
        }
        else if (ControlCar[1].clickedIs == true && backWheel.motorSpeed < 0)
        {
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + brakeForce * Time.deltaTime, maxSpeed, 0);
        }

        wheelJoints[1].motor = backWheel;
        wheelJoints[0].motor = frontWheel;
    }
}
