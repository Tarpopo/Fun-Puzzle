using UnityEngine;

namespace Assets.Scripts.MainMenu
{
   public class MoveOutMenu : MonoBehaviour
   {
      [SerializeField] private RectTransform[] _buttons;
      [SerializeField] private Vector2[] _endPositions;
      [SerializeField]private int _frames = 15;
      
      private Vector2[] _startPositions;
      private bool _isShow;

      private void Start()
      {
         _startPositions=new Vector2[_buttons.Length];
         for (int i = 0; i < _buttons.Length; i++)
         {
            _startPositions[i] = _buttons[i].anchoredPosition;
         }
      }

      public void ShowHideMenu()
      {
         StopAllCoroutines();
         var frames = _frames;
         var toPosition = _isShow ? _startPositions : _endPositions;
         _isShow = !_isShow;
         for (int i = 0; i < _buttons.Length; i++)
         {
            StartCoroutine(CorroutinesKit.ChangePosition(_buttons[i], toPosition[i], frames++));
         }
      }

   }
}
