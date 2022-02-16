using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutLinesCreator : MonoBehaviour
{
    // [SerializeField]
    // private RectTransform _targetImage;
    //
    // [SerializeField]
    // private GameObject _linePref;
    //
    // [SerializeField]
    // private PuzzlesCreator _puzzlesCreator;
    //
    // private List<Line> _lines;
    //
    // public void CreateLines()
    // {
    //     return;
    //
    //     var positions = _puzzlesCreator.GetCutPoints();
    //
    //     Vector3 offset = new Vector3(_targetImage.sizeDelta.x / 2, _targetImage.sizeDelta.y / 2, 0);
    //
    //     _lines = new List<Line>(positions.Count);
    //
    //     for (int i = 0; i < positions.Count; i++)
    //     {
    //         for (int k = 0; k < positions[i].Count; k++)
    //         {
    //             positions[i][k] -= offset;
    //         }
    //
    //         CreateLine(positions[i]);
    //     }
    // }
    //
    // private void CreateLine(List<Vector3> positions)
    // {
    //     GameObject lineGo = Instantiate(_linePref, _targetImage);
    //
    //     Line line = lineGo.GetComponent<Line>();
    //
    //     line.SetPositions(positions);
    //
    //     _lines.Add(line);
    // }
    //
    // public void HideLines()
    // {
    //     if (_lines == null)
    //         return;
    //
    //     for (int i = 0; i < _lines.Count; i++)
    //     {
    //         _lines[i].Hide();
    //     }
    // }
}
