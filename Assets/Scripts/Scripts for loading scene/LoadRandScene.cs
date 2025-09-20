using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadRandScene : MonoBehaviour
{
    [SerializeField] private string[] _scenes;
    [SerializeField] private VideoPlayer _video;

    private void Start()
    {
        _video.Play();

        SceneManager.LoadSceneAsync(_scenes[UnityEngine.Random.Range(0, _scenes.Length - 1)]);
    }
}
