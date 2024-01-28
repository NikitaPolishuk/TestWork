
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectionView : BaseView
{
    [SerializeField]
    private Button _build1;
    [SerializeField]
    private Button _build2;
    [SerializeField]
    private Button _build3;
    [SerializeField]
    private Button _build4;

    private void Start()
    {
        var gameManager = GameManager.instance;
        _build1.onClick.AddListener(() => gameManager.ChooseBuilding(0));
        _build2.onClick.AddListener(() => gameManager.ChooseBuilding(1));
        _build3.onClick.AddListener(() => gameManager.ChooseBuilding(2));
        _build4.onClick.AddListener(() => gameManager.ChooseBuilding(3));
    }
}
