using UnityEngine;

public class BaseView : MonoBehaviour
{
    protected UiManager _uiManager;

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetViewController(UiManager controller)
    {
        _uiManager = controller;
    }
}
