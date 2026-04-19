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
    

    private void Awake()
    {
        this._initialPosition = this._tvTransform.position;
    }

    public override void Interact()
    {
        Debug.LogError("Player SMACKED " + this.gameObject.name);

        this.Push();

        PlayerControlsManager.instance.targetObject = null;
    }


    private void Push()
    {
        Vector3 finalPushPosition = this._initialPosition + (this._bangDirection * this._pushMagnitude);
        
        Sequence pushSequence = DOTween.Sequence();

        Tweener pushAwayTween = this._tvTransform.DOMove(finalPushPosition, 0.1f);
        Tweener returnToPositionTween = this._tvTransform.DOMove(this._initialPosition, 0.1f);
        Tweener shakeTween = this._tvTransform.DOShakeRotation(0.2f, 30.0f, 10, 60.0f, true, ShakeRandomnessMode.Harmonic);
        
        pushSequence.Append(pushAwayTween).Join(shakeTween).Append(returnToPositionTween);
    }
}
