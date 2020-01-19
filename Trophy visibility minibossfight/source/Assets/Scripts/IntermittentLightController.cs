using UnityEngine;

public class IntermittentLightController : MonoBehaviour
{
    public GameObject spotLight;
    public GameObject halo;
    public Light spotLightComponent;
    public Light haloComponent;
    public float lightIntensityChangeRateo;
    public float minSpotLightIntensity;
    public float maxSpotLightIntensity;
    public float minHaloIntensity;
    public float maxHaloIntensity;
    private bool on = true;

    private void Start()
    {
        spotLightComponent = spotLight.GetComponent<Light>();
        haloComponent = halo.GetComponent<Light>();
    }

    private void Update()
    {
        if (on)
        {
            spotLightComponent.intensity = (Mathf.Cos(Time.time * Mathf.PI * lightIntensityChangeRateo) + 1) * (maxSpotLightIntensity - minSpotLightIntensity) / 2 + minSpotLightIntensity;
            haloComponent.intensity = (Mathf.Cos(Time.time * Mathf.PI * lightIntensityChangeRateo) + 1) * (maxHaloIntensity - minHaloIntensity) / 2 + minHaloIntensity;
        }
    }

    public void TurnOff()
    {
        on = false;
        spotLight.SetActive(false);
        halo.SetActive(false);
    }

    public void TurnOn()
    {
        on = true;
        spotLight.SetActive(true);
        halo.SetActive(true);
    }
}
