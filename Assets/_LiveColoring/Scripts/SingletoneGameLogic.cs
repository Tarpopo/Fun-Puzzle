using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ColoringProject
{
    public class SingletoneGameLogic : MonoBehaviour
    {

        string _mainMenuName = "MainMenu";
        public static SingletoneGameLogic Instance = null;

        [SerializeField] private SpriteCollections _currentCollection;

        public SpriteCollections CurrentCollection => _currentCollection;

        [SerializeField] private int _topicId = 0;
        [SerializeField] private int _imageId = 0;

        public SpriteCollection CurrentSpriteCollection = null;
        public AnimationCollection CurrentAnimationCollection = null;

        [SerializeField] private Transform generateIntoСarousel = null;
        [SerializeField] private GameObject prefabForGeneration = null;
        [SerializeField] private CardMover cardMover = null;
        private bool _firstLaunch = true;
        #region Music and sounds

        [SerializeField] private float soundVolime;

        public float SoundVolime
        {
            get => soundVolime;
            set
            {
                soundVolime = value;

            }
        }

        [SerializeField] private float musicVolime = 1;

        public float MusicVolime
        {
            get => musicVolime;
            set
            {
                musicVolime = value;
                musicSource.volume = musicVolime;
            }
        }

        [SerializeField] AudioSource musicSource;
        [SerializeField] private List<AudioClip> musicList = new List<AudioClip>();
        private AudioClip currentClip = null;

        private void SelectMusic()
        {
            List<AudioClip> selectMusicList = new List<AudioClip>(musicList);
            if (currentClip != null) selectMusicList.Remove(currentClip);
            currentClip = selectMusicList[Random.Range(0, selectMusicList.Count)];
            musicSource.clip = currentClip;
            musicSource.Play();
        }


        #endregion

        private void OnEnable()
        {
            ReturnToMenu();
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this.gameObject);
        }

        private void Awake()
        {
            musicSource = GetComponent<AudioSource>();
            CheckInstances();
        }

        private void CheckInstances()
        {
            if (Instance == null) Instance = this;
            if (Instance != this) Destroy(gameObject);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            CheckInstances();
            IniciateScene(scene);
        }

        private void IniciateScene(Scene scene)
        {
            if (scene.name != _mainMenuName) return;
            _firstLaunch = true;
            CategoryBtns categoryBtns = FindObjectOfType<CategoryBtns>();
            if (categoryBtns != null) categoryBtns.ClickOnButton(_topicId);

            if (FindObjectOfType<CardMover>() != null) cardMover = FindObjectOfType<CardMover>();
            generateIntoСarousel = FindObjectOfType<CardMover>().transform;
            SelectMusic();
            SetupImagesIntoCarousel(_topicId);

        }

        [Button]
        public void SetupImagesIntoCarousel(int topic)
        {
            if (_topicId == topic && !_firstLaunch) return;
            _topicId = topic;
            foreach (Transform child in generateIntoСarousel)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < CurrentCollection.SpriteCollectionTopics[topic].SpriteCollectionsList.Count; i++)
            {
                var sprites = CurrentCollection.SpriteCollectionTopics[topic].SpriteCollectionsList[i];
                GameObject parentPrefab = Instantiate(prefabForGeneration, generateIntoСarousel);
                Button button = parentPrefab.AddComponent<Button>();

                if (!SaveAndLoadBehaviour.Instance.TopicsStatus[_topicId].LevelsStatus[i].AnimsStatus.Contains(SaveStatus.Opened)) button.interactable = false;


                ColorBlock colorBlock = button.colors;
                colorBlock.pressedColor = Color.white;
                colorBlock.selectedColor = Color.white;
                button.colors = colorBlock;

                int notModifyLevel = i;
                button.onClick.AddListener(() => LoadCollection(topic, notModifyLevel));
                parentPrefab.GetComponent<Image>().raycastTarget = true;
                GameObject imageGenerate;

                imageGenerate = Instantiate(prefabForGeneration, parentPrefab.transform); //Silhouette
                imageGenerate.GetComponent<Image>().sprite = sprites.Silhouette;
                DecreaseImage(imageGenerate);

                foreach (var sprite in sprites.SpriteList) //ImageLayers settup
                {
                    imageGenerate = Instantiate(prefabForGeneration, parentPrefab.transform);
                    imageGenerate.GetComponent<Image>().sprite = sprite;
                    DecreaseImage(imageGenerate);
                }

                imageGenerate = Instantiate(prefabForGeneration, parentPrefab.transform);
                imageGenerate.GetComponent<Image>().sprite = sprites.OutlineSprite;

                DecreaseImage(imageGenerate);
            }

            StartCoroutine(WaitFrameAndUpdate());
        }

        IEnumerator WaitFrameAndUpdate()
        {
            yield return new WaitForEndOfFrame();
            cardMover.UpdateCards();
            _firstLaunch = false;
        }

        /// <summary>
        /// Выравнить изображение
        /// </summary>
        /// <param name="imageGenerate"></param>
        private static void DecreaseImage(GameObject imageGenerate)
        {
            RectTransform rectTransform = imageGenerate.GetComponent<RectTransform>();
            Vector2 sizeDelta = rectTransform.sizeDelta;
            rectTransform.sizeDelta = new Vector2(sizeDelta.x - 40, sizeDelta.y - 40);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.localPosition = new Vector2(0.5f, 0.5f);
        }

        public List<SaveStatus> GetCurrentAnimationStatuses()
        {
            return SaveAndLoadBehaviour.Instance.TopicsStatus[_topicId].LevelsStatus[_imageId].AnimsStatus;
        }

        #region LevelSwitch

        [Button]
        public void LoadCollection(int topicId, int imageId)
        {
            CurrentSpriteCollection = CurrentCollection.SpriteCollectionTopics[topicId].SpriteCollectionsList[imageId];
            CurrentAnimationCollection = CurrentCollection.SpriteCollectionTopics[topicId].AnimationCollectionsList[imageId];
            _imageId = imageId;
            Application.LoadLevel("ColoringScene");
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(_mainMenuName);
        }

        public void LoadNextCharacter()
        {
            if (CurrentCollection.SpriteCollectionTopics[_topicId].SpriteCollectionsList.Count <= _imageId + 1)
            {
                if (CurrentCollection.SpriteCollectionTopics.Count <= _topicId + 1) _topicId = 0;
                else _topicId++;
                _imageId = 0;
            }
            else _imageId++;
            LoadCollection(_topicId, _imageId);
        }

        #endregion

    }
}
