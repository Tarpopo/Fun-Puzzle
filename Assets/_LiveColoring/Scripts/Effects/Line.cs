using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _line;

    private List<Vector3> _positions;

    public void SetPositions(List<Vector3> positions)
    {
        _positions = positions;
    }

    public void Hide()
    {
        StartCoroutine(HideLineAnim());
    }

    public void ShowLine()
    {
        Vector3[] positions = _positions.ToArray();

        _line.positionCount = positions.Length;
        _line.SetPositions(positions);

        _line.Simplify(0.8f);
    }

    private IEnumerator ShowLineAnim()
    {
        Vector3[] positions = _positions.ToArray();

        for (int i = 0; i < _positions.Count; i++)
        {
            _line.positionCount = i + 1;
            _line.SetPositions(positions);

            yield return null;
        }
    }

    private IEnumerator HideLineAnim()
    {
        while(_line.positionCount > 1)
        {
            _line.positionCount--;

            yield return new WaitForSeconds(0.01f);
        }

        _line.enabled = false;
    }

    public List<Vector3> GetPoints()
    {
        return _positions;
    }

    public void SetColor(Color c)
    {
        _line.startColor = _line.endColor = c;
    }
}
