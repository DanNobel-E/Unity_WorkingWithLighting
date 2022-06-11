using UnityEngine;
using System.Collections;

public enum LerpLoopState {FORWARD, BACKWARD}

public class LerpLoop : MonoBehaviour
{
	public Transform startT, endT;
	public float duration = 1;
    public LerpLoopState state;
    private float currTime = 0;
    public bool pos, rot, scale;
    public lerpType type;
    private float fraction = 0; 

	void Start(){
		state = LerpLoopState.FORWARD;
	}

	void Update()
	{
        if (!pos && !rot && !scale) return;

		if (currTime > duration) {
			state = LerpLoopState.BACKWARD;
		}
		else if(currTime < 0){
			state = LerpLoopState.FORWARD;
		}

		if(state == LerpLoopState.FORWARD)
			currTime += Time.deltaTime;
		else
			currTime -= Time.deltaTime;

        fraction = customLerp.getFraction (currTime, duration, type);
        if (pos)
            transform.localPosition = Vector3.Lerp(startT.position, endT.position, fraction);
            //transform.localPosition = Vector3.Lerp(startT.position, endT.position, currTime);
        if (rot)
            transform.localRotation = Quaternion.Lerp(startT.rotation, endT.rotation, fraction);
            //transform.localRotation = Quaternion.Lerp(startT.rotation, endT.rotation, currTime);
        if (scale)
            transform.localScale = Vector3.Lerp(startT.localScale, endT.localScale, fraction);
            //transform.localScale = Vector3.Lerp(startT.localScale, endT.localScale, currTime);
    }
}
