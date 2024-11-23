using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RandomRoulette : MonoBehaviour
{
    [SerializeField] private float _spinningTime = 10f;
    [SerializeField] private float _spinningSpeed = 10f;
    [SerializeField] private string[] _lotteryMembersList;
    [SerializeField] private Image _rouletteImage;
    [SerializeField] private Text _membersListText;
    [SerializeField] private Text _winnerNameText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Button _startButton;
    private IEnumerator _spinRoutine;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartSpin);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartSpin);

        StopCoroutine(_spinRoutine);
    }

    private void Start()
    {
        AddMembersToTextList();

        _spinRoutine = Spin();

        _timeText.text = string.Format("TIME: {0:F2}", _spinningTime);
    }

    private void StartSpin()
    {
        _winnerNameText.text = "";

        StartCoroutine(_spinRoutine);
    }

    private void AddMembersToTextList()
    {
        if (_lotteryMembersList.Length > 0)
        {
            _membersListText.text = "";

            for (var i = 0; i < _lotteryMembersList.Length; i++)
            {
                if (i < _lotteryMembersList.Length - 1)
                {
                    _membersListText.text += _lotteryMembersList[i] + "\n";
                }
                else
                {
                    _membersListText.text += _lotteryMembersList[i];
                }
            }
        }
    }

    private IEnumerator Spin()
    {
        float time = _spinningTime;

        while (time > 0)
        {
            _rouletteImage.fillAmount += _spinningSpeed * Time.deltaTime;

            time -= Time.deltaTime;
            _timeText.text = string.Format("TIME: {0:F2}", time);

            if (_rouletteImage.fillAmount >= 1)
            {
                _rouletteImage.fillAmount = 0;
            }

            _winnerNameText.text = _lotteryMembersList[Random.Range(0, _lotteryMembersList.Length)];

            yield return null;
        }
    }
}
