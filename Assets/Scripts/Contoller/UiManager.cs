using System;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private List<BaseView> _viewArr = new List<BaseView>();
    private Dictionary<Type, BaseView> _viewsReferences = new Dictionary<Type, BaseView>();

    private void Start()
    {
        RegisterViews();
    }

    private void RegisterViews()
    {
        foreach (var view in _viewArr)
        {
            var viewType = view.GetType();
            if (_viewsReferences.ContainsKey(viewType))
            {
                Debug.LogError($"View with type {viewType} already present in container");
                continue;
            }

            view.SetViewController(this);
            _viewsReferences.Add(viewType, view);
        }
    }

    public T GetView<T>() where T : Component
    {
        var viewType = typeof(T);
        if (!_viewsReferences.ContainsKey(viewType))
        {
            Debug.Log($"View of type {typeof(T)} is null");
            return default;
        }
        return _viewsReferences[viewType] as T;
    }

    public void ÑhangingControlMethods(bool joyStickON)
    {
        TouchPadView tachPadView = GetView<TouchPadView>();
        JoyStickView joyStickView = GetView<JoyStickView>();

        if(tachPadView != null && joyStickView != null && joyStickON)
        {
            joyStickView.Show();
            tachPadView.Hide();
        }
        else
        {
            joyStickView.Hide();
            tachPadView.Show();
        }
    }
}
