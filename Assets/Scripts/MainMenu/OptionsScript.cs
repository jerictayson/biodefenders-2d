using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OptionsScript : MonoBehaviour
{
    private Resolution[] _resolutions;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMPro.TMP_Dropdown _dropdownMenu;
    // Start is called before the first frame update
    void Start()
    {

        _resolutions = Screen.resolutions;
        _dropdownMenu.ClearOptions();

        var currentResolutionIndex = 0;
        var list = new List<string>();
        //loop over the resolutions and add them to the dropdownmenu
        for (var i = 0; i < _resolutions.Length; i++)
        {
            list.Add($"{_resolutions[i].width} x {_resolutions[i].height}");
            if(_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        _dropdownMenu.AddOptions(list);
        _dropdownMenu.value = currentResolutionIndex;
        _dropdownMenu.RefreshShownValue();

    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        _audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        
    }
    
    public void SetResolution(int resolutionIndex)
    {
        var resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
