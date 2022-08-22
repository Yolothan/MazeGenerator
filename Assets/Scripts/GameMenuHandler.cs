using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuHandler : MonoBehaviour
{
    [SerializeField]
    private Text heightText, widthText;
    [SerializeField]
    private Slider sliderHeight, sliderWidth;
    [SerializeField]
    private GameManager gameManager;    
    // Start is called before the first frame update
   
    public void OnValueChangedSliderHeight()
    {
        heightText.text = sliderHeight.value.ToString();
        gameManager.height = (int)sliderHeight.value;
    }
    public void OnValueChangedSliderWidth()
    {
        widthText.text = sliderWidth.value.ToString();
        gameManager.width = (int)sliderWidth.value;
    }
}
