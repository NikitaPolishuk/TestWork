using UnityEngine;
using UnityEngine.UI;

public class TouchPadView : BaseView
{
    [SerializeField]
    private Image _outline;

    public void ChangeColorOutline(Color color)
    { 
        _outline.color = color; 
    }
}
