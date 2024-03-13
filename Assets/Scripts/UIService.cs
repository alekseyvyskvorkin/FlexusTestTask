using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIService : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _forceText;

    [Inject] private LaunchSettings _lauchSettings;
    [Inject] private TrajectoryHandler _trajectoryHandler;

    private void Start()
    {
        _slider.maxValue = _lauchSettings.LauchForce;
        _slider.minValue = 0;
        _slider.value = _lauchSettings.LauchForce;
        _slider.onValueChanged.AddListener(ChangeForce);
        ChangeForce(_lauchSettings.LauchForce);
    }

    private void ChangeForce(float force)
    {
        _forceText.text = ((int)_slider.value).ToString();
        _lauchSettings.ChangeForce(force);
        _trajectoryHandler.DrawTrajectory();
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }
}

