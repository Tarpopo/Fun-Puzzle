using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewAnimationEffect : MonoBehaviour
{
    [SerializeField]
    private Image _whiteScreen, _blackScreen;

    [SerializeField]
    private GameObject _origBackground, _effectBackground, _ball, _halfBalls, _effect, _effect2, _effect3, _rotatingRays, _scaleBar;

    [SerializeField]
    private Transform _leftBall, _rightBall;

    // [SerializeField]
    // private CharacterCreator _characterCreator;
    //
    // [SerializeField]
    // private GameStateChanger _gameState;
    //
    // [SerializeField]
    // TutorForBall _tutorForBall;

    [SerializeField]
    TapParticlesEffect particlesEffect;

    private bool musicIsPaused;
    private bool BallIsClickable = true;
    public bool IsBlocked
    {
        get; set;
    }

    public event System.Action OnLootboxShowed;
    public event System.Action OnAdCharacterSpawned;

    public void Show()
    {
        StartCoroutine(ShowAnim());
    }

    private void Start()
    {
        Show();
    }

    private IEnumerator ShowAnim()
    {
        _scaleBar.gameObject.SetActive(false);
    
        // if (MusicPlayer.MusicIsEnabled)
        // {
        //     GameObject.FindGameObjectWithTag("GlobalBehaviour")?.GetComponent<MusicPlayer>()?.PauseTemp();
        //     MusicPlayer.MusicWasPausedAuto = true;
        // }
        
        _whiteScreen.gameObject.SetActive(true);
    
        Color alpha = Color.white;
        alpha.a = 0;
    
        _whiteScreen.color = alpha;
    
        yield return StartCoroutine(CoroutinesUtil.ImageToColorAnim(_whiteScreen, Color.white, 20));
    
        //_origBackground.SetActive(false);
        _effectBackground.SetActive(true);
    
        _ball.SetActive(true);
        _rotatingRays.SetActive(true);
    
        yield return StartCoroutine(CoroutinesUtil.ImageToColorAnim(_whiteScreen, alpha, 20));
    
        _whiteScreen.gameObject.SetActive(false);
        BallIsClickable = true;
        //_tutorForBall.Initialize();
        particlesEffect.Activate();
    }



    public void UnlockLootBox()
    {
        IsBlocked = false;
    }

    public void ClickBall()
    {
        if (!BallIsClickable) return;
        //_tutorForBall.Hide();
        _ball.GetComponent<Shaking>().Shake();
        BallIsClickable = false;
        _scaleBar.gameObject.SetActive(true);
        _scaleBar.GetComponent<TimeScaleAnimator>().StartAnimation(5);
        OnLootboxShowed?.Invoke();
        StartCoroutine(waitForUnblock());
    }

    private IEnumerator waitForUnblock()
    {
        var sbar = _scaleBar.GetComponent<TimeScaleAnimator>();
        while (IsBlocked || sbar.IsAnimating())
        {
            yield return null;
        }
        StartCoroutine(Explosive());
        _scaleBar.gameObject.SetActive(false);
    }

    public void OnStartAd()
    {
        StopAllCoroutines();
        _scaleBar.gameObject.SetActive(false);
    }

    public void OnAdEnded()
    {
        StartCoroutine(Explosive());
    }

    private IEnumerator Explosive()
    {
        yield return new WaitForSeconds(1.5f);
    
        _whiteScreen.gameObject.SetActive(true);
    
        //_tutorForBall.Hide();
    
        _effect.SetActive(true);
        _effect2.SetActive(true);
    
        StartCoroutine(CoroutinesUtil.ScalerAnim(_ball.transform, Vector3.zero, 30));
    
        yield return StartCoroutine(CoroutinesUtil.ImageToColorAnim(_whiteScreen, Color.white, 30));
    
        Transform currentCharacter;
        bool IsAdCharacter=false;
    
        // if (GlobalData.CanCreatePremiunAd())
        // {
        //     currentCharacter = createAdCharacter();
        //     IsAdCharacter = true;
        //     OnAdCharacterSpawned?.Invoke();
        // }
        // else
        // {
        //     currentCharacter = createCharacter();
        //     IsAdCharacter = false;
        // }
        
        
    
        // Vector3 origCharScale = currentCharacter.localScale;
        //
        // currentCharacter.localScale *= 0.7f;
    
        _ball.SetActive(false);
    
        _halfBalls.SetActive(true);
        //SoundManager.Instance.Play("ltbopen");
    
        Vector3 leftPos = _leftBall.localPosition - new Vector3(50, 0, 0);
        Vector3 rightPos = _rightBall.localPosition + new Vector3(50, 0, 0);
    
        StartCoroutine(CoroutinesUtil.MoveLocal(_leftBall, leftPos, 30, null));
        StartCoroutine(CoroutinesUtil.MoveLocal(_rightBall, rightPos, 30, null));
    
        _effect3.SetActive(true);
        //SoundManager.Instance.Play("ltblight", 0.1f);
    
        Color target = Color.white;
        target.a = 0;
    
        yield return StartCoroutine(CoroutinesUtil.ImageToColorAnim(_whiteScreen, target, 30));
    
        yield return new WaitForSeconds(0.3f);
        // SoundManager.Instance.Play("ltbreward", 0.3f);
        // GlobalData.OpenAnimation(GlobalData.CurrentCharacterName);
        //
        // if (IsAdCharacter)
        // {
        //     PlayRandomAnim(currentCharacter.GetComponent<Character>());
        // }
        // else
        // {
        //     PlayNextAnim(currentCharacter.GetComponent<Character>());
        //     
        // }
        //
        //
        // var character = currentCharacter.GetComponent<Character>();
    
        yield return null;
    
        // while (!character.CanPlayAnim())
        //     yield return null;
    
        yield return new WaitForSeconds(1f);
    
        // if (MusicPlayer.MusicWasPausedAuto)
        // {
        //     MusicPlayer.MusicWasPausedAuto = false;
        //     GameObject.FindGameObjectWithTag("GlobalBehaviour")?.GetComponent<MusicPlayer>()?.UnpauseTemp();
        // }
    
        particlesEffect.Deactivate();
    
        _blackScreen.gameObject.SetActive(true);
    
        _blackScreen.color = Color.clear;
    
        yield return StartCoroutine(CoroutinesUtil.ImageToColorAnim(_blackScreen, Color.black, 30));
    
        //Destroy(currentCharacter.gameObject);
    
        //_gameState.PlayStageAnimation(false);
    
        //_origBackground.SetActive(true);
        _halfBalls.SetActive(false);
        //_effectBackground.SetActive(false);
        _whiteScreen.gameObject.SetActive(false);
        _rotatingRays.SetActive(false);
        _effect3.SetActive(false);
    
        //_characterCreator.GetCharacter().localScale = origCharScale;
    
        yield return StartCoroutine(CoroutinesUtil.ImageToColorAnim(_blackScreen, Color.clear, 30));
    
        _blackScreen.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
       
    }

    // private void PlayNextAnim(Character character)
    // {
    //     character.PlaySpecialAnimation(GlobalData.GetOpenedAnimations(GlobalData.CurrentCharacterName));
    // }
    //
    // private void PlayRandomAnim(Character character)
    // {
    //     character.PlayRandomAnimation();
    // }
    //
    // private Transform createCharacter()
    // {
    //     return _characterCreator.SpawnCharacter();
    // }
    //
    // private Transform createAdCharacter()
    // {
    //     return _characterCreator.SpawnRandomAdCharacter();
    // }
}
