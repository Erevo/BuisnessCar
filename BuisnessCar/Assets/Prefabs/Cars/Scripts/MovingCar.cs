using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading;
using TouchControlsKit;

public class MovingCar : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] bool IsRmotorUsing = true;
    [SerializeField] bool IsFmotorUsing = true;
    [SerializeField] Transform centerOfmass;
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float maxTorque = 1000f;
    [SerializeField] public double Speed;

    [SerializeField] Text txt_speedometr;

    WheelJoint2D FWheel;
    WheelJoint2D RWheel;

    JointMotor2D FMotor;
    JointMotor2D RMotor;

    Vector3 scale;

    void Start()
    {
        txt_speedometr = GameObject.FindGameObjectWithTag("Speedometr").GetComponent<Text>();

        rb = GetComponent<Rigidbody2D>();
        scale = GetComponent<Transform>().localScale;
        var Wheels = GetComponents<WheelJoint2D>();
        FWheel = Wheels[0];
        RWheel = Wheels[1];

        FMotor = FWheel.motor;
        RMotor = RWheel.motor;

        rb.centerOfMass = centerOfmass.localPosition * transform.localScale.y;
    }

    void Update()
    {
        FWheel.motor = FMotor;
        RWheel.motor = RMotor;
        FWheel.useMotor = IsFmotorUsing;
        RWheel.useMotor = IsRmotorUsing;
        if (!GetComponent<CarProfile>().isActive)
        {
            FMotor.motorSpeed = 0;
            RMotor.motorSpeed = 0;

            //FWheel.motor = FMotor;
            //RWheel.motor = RMotor;

            return;
        }



        float horizontal = TCKInput.GetAxis("Joystick", EAxisType.Horizontal);
        if (horizontal < 0 && rb.velocity.x < 0)
            transform.localScale = new Vector3(-scale.x, transform.localScale.y, transform.localScale.z);
        else
        if (horizontal > 0 && rb.velocity.x > 0)
            transform.localScale = new Vector3(scale.x, transform.localScale.y, transform.localScale.z);

        float radius = GetComponentInChildren<CircleCollider2D>().radius * transform.localScale.y * GetComponentsInChildren<Transform>()[1].localScale.y;
        Speed = 180f / (radius * Mathf.PI) * ((maxSpeed / 3.6f) * -horizontal);



        txt_speedometr.text = $"{Mathf.Abs(Mathf.RoundToInt(rb.velocity.x * 3.6f))}";


        FMotor.motorSpeed = (float)Speed;
        RMotor.motorSpeed = (float)Speed;

        RMotor.maxMotorTorque = maxTorque;
        FMotor.maxMotorTorque = maxTorque;


        List<int> layers = new List<int>
        {
            LayerMask.NameToLayer("Car"),
            LayerMask.NameToLayer("Ignore Character"),
        };
        if (TCKInput.GetAction("jumpBtn", EActionEvent.Down))
        {
            int lastLayer = -1;
            foreach (var layer in layers)
            {
                Physics2D.IgnoreLayerCollision(layer, layer, true);
                if (lastLayer != -1)
                    Physics2D.IgnoreLayerCollision(layer, lastLayer, true);
                lastLayer = layer;
            }
        }
        if (TCKInput.GetAction("jumpBtn", EActionEvent.Up))
        {
            int lastLayer = -1;
            foreach (var layer in layers)
            {
                Physics2D.IgnoreLayerCollision(layer, layer, false);
                if (lastLayer != -1)
                    Physics2D.IgnoreLayerCollision(layer, lastLayer, false);
                lastLayer = layer;
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //        Gizmos.DrawSphere(rb.worldCenterOfMass, 0.3f);
    //}
}