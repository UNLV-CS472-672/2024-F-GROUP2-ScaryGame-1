using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CeilingLampLogic : MonoBehaviour
{
    public Transform AntagonistTransform;

    private const float LIGHTOFFRADIUS = 1.5f;
    private const float OFFTIME = 2.0f;

    private float gametime_at_off = 0f;
    private bool light_on = true;
    private Light ThisLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize light object
        ThisLight = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the light is off or on
        if (light_on)
        {
            // Light is on - check that the antagonist is nearby, then flicker the light off
            if (Vector3.Distance(this.gameObject.transform.position, AntagonistTransform.position) < LIGHTOFFRADIUS)
            {
                LightFlickerOff();
            }
        }
        else
        {
            // Light is off - check that it has been at least x seconds since it turned off
            if ((Time.realtimeSinceStartup - gametime_at_off) > OFFTIME)
            {
                // Light was turned off over x seconds ago, check that the antagonist is not near. then turn the light back on
                if (Vector3.Distance(this.gameObject.transform.position, AntagonistTransform.position) > LIGHTOFFRADIUS)
                {
                    LightFlickerOn();
                }
            }
        }
    }

    // A function that is called to turn the light object on
    private void LightFlickerOff()
    {
        light_on = false;
        gametime_at_off = Time.realtimeSinceStartup;
        StartCoroutine(FlickerOffTimedActions());
    }

    // A time-based, blocking function that turns the light object of the lamp off, on, then off to emulate "flickering" off
    private IEnumerator FlickerOffTimedActions()
    {
        ThisLight.enabled = false;
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = false;
    }

    // A function that is called to turn the light object off
    private void LightFlickerOn()
    {
        light_on = true;
        StartCoroutine(FlickerOnTimedActions());
    }
    // A time-based, blocking function that turns the light object of the lamp on, off, then on to emulate "flickering" on
    private IEnumerator FlickerOnTimedActions()
    {
        ThisLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = false;
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = true;
    }
}
