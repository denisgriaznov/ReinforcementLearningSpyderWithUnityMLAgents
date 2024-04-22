using UnityEngine;
using System.Collections;

public class BasicAnimator : MonoBehaviour
{
    public bool enabledByDefault = false;
    public float limitMin = 0;
    public float limitMax = 0;
    public float animSpeed = 0;

    public bool enabledRotation = true;

    public GameObject animChildOnlyMoving = null;

    void Awake()
    {
        animEnabled = enabledByDefault;
        if(enabledByDefault)
        {
            limits[0] = limitMin;
            limits[1] = limitMax;
            eAnimSpeedY = animSpeed;
            duration = Random.Range(0.75f, 1.25f);
        }
    }
    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    private float t = 0;
    private float duration = 1;

    private bool animEnabled = false;
    public bool AnimEnabled
    {
        get { return animEnabled; }
        set { animEnabled = value; }
    }

    public void setLimits(float min, float max = 1.0f)
    {
        limits[0] = min;
        limits[1] = max;
    }

    private Vector3 start = new Vector3(0, 0, 0);
    private bool inited = false;
    private int sign = 1;
    private float eAnimSpeedY = 1.25f;
    private float[] limits = { 0.35f, 1.0f };

    private const float eRotationSpeed = 30;
    
    void Update()
    {
        if (!animEnabled)
            return;

        if (!inited)
        {
            inited = true;
            start = transform.localPosition;
        }
        t += Time.deltaTime * eAnimSpeedY;
        if (t < duration)
        {
            float dt = t;
            if (sign < 0)
                dt = duration - t;
            float y = Mathf.SmoothStep(limits[0], limits[1], dt);
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
            if(animChildOnlyMoving != null)
            {
                animChildOnlyMoving.transform.localPosition = new Vector3(animChildOnlyMoving.transform.localPosition.x, y, animChildOnlyMoving.transform.localPosition.z);
            }
        }
        else
        {
            t = 0;
            sign *= -1;
        }
        if(enabledRotation)
            transform.Rotate(0, eRotationSpeed * Time.deltaTime, 0);
    }


}
