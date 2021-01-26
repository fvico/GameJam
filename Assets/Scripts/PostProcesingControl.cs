using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcesingControl : MonoBehaviour
{  [SerializeField]
    Volume _volume;
    Vignette _vignette;
    DepthOfField _depthOfField;
    ChromaticAberration _chromaticAberration;
    LiftGammaGain _liftGammaGain;
    ColorAdjustments _colorAdjustments;

    private void Start()
    {
        _volume.GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
        _volume.profile.TryGet(out _depthOfField);
        _volume.profile.TryGet(out _chromaticAberration);
        _volume.profile.TryGet(out _liftGammaGain);
        _volume.profile.TryGet(out _colorAdjustments);

    }
    public void ActivateVolumen()
    {
        _vignette.active = true;
        _depthOfField.active = true;
        _chromaticAberration.active = true;
        _liftGammaGain.active = true;
        _colorAdjustments.active = true;
    }
    public void DeactivateVolumen()
    {
        _vignette.active = false;
        _depthOfField.active = false;
        _chromaticAberration.active = false;
        _liftGammaGain.active = false;
        _colorAdjustments.active = false;
    }
}
