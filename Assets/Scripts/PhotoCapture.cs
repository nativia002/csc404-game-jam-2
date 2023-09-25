using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [SerializeField] private GameObject _endGameUI;

    [Header("Photo Taker")]
    [SerializeField] private Image _photoDisplayArea;
    [SerializeField] private GameObject _photoFrame;
    [SerializeField] private GameObject _cameraUI;

    [Header("Flash Effect")]
    [SerializeField] private GameObject _cameraFlash;
    [SerializeField] private float _flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator _fadingAnimation;

    [Header("Camera Audio")]
    [SerializeField] private AudioSource _cameraAudio;


    private Texture2D _screenCapture;
    private bool _viewingPhoto;

    private int _numPhotos = 0;
    public List<Texture2D> photos = new List<Texture2D>();
    public bool isGameOver = false;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver)
        {
            if (!_viewingPhoto)
            {
                // Take Screenshot
                _cameraUI.SetActive(false);
                StartCoroutine(CapturePhoto());
                
            } else
            {
                RemovePhoto();
                if (_numPhotos == 5)
                {
                    _cameraUI.SetActive(false);
                    _endGameUI.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    isGameOver = true;
                }
            }
        }

    }

    IEnumerator CapturePhoto()
    {
        _viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        photos.Add(new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false));
        _screenCapture = photos[photos.Count -1];
        _screenCapture.ReadPixels(regionToRead, 0, 0, false);
        _screenCapture.Apply();

        _numPhotos += 1;
        ShowPhoto();
    }

    IEnumerator CameraFlashEffect()
    {
        _cameraAudio.Play();
        _cameraFlash.SetActive(true);
        yield return new WaitForSeconds(_flashTime);
        _cameraFlash.SetActive(false);

    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(_screenCapture, new Rect(0.0f, 0.0f, _screenCapture.width, _screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        _photoDisplayArea.sprite = photoSprite;

        _photoFrame.SetActive(true);
        StartCoroutine(CameraFlashEffect());
        _fadingAnimation.Play("PhotoFade");
    }


    void RemovePhoto()
    {
        _viewingPhoto = false;
        _photoFrame.SetActive(false);
        _cameraUI.SetActive(true);
    }
}
