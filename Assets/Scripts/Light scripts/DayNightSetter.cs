using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightSetter : MonoBehaviour
{
	[SerializeField] private float _cycleTime = 30f;
	[SerializeField] private Gradient _gradient = new Gradient();
	[SerializeField] private AnimationCurve _curve;

	[Range(0, 100)]
    [SerializeField] private float _nowTime = 0;

	private float _nowPosition = 0;

	private Light2D _light;

	private void Awake()
	{
		_light = GetComponent<Light2D>();

		_nowTime = _nowTime * _cycleTime / 100;
    }

	private void Update()
	{
		_nowTime += Time.deltaTime;

		_nowPosition = _nowTime / _cycleTime;

		_light.color = _gradient.Evaluate(_nowPosition);
		_light.intensity = _curve.Evaluate(_nowPosition);

		if (_nowTime > _cycleTime)
		{
			_nowTime = 0;
		}
	}
}
