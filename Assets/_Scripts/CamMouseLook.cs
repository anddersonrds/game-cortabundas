using UnityEngine.Playables;
using UnityEngine;

public class CamMouseLook : MonoBehaviour
{
    public PlayableDirector director;
    Vector2 mouseLook, SmoothV;

    public float sensitivity = 5.0f, smoothing = 2.0f;

    GameObject player;
	
	void Start ()
    {
        player = this.transform.parent.gameObject;        
	}	
	
	void Update ()
    {
        if (director.state == PlayState.Playing)
        {
            return;
        }
        else
        {
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            SmoothV.x = Mathf.Lerp(SmoothV.x, md.x, 1f / smoothing);
            SmoothV.y = Mathf.Lerp(SmoothV.y, md.y, 1f / smoothing);

            mouseLook += SmoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -70f, 90f);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

            player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
        }

    }
}
