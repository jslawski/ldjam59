using UnityEngine;
using DG.Tweening;

public class SideBanger : InteractableObject
{
    [SerializeField]
    private Transform _tvTransform;

    [SerializeField]
    private Vector3 _bangDirection;

    [SerializeField]
    private float _pushMagnitude;

    private Vector3 _initialPosition;

    public int index;
    
    [SerializeField]
    private GameObject _hitParticlePrefab;

    private void Awake()
    {
        this._initialPosition = this._tvTransform.position;
    }

    public override void Interact()
    {
        Instantiate(this._hitParticlePrefab, this.interactContactPoint, new Quaternion());    

        this.Push();

        PlayerControlsManager.instance.targetObject = null;

        AudioChannelSettings audioChannelSettings = new AudioChannelSettings(false, 0.8f, 1.2f, 0.3f, "SFX");
        AudioClip audioClip = Resources.Load<AudioClip>("Audio/TVSmackingSound");

        AudioManager.instance.Play(audioClip, audioChannelSettings);

        TVSignalTuner.instance.UpdateStatic(this.index);

        if (this.onInteracted != null)
        {
            this.onInteracted(this);
        }
    }

    private void Push()
    {
        Vector3 finalPushPosition = this._initialPosition + (this._bangDirection * this._pushMagnitude);
        
        Sequence pushSequence = DOTween.Sequence();

        Tweener pushAwayTween = this._tvTransform.DOMove(finalPushPosition, 0.1f);
        Tweener returnToPositionTween = this._tvTransform.DOMove(this._initialPosition, 0.1f);
        Tweener shakeTween = this._tvTransform.DOShakeRotation(0.1f, 30.0f, 10, 60.0f, true, ShakeRandomnessMode.Harmonic);
        Tweener returnRotation = this._tvTransform.DORotate(Vector3.zero, 0.1f);

        pushSequence.Append(pushAwayTween).Join(shakeTween).Append(returnToPositionTween).Join(returnRotation);
    }
}
