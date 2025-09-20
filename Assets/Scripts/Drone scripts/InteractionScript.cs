using System.Reflection;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
	[SerializeField] private NoteRenderer _noteRenderer;

	private MonoBehaviour _interactionComponent = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		foreach (var component in collision.transform.gameObject.GetComponentsInParent<MonoBehaviour>())
		{
			var method = component.GetType().GetMethod("Action", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			
			if (method != null)
			{
				_interactionComponent = component;
				_noteRenderer._object = component.gameObject;
				break;
			}
		}
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var component in collision.transform.gameObject.GetComponentsInParent<MonoBehaviour>())
        {
            var method = component.GetType().GetMethod("Action", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (method != null)
            {
                _noteRenderer._object = null;
                break;
            }
        }
    }

    public void Interact()
	{
		if (_interactionComponent != null)
		{
			_interactionComponent.Invoke("Action", 0f);
		}
	}
}
