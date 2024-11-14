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
        ThisLight = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light_on)
        {
            if (Vector3.Distance(this.gameObject.transform.position, AntagonistTransform.position) <= LIGHTOFFRADIUS)
            {
                LightFlickerOff();
            }
        }
        else
        {
            if ((Time.realtimeSinceStartup - gametime_at_off) > OFFTIME)
            {
                if (Vector3.Distance(this.gameObject.transform.position, AntagonistTransform.position) > LIGHTOFFRADIUS)
                {
                    LightFlickerOn();
                }
            }
        }
    }

    private void LightFlickerOff()
    {
        light_on = false;
        gametime_at_off = Time.realtimeSinceStartup;
        StartCoroutine(FlickerOffTimedActions());
    }

    private IEnumerator FlickerOffTimedActions()
    {
        ThisLight.enabled = false;
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = false;
    }

    private void LightFlickerOn()
    {
        light_on = true;
        StartCoroutine(FlickerOnTimedActions());
    }

    private IEnumerator FlickerOnTimedActions()
    {
        ThisLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = false;
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = true;
    }
}
