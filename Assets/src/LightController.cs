using UnityEngine;
using System.Collections;
using Jokie;

public class LightController : MonoBehaviour 
{
    public Light light1;
    public Light light2;

    private float _light1First;
    private float _light2First;

    private bool _flashing;
    private Timer _flash;

	// Use this for initialization
	void Start () 
    {
        _light1First = light1.intensity;
        _light2First = light2.intensity;
	}

    public void Flash()
    {
        if (!_flashing)
        {
            light1.intensity *= 16;
            light2.intensity *= 16;
            _flashing = true;
            _flash = new Timer(1.0f);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (_flashing)
        {
            if (_flash.Check())
            {
                _flashing = false;
                light1.intensity = _light1First;
                light2.intensity = _light2First;
            }
        }
	}
}
