using UnityEngine;
using UnityEngine.UI;

public class CrosshairBehavior : MonoBehaviour
{
    public Image reticleImage;
    public Color targetColor;
    public float animationSpeed = 5;
    public float detectRange = 20;
    public LayerMask ignoreLayer;
    Color originalReticleColor;
    Vector3 originalReticleScale;
    Color currentReticleColor;
    Vector3 shrunkSize;
    void Start()
    {
        originalReticleColor = reticleImage.color;
        originalReticleScale = reticleImage.transform.localScale;
        currentReticleColor = originalReticleColor;
        shrunkSize = originalReticleScale / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(!reticleImage)
            return;
        InteractiveEffect();
    }

    void InteractiveEffect()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, detectRange, ~ignoreLayer))
        {
            Debug.Log("Hit Something " + hit.collider.name);
            if(hit.collider.CompareTag("GrapplePoint"))
            {
                currentReticleColor = targetColor;
                ReticleAnimation(shrunkSize, currentReticleColor, animationSpeed);
            }
        }
        else
        {
            currentReticleColor = originalReticleColor;
            ReticleAnimation(originalReticleScale, currentReticleColor, animationSpeed);
        }
    }

    void ReticleAnimation(Vector3 targetScale, Color targetColor, float speed)
    {
        var step = speed * Time.deltaTime;
        reticleImage.color = Color.Lerp(reticleImage.color, targetColor, step);
                reticleImage.transform.localScale = 
                    Vector3.Lerp(reticleImage.transform.localScale, targetScale, step);
    }
}
