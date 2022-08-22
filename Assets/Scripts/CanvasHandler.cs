using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField]
    private Text heightText, widthText, sliderHeightGameText, sliderWidthGameText;
    [SerializeField]
    private Slider sliderHeight, sliderWidth, sliderGameHeight, sliderGameWidth;    
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject gameMenu, bigMenu, menuPanel;
   
    
    void Start()
    {        
        heightText.text = sliderHeight.value.ToString();
        widthText.text = sliderWidth.value.ToString();
        gameManager.height = (int)sliderHeight.value;
        gameManager.width = (int)sliderWidth.value;
    }
   
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

    public void ToggleMenu()
    {
        if(menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(true);            
        }
    }
    public void ToggleBigMenu()
    {
        gameManager.ReturnMenu();
        gameMenu.SetActive(false);
        menuPanel.SetActive(false);       
        bigMenu.SetActive(true);
        sliderHeight.value = gameManager.height;
        sliderWidth.value = gameManager.width;
        widthText.text = sliderWidth.value.ToString();
        heightText.text = sliderHeight.value.ToString();
    }

    public void SliderValues()
    {
        sliderGameWidth.value = gameManager.width;
        sliderGameHeight.value = gameManager.height;
        sliderWidthGameText.text = sliderGameWidth.value.ToString();
        sliderHeightGameText.text = sliderGameHeight.value.ToString();
    }
    
}
