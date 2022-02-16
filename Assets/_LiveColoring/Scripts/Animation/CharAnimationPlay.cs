using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColoringProject
{
    public class CharAnimationPlay : MonoBehaviour
    {
        public enum MoveDirection
        {
            idle,
            left,
            right
        }
        private Animator anim;
        public AnimationManager manager;

        private string _currentAnimPlaying;

        public string CurrentAnimPlaying
        {
            get => _currentAnimPlaying;
            set
            {
                if (_currentAnimPlaying == value) return;
                _currentAnimPlaying = value;
                anim.Play(_currentAnimPlaying);
            }
        }

        private bool CustomAnimPlaying;

        private Vector3 _scale;

        [SerializeField] private MoveDirection moveDir = MoveDirection.idle;

        private Camera _camera;

        public MoveDirection MoveDir
        {
            get => moveDir;
            set => moveDir = value;
        }

        const float CharSpeed = 40;


        [SerializeField] private bool reverseDirection = false;
        float directionModifire = 1;
        [SerializeField] private bool instantWalkSpeed = false;
        private Vector3 startSize;
        private float animationWalkSpeed = 0;

        public float AnimationWalkSpeed
        {
            get => animationWalkSpeed;
            set
            {
                if (Math.Abs(value - animationWalkSpeed) > 0.05f && !instantWalkSpeed)
                {
                    anim.SetFloat("Speed", animationWalkSpeed);
                    if (animationWalkSpeed > value) animationWalkSpeed -= Time.deltaTime * 3;
                    else animationWalkSpeed += Time.deltaTime * 3;
                }
                else
                {
                    anim.SetFloat("Speed", value);
                    animationWalkSpeed = value;
                }
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            AnimationWalkSpeed = 0;
            anim.SetFloat("Speed", 0);
            _scale = transform.localScale;
            if (reverseDirection) directionModifire = -1;
            _camera = Camera.main;

            if(reverseDirection) moveDir = MoveDirection.right;
            else moveDir = MoveDirection.left;
            ChooseAnimForMovement();
            AnimationWalkSpeed = 0;
        }

        private void Update()
        {
            Movement();
            ChooseAnimForMovement();
        }

        #region Movement

        public float dontMoveTime = 0;
        private void ChooseAnimForMovement()
        {
            if (CustomAnimPlaying) return;
            switch (moveDir)
            {
                case MoveDirection.idle:
                    AnimationWalkSpeed = 0;
                    if (instantWalkSpeed) dontMoveTime = 0.7f;
                    break;
                case MoveDirection.left:
                    if (dontMoveTime > 0) dontMoveTime -= Time.deltaTime;
                    else
                        transform.position += Vector3.left * (Time.deltaTime * CharSpeed);
                    transform.localScale = new Vector3(_scale.x * directionModifire, _scale.y);
                    AnimationWalkSpeed = 1;
                    break;
                case MoveDirection.right:
                    if (dontMoveTime > 0) dontMoveTime -= Time.deltaTime;
                    else
                        transform.position += Vector3.right * (Time.deltaTime * CharSpeed);
                    transform.localScale = new Vector3(-_scale.x * directionModifire, _scale.y);
                    AnimationWalkSpeed = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Movement()
        {
            MoveDir = MoveDirection.idle;
            if (!manager.playerClickArea.Downed) return;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                float characterXPosition = transform.position.x;
                Vector3 currentMousePosition = MouseWorldPosition();
                float mouseXPositon = currentMousePosition.x;

                if (Math.Abs(characterXPosition - mouseXPositon) > 10)
                {
                    if (characterXPosition < mouseXPositon)
                        MoveDir = MoveDirection.right;
                    else
                        MoveDir = MoveDirection.left;
                }
            }
        }

        private Vector3 MouseWorldPosition()
        {
            Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100); // z - distance of the plane from the camera 
            return _camera.ScreenToWorldPoint(screenPoint);
        }

        #endregion


        #region AnimationPlay

        [Button]
        public void AnimPlay(string animName)
        {
            if (CustomAnimPlaying == false)
            {
                CustomAnimPlaying = true;
                CurrentAnimPlaying = animName;
                StopCoroutine(nameof(OnCompleteAnimation));
                StartCoroutine(nameof(OnCompleteAnimation));
            }
        }

        IEnumerator OnCompleteAnimation()
        {
            yield return new WaitForEndOfFrame();
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }

            CurrentAnimPlaying = "Idle";
            CustomAnimPlaying = false;
        }

        #endregion
    }
}