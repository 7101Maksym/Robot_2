using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Main_Portal : MonoBehaviour
{
    [SerializeField] private string[] _scenes;
    [SerializeField] private string s;
	[SerializeField] private GameObject _portalEffect;

	private VideoPlayer _video;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private AsyncOperation _operation;

	public bool IsActive1 = false, IsActive2 = false;
	private bool _active = false, _canLoad = true;

	private void Awake()
	{
		_animator = GetComponentInChildren<Animator>();
		_spriteRenderer = _portalEffect.GetComponent<SpriteRenderer>();
		_video = GetComponentInChildren<VideoPlayer>();

		_video.targetCamera = Camera.main;
    }

    private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (_active && _canLoad)
			{
				_video.enabled = true;
				_video.Play();

				_canLoad = false;

                _operation = SceneManager.LoadSceneAsync(_scenes[UnityEngine.Random.Range(0, _scenes.Length - 1)]);

				_operation.allowSceneActivation = false;

				StartCoroutine(ActivateScene());
            }
		}
	}

	private void Update()
	{
		if (IsActive1 && IsActive2)
		{
			_animator.SetBool("Active", true);

			StartCoroutine(Activation());
		}
		else if (IsActive1 || IsActive2)
		{
			_animator.SetBool("PartlyActive", true);
		}

		if (_spriteRenderer.color.a >= 0.9f)
		{
			_active = true;
		}
	}

	private IEnumerator Activation()
	{
		yield return new WaitForSeconds(8);

		_spriteRenderer.color += new Color(0, 0, 0, 0.001f);
	}

	private IEnumerator ActivateScene()
	{
		yield return new WaitForSeconds(5);

		_operation.allowSceneActivation = true;
	}
}
