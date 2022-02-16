using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class CardMover : MonoBehaviour
{
    private List<Transform> _freeCards = new List<Transform>();
    private List<Transform> _occupiedCards = new List<Transform>();
    private List<Transform> _templatePositions = new List<Transform>(3);
    private IntTimer _timer;
    private int _nonTouchTime;
    private Pulsator _pulsator;
    [SerializeField]private Transform _transform;
    
    [SerializeField] private Transform _template;
    [SerializeField] private InputPlayer _inputPlayer;
    [SerializeField] private int _movingSpeed;
    [SerializeField] private int _scalingSpeed;
    [SerializeField] private float _sideCardScale;
    [SerializeField] private float _sideCardsRotationZ;

    private void Start()
    {
        _pulsator = GetComponent<Pulsator>();
        _nonTouchTime = _movingSpeed > _scalingSpeed ? _movingSpeed + 1 : _scalingSpeed + 1;
        if (TryGetComponent(out _timer) == false)
        {
            _timer = gameObject.AddComponent<IntTimer>();
        }

        foreach (var template in _template)
        {
            _templatePositions.Add((Transform) template);
        }

        //UpdateCards();
    }


    [Button]
    public void UpdateCards()
    {
        _freeCards.Clear();
        _occupiedCards.Clear();

        foreach (var card in transform)
        {
            var obj = (Transform) card;
            obj.gameObject.SetActive(false);
            obj.localScale = Vector2.zero;
            _freeCards.Add(obj);
        }

        var count = _freeCards.Count - _templatePositions.Count <= 0 ? _freeCards.Count : _templatePositions.Count;

        _occupiedCards = _freeCards.GetRange(0, count);
        _freeCards.RemoveRange(0, count);
        for (var i = 0; i < _templatePositions.Count; i++)
        {
            if (_occupiedCards.Count == 1)
            {
                _occupiedCards[0].transform.position = Vector2.zero;
                _occupiedCards[0].gameObject.SetActive(true);
                continue;
            }

            if (i >= count)
            {
                _occupiedCards.Add(_transform);
                continue;
            }

            _occupiedCards[i].transform.position = _templatePositions[i].position;
            _occupiedCards[i].transform.localScale = _sideCardScale * Vector2.one;
            _occupiedCards[i].gameObject.SetActive(true);
        }

        // if (_occupiedCards.Count > 1)
        // {
        //     var cardTransform = _occupiedCards[1];
        //     _occupiedCards[1].position = _templatePositions[0].position;
        //     _occupiedCards[0].position = _templatePositions[1].position;
        //     _occupiedCards.RemoveAt(1);
        //     _occupiedCards.Insert(0, cardTransform);
        // }
        var movingSpeed = _movingSpeed;
        var scalingSpeed = _scalingSpeed;
        _movingSpeed = 1;
        _scalingSpeed = 1;
        MoveCards(1);
        _movingSpeed = movingSpeed;
        _scalingSpeed = scalingSpeed;
        
        // _occupiedCards[_occupiedCards.GetMidIndex()].localScale = Vector2.one;
        // _pulsator.StartPulse(_occupiedCards[_occupiedCards.GetMidIndex()]);
        // if (_occupiedCards.Count == 1) return;
        // RotateSideCards(); 
    }
    
    public void MoveCards(int direction)
    {
        if (_occupiedCards.Count == 1 || _timer.IsWork()) return;
        _timer.StartTimer(_nonTouchTime);
        
        var midIndex = _occupiedCards.GetMidIndex();
        if (_freeCards.Count == 0)
        {
            if (_occupiedCards[midIndex] != _transform)
            {
                StartCoroutine(CorroutinesKit.ChangeScale(_occupiedCards[midIndex],
                    Vector2.one * _sideCardScale, _scalingSpeed));
            }

            _occupiedCards.CycleStep(direction);

            midIndex = _occupiedCards.GetMidIndex();
            if (_occupiedCards[midIndex] != _transform)
            {
                _occupiedCards[midIndex].SetSiblingIndex(_occupiedCards.GetEdgeIndex(1));
                SetZeroRotation(_occupiedCards[midIndex]);
                StartCoroutine(CorroutinesKit.ChangeScale(_occupiedCards[midIndex],
                    Vector2.one, _scalingSpeed));
            }
            RotateSideCards();
            
            for (var i = 0; i < _occupiedCards.Count; i++)
            {
                if (_occupiedCards[i] == _transform) continue;

                if (_occupiedCards.GetEdgeIndex(-direction) == i)
                {
                    StartCoroutine(CorroutinesKit.PlayDoubleScale(_occupiedCards[i], Vector2.zero,
                        Vector2.one * _sideCardScale, _templatePositions[i].position, _scalingSpeed));
                    continue;
                }

                StartCoroutine(CorroutinesKit.ChangePosition(_occupiedCards[i], _templatePositions[i].position,
                    _movingSpeed));
            }
            _pulsator.StartPulse(_occupiedCards[midIndex]);
            return;
        }
        
        
        StartCoroutine(CorroutinesKit.ChangeScale(_occupiedCards[midIndex], 
            Vector2.one * _sideCardScale, _scalingSpeed));
        
        var card = _occupiedCards[_templatePositions.GetEdgeIndex(direction)];
        _occupiedCards.Remove(card);
        StartCoroutine(CorroutinesKit.ChangeScale(card, Vector2.zero, _scalingSpeed, true));

        _occupiedCards.Insert(_templatePositions.GetEdgeIndex(-direction), _freeCards[0]);
        _freeCards[0].gameObject.SetActive(true);
        _freeCards[0].position = _templatePositions[_templatePositions.GetEdgeIndex(-direction)].position;
        StartCoroutine(CorroutinesKit.ChangeScale(_freeCards[0], Vector2.one*_sideCardScale, _scalingSpeed));
        _freeCards.RemoveAt(0);
        _freeCards.Add(card);
        
        midIndex = _occupiedCards.GetMidIndex();
        _occupiedCards[_occupiedCards.GetEdgeIndex(-direction)].SetSiblingIndex(0);
        _occupiedCards[midIndex].SetSiblingIndex(_occupiedCards.GetEdgeIndex(1));
        SetZeroRotation(_occupiedCards[midIndex]);
        StartCoroutine(CorroutinesKit.ChangeScale(_occupiedCards[midIndex],
            Vector2.one, _scalingSpeed));
        RotateSideCards();
        
        for (var i = 0; i < _occupiedCards.Count; i++)
        {
            StartCoroutine(CorroutinesKit.ChangePosition(_occupiedCards[i], _templatePositions[i].position,
                _movingSpeed));
        }
        _pulsator.StartPulse(_occupiedCards[midIndex]);
        _timer.StartTimer(_nonTouchTime);
    }

    private void SetZeroRotation(Transform card)
    {
        card.rotation=quaternion.identity;
    }

    private void RotateSideCards()
    {
        ChangeRotationZ(_occupiedCards[_occupiedCards.GetEdgeIndex(1)],-1);
        ChangeRotationZ(_occupiedCards[_occupiedCards.GetEdgeIndex(-1)],1);
    }

    private void ChangeRotationZ(Transform card, int direction)
    {
        if (card == _transform) return;
        card.rotation = quaternion.Euler(0, 0, _sideCardsRotationZ*direction);
    }



}
