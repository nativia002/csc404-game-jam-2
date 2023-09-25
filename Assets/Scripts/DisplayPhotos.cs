using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPhotos : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Photo Album")]
    
    [SerializeField] private List<Image> _photoDisplays = new List<Image>(5);
    [SerializeField] private PhotoCapture photoCapture;
    private Texture2D _screenCapture;
    private Image _photoDisplay;
    void Start()
    {
        for (int i=0; i<5; i++)
        {
            _screenCapture = photoCapture.photos[i];
            _photoDisplay = _photoDisplays[i];
            Sprite photoSprite = Sprite.Create(_screenCapture, new Rect(0.0f, 0.0f, _screenCapture.width, _screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
            _photoDisplay.sprite = photoSprite;
        }

        
    }

}
