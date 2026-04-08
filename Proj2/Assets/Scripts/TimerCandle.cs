using UnityEngine;

public class TimerCandle : MonoBehaviour
{
    public ParticleSystem flame;
    public Light candleLight;

    public bool IsLit{ get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LightCandle();
    }

    public void LightCandle()
    {
        IsLit = true;

        if (flame != null)
        {
            flame.Play();
        }

        if (candleLight != null)
        {
            candleLight.enabled = true;
        }
    }

    public void ExtinguishCandle()
    {
        IsLit = false;

        if (flame != null)
        {
            flame.Stop();
        }

        if (candleLight != null)
        {
            candleLight.enabled = false;
        }
    }
}
