using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaGauge : MonoBehaviour
{
    public void UpdateGauge(float stamina)
    {
        var slider = GetComponent<Slider>();
        slider.value = stamina;
    }

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        //if (RotationMode == false)
        //{
        //    stamina--;
        //}
        //else if (RotationMode == true)
        //{
        //    stamina++;
        //}
    }
}
