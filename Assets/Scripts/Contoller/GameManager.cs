using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private UiManager _uiManager;
    [SerializeField]
    private JoyStick _joyStick;
    [SerializeField]
    private CameraController _cameraController;
    [SerializeField]
    private TachPadController _tachPadController;
    [SerializeField]
    private CameraController _cameraContoller;
    [SerializeField]
    private BuildsData _buildsData;

    private void Awake()
    {
        if(instance == null) instance = this;
        else if(instance == this) Destroy(gameObject);
    }

    private void Start()
    {
        _tachPadController.TachActive += OnTachActive;
        _joyStick.JoyStickMove += OnJoyStickMove;
    }

    private void OnJoyStickMove(Vector2 move)
    {
        _cameraController.RotationCameraByDelta(move);
    }

    private void OnTachActive(TachPadState state)
    {
        TouchPadView tachPadView = _uiManager.GetView<TouchPadView>();
        if (tachPadView != null)
        {
            switch (state)
            {
                case TachPadState.Red:
                    tachPadView.ChangeColorOutline(Color.red);
                    break;
                case TachPadState.Green:
                    tachPadView.ChangeColorOutline(Color.green);
                    break;
                case TachPadState.Gray:
                    tachPadView.ChangeColorOutline(Color.gray);
                    break;
            }
        }
    }

    public void ChooseBuilding(int buildId)
    {
        var build = _buildsData.GetBuildWithId(buildId);
        if (build == null)
        {
            Debug.LogError("Couldn get access to the building");
        }
        else
        {
            _cameraContoller.SmoothZoom(build.transform);
        }
    }
}
