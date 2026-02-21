using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TravelSelectionUI : PhaseUI
{
    [SerializeField] private List<Button> _areas;
    [SerializeField] private List<Image> _images;

    [SerializeField] private TextMeshProUGUI _timer;

    private float _time;
    private bool _isTimeOver;
    private int _clickedIdx;
    private bool _isBtnClicked = false;

    private void Awake()
    {
        for(int i = 0; i < _areas.Count; i++)
        {
            
            int index = i;
            _areas[i].onClick.AddListener(() => GameManager.Instance.ClickArea(index));
            
            _areas[i].onClick.AddListener(() => 
            {

                _clickedIdx = index;

                Debug.Log($"clickedIdx: {_clickedIdx}");

                if (!_isBtnClicked)
                {
                    for(int k = 0; k < _images.Count; k++)
                    {
                        if (k == index)
                            continue;

                        //다른 버튼들 비 활성화 및 색상 어둡게
                        _images[k].color = Color.grey;
                        _images[k].GetComponentInParent<Button>().interactable = false;
                    }
                }
            });

        }
    }

    public void OnEnable()
    {
        _time = GameManager.Instance.Config.DefaultTravelSelectionTime;
        _timer.text = Mathf.CeilToInt(_time).ToString();
        _isTimeOver = false;
        GameManager.Instance.ClickArea(_areas.Count-1);

        for(int i = 0; i < _images.Count; i++)
        {
            _images[i].color = Color.white;
            _images[i].GetComponentInParent<Button>().interactable = true;
        }
    }

    private void Update()
    {
        if (_isTimeOver) return;

        if (_time < 0f)
        {
            _isTimeOver = true;
            GameManager.Instance.AsyncPhase();
        }

        _time -= Time.deltaTime;
        _timer.text = Mathf.CeilToInt(_time).ToString();
    }


}