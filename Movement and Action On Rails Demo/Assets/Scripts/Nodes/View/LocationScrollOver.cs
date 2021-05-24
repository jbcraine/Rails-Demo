using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScrollOver : MonoBehaviour
{
    private ParticleSystem locationParticles;
    private Light locationLight;
    public float maximumIntensity;
    public float minimumIntensity;
    //How quickly should the locationLight intensity be raised or lowered?
    public float intensityPerSecond = .25f;
    private float currentIntensity;
    private bool active;

    //The amount of time to reach the maximum or minimum intensity
    [SerializeField]
    private float timeToLight;
    private IEnumerator brighten_c;
    private IEnumerator dimmer_c;

    void Awake()
    {
        locationParticles = GetComponentInChildren<ParticleSystem>();
        locationLight = GetComponentInChildren<Light>();
        brighten_c = ChangeLightIntensity(true);
        dimmer_c = ChangeLightIntensity(false);
    }

    private void Start() {
        currentIntensity = locationLight.intensity = minimumIntensity;
        locationParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void CursorOverLocationEnter()
    {

        //StopCoroutine(dimmer_c);
        StopAllCoroutines();
        //StartCoroutine(brighten_c);
        StartCoroutine(ChangeLightIntensity(true));
        locationParticles.Play();
    }

    public void CursorOverLocationExit()
    {
        //StopCoroutine(brighten_c);
        StopAllCoroutines();
        //StartCoroutine(dimmer_c);
        StartCoroutine(ChangeLightIntensity(false));
        locationParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    private void OnMouseEnter() {
        CursorOverLocationEnter();
    }

    private void OnMouseExit() {
        CursorOverLocationExit();
    }

    //Accepts a bool. True to turn the locationLights up, false to turn them down.
    IEnumerator ChangeLightIntensity(bool state)
    {
        if (state)
        {
            while (currentIntensity < maximumIntensity)
            {
                currentIntensity += intensityPerSecond;
                locationLight.intensity = currentIntensity;
                Debug.Log("Brighten");
                yield return new WaitForSeconds(.1f);
            }
            if (currentIntensity >= maximumIntensity)
            {
                locationLight.intensity = currentIntensity = maximumIntensity;
                yield return null;
            }
        }
        else
        {
            while (currentIntensity > minimumIntensity)
            {
                currentIntensity -= intensityPerSecond;
                locationLight.intensity = currentIntensity;
                Debug.Log("Dimmer");
                yield return new WaitForSeconds(.1f);
            }
            if (currentIntensity <= minimumIntensity)
            {
                locationLight.intensity = currentIntensity = minimumIntensity;
                yield return null;
            }
        }
    }
}
