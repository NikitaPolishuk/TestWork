using UnityEngine;
using UnityEngine.UI;

public class ChangeControlPanelView : BaseView
{ 
    [SerializeField]
    private Button _tachPad;
    [SerializeField]
    private Button _joyStick;

    private void Start()
    {
        _tachPad.onClick.AddListener(() =>
        {
           _uiManager.�hangingControlMethods(false);
        });

        _joyStick.onClick.AddListener(() =>
        {
            _uiManager.�hangingControlMethods(true);
        });
    }
}
