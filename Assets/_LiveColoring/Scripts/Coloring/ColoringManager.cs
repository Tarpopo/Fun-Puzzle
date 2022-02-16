using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace ColoringProject
{
    public class ColorStruct
    {
        public List<Image> Sprites = new List<Image>(); //раскрашиваемая часть
        public Color Color;
        public ColorsEnum ColorsEnum;
        public Button Button;
    }

    public class ColoringManager : MonoBehaviour
    {
        public static ColoringManager Instance;
        #region Префабы и объекты для генерации

        [Required] [SerializeField] private SpriteCollection tCollection;

        [Required] [SerializeField] private Button buttonPrefab;
        [Required] [SerializeField] private Image imagePrefab;

        [Required] [SerializeField] private Transform imageGenerateGameObject;
        [Required] [SerializeField] private Transform backgroybdGenerateGameObject;
        [Required] [SerializeField] private Transform buttonGenerateGameObject;

        #endregion

        #region WinEffects



        [SerializeField] [Required] private ParticleSystem winPartile;

        [SerializeField] [Required] private ParticleSystem colorPartile;
        #endregion

        private List<ColorStruct> _colorStructList = new List<ColorStruct>();
        [ShowInInspector] private List<Button> _unpressedColorsButtonsList = new List<Button>();


        #region Подготовка

        private void Awake()
        {
            if(Instance!=null) Debug.LogError("2 SingleTones");
            Instance = this;
        }

        private void Start()
        {
            if (SingletoneGameLogic.Instance == null)
                Debug.LogError("Нет главного синглтона, запускать со сцены Menu через метод LoadCollection в SingletoneGameLogic");
            else
                tCollection = SingletoneGameLogic.Instance.CurrentSpriteCollection;

            if (tCollection == null) Debug.LogError("Изображение не найдено");

            if (fingerShow == null) fingerShow = FindObjectOfType<FingerShow>();
            SetupImages();
            StartCoroutine(WaitForHint());
            // StartCoroutine(WinImageAppear(false));
            WhiteScreenAppear.Instance.AppearScreen(false);
        }

        [Button]
        public void SetupImages()
        {
            if (tCollection.SpriteList.Count != tCollection.ColorsList.Count) Debug.LogError("Цвета и изображения выставлены не верно");

            SetupSprites();

            SetupOutline();

            SetupBackgroundSprites();

            SetupBackGroundOutline();

            foreach (ColorStruct colorStruct in _colorStructList) _unpressedColorsButtonsList.Add(colorStruct.Button);

            SetupHint();
        }

        private void SetupSprites()
        {
            Image silhouette = Instantiate(imagePrefab, imageGenerateGameObject);
            silhouette.sprite = tCollection.Silhouette;
            silhouette.fillAmount = 1;
            silhouette.color = new Color(0.91f, 0.91f, 0.91f);
            for (int index = 0; index < tCollection.SpriteList.Count; index++)
            {
                Image image = Instantiate(imagePrefab, imageGenerateGameObject);
                image.sprite = tCollection.SpriteList[index];
                image.enabled = false;
                image.fillAmount = 0;

                Button button = Instantiate(buttonPrefab, buttonGenerateGameObject);
                Color color = tCollection.VisualColorsList[index];
                color = new Color(color.r, color.g, color.b,1);


                SetColorOnButton(button, color);



                ColorStruct colorStruct = new ColorStruct();
                colorStruct.Sprites.Add(image);
                colorStruct.Color = color;
                colorStruct.ColorsEnum = tCollection.ColorsList[index];
                colorStruct.Button = button;

                button.onClick.AddListener(() => ButtonPressed(colorStruct));

                _colorStructList.Add(colorStruct);
            }
        }

        private void SetupBackgroundSprites()
        {
            for (int index = 0; index < tCollection.BackGroundSprite.Count; index++)
            {
                Image image = Instantiate(imagePrefab, backgroybdGenerateGameObject);
                image.sprite = tCollection.BackGroundSprite[index];
                image.enabled = false;
                image.fillAmount = 0;
                Color color = tCollection.VisualBackgroundColors[index];
                color =  new Color(color.r,color.g,color.b,1);
                Button button = null;
                bool hasColor = false; 
                foreach (ColorStruct existedColors in _colorStructList)
                { 
                    if (tCollection.BackgroundColors[index] == existedColors.ColorsEnum)
                    { 
                        button = existedColors.Button;
                        existedColors.Sprites.Add(image);
                        hasColor = true;
                    }
                }

                if (!hasColor) button = Instantiate(buttonPrefab, buttonGenerateGameObject);


                ColorStruct colorStruct = new ColorStruct();
                colorStruct.Sprites.Add(image);
                colorStruct.Color = color;
                colorStruct.ColorsEnum = tCollection.ColorsList[index];
                colorStruct.Button = button;
                if (!hasColor)
                {
                    _colorStructList.Add(colorStruct);
                    SetColorOnButton(button, color);
                } 
                button.onClick.AddListener(() => { ButtonPressed(colorStruct); });
            }
        }

        private void SetupOutline()
        {
            Image image = Instantiate(imagePrefab, imageGenerateGameObject);
            image.sprite = tCollection.OutlineSprite;
            image.fillAmount = 1;
        }

        private void SetupBackGroundOutline()
        {
            Image image = Instantiate(imagePrefab, backgroybdGenerateGameObject);
            image.sprite = tCollection.BackGroundOutlineSprite;
            image.fillAmount = 1;
            image.color = new Color(0.91f, 0.91f, 0.91f);
        }

        #endregion

        #region Нажатие кнопки с цветом

        public bool IsColoring;

        private void ButtonPressed(ColorStruct colorStruct)
        {
            if (IsColoring)
                return; //todo звук отмены
            _unpressedColorsButtonsList.Remove(colorStruct.Button);
            SetupHint();
            colorStruct.Button.interactable = false;
            foreach (var image in colorStruct.Sprites)
            {
                StartCoroutine(ColoringAnimation(image,colorStruct));
                image.enabled = true;
                
            }
            Destroy(colorStruct.Button.gameObject);
            //SetColorOnButton(colorStruct.Button, Color.gray);
            if (_unpressedColorsButtonsList.Count == 0)
            {
                Destroy(colorStruct.Button.transform.parent.parent.gameObject);
                Invoke(nameof(WinLevel), 3);
            }
        }

        private IEnumerator ColoringAnimation(Image image, ColorStruct colorStruct)
        {
            colorPartile.Play();
            ParticleSystem.MainModule psMain = colorPartile.main;
            psMain.startColor = new ParticleSystem.MinMaxGradient(colorStruct.Color);
            image.enabled = true;
            float time = 0;
            float maxTime = 1;
            while (time < maxTime)
            {
                image.fillAmount = time / maxTime;
                time += Time.deltaTime / 2;
                yield return new WaitForEndOfFrame();
                IsColoring = true;
            }
            colorPartile.Stop();
            image.fillAmount = 1;
            IsColoring = false;
        }

        #endregion

        #region ColorButton

        private static void SetColorOnButton(Button button, Color color)
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = color;
            colorBlock.selectedColor = color;
            button.colors = colorBlock;
        }

        #endregion

        #region Hint

        private float _hintTimer;
        private bool _firstHint;
        private const float FirstHintTimer = 6;
        private const float SecondHintTimer = 9;
        private Button _hintPickedButton;
        [SerializeField] private FingerShow fingerShow;

        private IEnumerator WaitForHint()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                _hintTimer++;
                if (!_firstHint)
                {
                    if (_hintTimer >= FirstHintTimer)
                    {
                        if (_hintPickedButton == null) break;
                        _hintTimer = 0;
                        _firstHint = true;
                        ColorHint(_hintPickedButton);
                    }
                }
                else if (_hintTimer >= SecondHintTimer)
                {
                    if (_hintPickedButton == null) break;
                    _hintTimer = 0;
                    _firstHint = false;
                    FingerClickHint(_hintPickedButton);
                }
            }
        }

        private void SetupHint()
        {
            if (_unpressedColorsButtonsList.Count == 0)
            {
                _hintPickedButton = null;
                return;
            }

            _hintTimer = 0;
            _hintPickedButton = _unpressedColorsButtonsList[Random.Range(0, _unpressedColorsButtonsList.Count)];
            _firstHint = false;
        }

        private void ColorHint(Button hintPickedButton)
        {
            if (_blinkColorCoroutine != null) StopCoroutine(_blinkColorCoroutine);
            _blinkColorCoroutine = StartCoroutine(BlinkColor(hintPickedButton));
        }


        private Coroutine _blinkColorCoroutine;

        private IEnumerator BlinkColor(Button button)
        {
            const float maxColorTime = 3;
            float partTime = maxColorTime / 4;


            Color defaultButtonColor = button.colors.normalColor;
            Color hintColor = Color.white;
            bool colored = false;
            int blinkTimes = 0;


            while (blinkTimes < 4)
            {
                yield return new WaitForEndOfFrame();
                partTime -= Time.deltaTime;
                if (partTime <= 0)
                {
                    if (colored)
                    {
                        SetColorOnButton(button, defaultButtonColor);
                        colored = false;
                    }
                    else
                    {
                        if (hintColor == defaultButtonColor) SetColorOnButton(button, Color.gray);
                        else
                            SetColorOnButton(button, hintColor);
                        colored = true;
                    }

                    partTime = maxColorTime / 4;
                    blinkTimes++;
                }
            }

            _blinkColorCoroutine = null;
        }


        private void FingerClickHint(Button clickButton)
        {
            fingerShow.ClickTo(clickButton.GetComponent<RectTransform>());
        }

        #endregion




        private void WinLevel()
        {
            WhiteScreenAppear.Instance.AppearScreen(true);
           // StartCoroutine(WinImageAppear(true));
            winPartile.Play();
            winPartile.GetComponent<OneSoundPlay>().Play();

            Debug.Log("ColoringEnded");
            Invoke(nameof(NextLevel), 3);
        }

        private void NextLevel()
        {
            WhiteScreenAppear.Instance.AppearScreen(false);
            //StartCoroutine(WinImageAppear(false));
            winPartile.Stop();
            AnimationManager.Instance.gameObject.SetActive(true); 
            gameObject.SetActive(false);
            //SingletoneGameLogic.Instance.ReturnToMenu();
        }
    }
}