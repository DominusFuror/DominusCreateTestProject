using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
  
    [SerializeField] TextMeshProUGUI circleRadius;
    [SerializeField] TextMeshProUGUI CircleX;
    [SerializeField] TextMeshProUGUI CircleY;
    [SerializeField] TextMeshProUGUI CircleZ;
    [SerializeField] TextMeshProUGUI CircleCount;

    [SerializeField] Slider circleRadiusSlider;
    [SerializeField] Slider CircleXSlider;
    [SerializeField] Slider CircleYSlider;
    [SerializeField] Slider CircleZSlider;
    [SerializeField] Slider circleCountSlider;

    [SerializeField] Toggle rotateTogle;


    Setts setts;

    [SerializeField]
    CirclePhotoBlockMovementManager circlePhotoBlockMovementManager;


    void UpdateText()
    {
        circleRadius.text = "Circle radius : " + setts.circleRadius;
        CircleX.text = "Circle X Multi : " + setts.circleAxeX;
        CircleY.text = "Circle Y Multi : " + setts.circleAxeY;
        CircleZ.text = "Circle Z Multi : " + setts.circleAxeZ;    
        CircleCount.text = "Circle image count : " + setts.imageCount;

     



    }
    public void OnValueChange(bool toggle)
    {


        setts.isRotate = rotateTogle.isOn;
        circlePhotoBlockMovementManager.SettingsUpdate(false, setts);
  

  

    }

    void GetSliderUpdate(float value)
    {
      
         setts.circleRadius = circleRadiusSlider.value;
         setts.circleAxeX = CircleXSlider.value;
         setts.circleAxeY = CircleYSlider.value;
         setts.circleAxeZ = CircleZSlider.value;      
       
         circlePhotoBlockMovementManager.SettingsUpdate(false, setts);
         UpdateText();
    }
    void SetSliderStartPos()
    {
      
        circleRadiusSlider.value = setts.circleRadius;
        CircleXSlider.value = setts.circleAxeX;
        CircleYSlider.value = setts.circleAxeY;
        CircleZSlider.value = setts.circleAxeZ;
        circleCountSlider.value = setts.imageCount;


        circleRadiusSlider.onValueChanged.AddListener(GetSliderUpdate);
        CircleXSlider.onValueChanged.AddListener(GetSliderUpdate);
        CircleYSlider.onValueChanged.AddListener(GetSliderUpdate);
        CircleZSlider.onValueChanged.AddListener(GetSliderUpdate);
        circleCountSlider.onValueChanged.AddListener(OnCountValueChange);

    }
    public void OnCountValueChange(float value)
    {
        setts.imageCount = (int)circleCountSlider.value;
        circlePhotoBlockMovementManager.SettingsUpdate(true, setts);
        UpdateText();
  
    }


    private void Awake()
    {
        setts = new Setts();
        LoadSettings();
        SetSliderStartPos();
        circlePhotoBlockMovementManager.SettingsUpdate(true, setts);
    }
    private void Start()
    {
   
    
    }
    private void OnApplicationQuit()
    {
        SaveSetting();
    }
    void SaveSetting()
    {

        try
        {
          
            string settingsJson = JsonConvert.SerializeObject(setts);
            File.WriteAllText("settings.txt", settingsJson);
        }
        catch (System.Exception)
        {

            throw;
        }
    }


    void LoadSettings()
    {
        try
        {
       
            string settingJSON = File.ReadAllText("settings.txt");
            setts = JsonConvert.DeserializeObject<Setts>(settingJSON);
            print("SD");

        }
        catch (System.Exception)
        {

            throw;
        }
      

    }

    [SerializeField]
    Animator menuAnimator;

   public static bool isSettingsBlockHidden = true;
    public void OnMenuButtonClick()
    {
        isSettingsBlockHidden = !isSettingsBlockHidden;
        menuAnimator.SetBool("IsHide", !menuAnimator.GetBool("IsHide"));
    }


  
}
public struct Setts
{
    public float circleRadius;
    public float circleAxeX ;
    public float circleAxeY ;
    public float circleAxeZ ;
    public int imageCount ;
    public bool isRotate;

  

}