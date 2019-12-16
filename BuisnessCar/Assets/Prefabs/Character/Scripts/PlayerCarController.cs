using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchControlsKit;

public class PlayerCarController : MonoBehaviour
{
    GameObject characher;
    CircleCollider2D trigger;
    CarProfile car;
    Profile profile;
    [SerializeField] CanvasGroup speedcanv;
    void Start()
    {
        characher = transform.GetChild(0).gameObject;
        trigger = GetComponentInChildren<CircleCollider2D>();
        profile = GetComponentInChildren<Profile>();
    }

    void Update()
    {
        if (TCKInput.GetAction("leavecarBtn", EActionEvent.Down))
            LeaveCar();
        if (TCKInput.GetAction("getcarBtn", EActionEvent.Down))
            GoToCar();

        if (characher.activeSelf)
        {
            car = FindCar();

            if (car != null && profile.Cars.Contains(car))
            {
                TCKInput.SetControllerEnable("getcarBtn", true);
            }
            else
            {
                TCKInput.SetControllerEnable("getcarBtn", false);
            }
        }
    }

    public void GoToCar()
    {
        car.tag = "Player";
        car.isActive = true;

        TCKInput.SetControllerEnable("getcarBtn", false);
        TCKInput.SetControllerEnable("leavecarBtn", true);


        speedcanv.alpha = 1;
        characher.SetActive(false);
    }

    public void LeaveCar()
    {
        TCKInput.SetControllerEnable("getcarBtn", true);
        TCKInput.SetControllerEnable("leavecarBtn", false);

        car.isActive = false;
        car.tag = "Untagged";
        characher.transform.position = car.transform.position;
        speedcanv.alpha = 0;
        characher.SetActive(true);

    }

    CarProfile FindCar()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(characher.transform.position, trigger.radius, LayerMask.GetMask("Car"));

        Collider2D currentCollider = null;
        float dist = Mathf.Infinity;

        foreach (Collider2D coll in colliders)
        {
            float currentDist = Vector3.Distance(transform.position, coll.transform.position);
            if (currentDist < dist)
            {
                currentCollider = coll;
                dist = currentDist;
            }
        }
        return (currentCollider != null) ? currentCollider.GetComponent<CarProfile>() : null;
    }
}
