using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public int jitter;

    void Update()
    {

        /*Vector3 p = Input.mousePosition; //fully functioning, just feels janky
        p.z = 20;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);


        float vert = (pos.y - player.position.y);
        float horiz = (pos.x - player.position.x);

        if (Mathf.Abs(vert) > 3)
        {
            vert = 3 * (vert / Mathf.Abs(vert));
        }
        if (Mathf.Abs(horiz) > 3)
        {
            horiz = 3 * (horiz / Mathf.Abs(horiz));
        }

        horiz = horiz / 2;
        vert = vert / 2;

        if (Vector3.Distance(new Vector3(this.transform.position.x + horiz, this.transform.position.y + vert, -10), new Vector3(player.transform.position.x, player.transform.position.y, -10)) > 4.9)
        {
            if (Vector2.Distance(this.transform.position, player.transform.position) > 3)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), 10 * Time.deltaTime);
            }
        }
        else if (Vector2.Distance(this.transform.position, player.transform.position) < 3)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x + horiz, this.transform.position.y + vert, -10), 10 * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), 10 * Time.deltaTime);
        }*/

        if (this.gameObject.GetComponent<Camera>() != null)
        {

            if (jitter > 0)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);


                Vector2 toVector = destination - new Vector2(player.position.x, player.position.y);
                float angleToTarget = Vector2.Angle(transform.right, toVector);

                if (destination.y - player.position.y < 0)
                {
                    angleToTarget = 360 - angleToTarget;
                }
                
                angleToTarget = ((angleToTarget * Mathf.PI) / 180)/* + Random.Range(-1f * jitter, 1f * jitter)*/;

                this.transform.position = new Vector3(player.position.x + offset.x + Mathf.Cos(angleToTarget) * jitter / 8, player.position.y + offset.y + Mathf.Sin(angleToTarget) * jitter / 8, offset.z);

                jitter--;
            }
            else
            {
                this.transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
            }

            //Debug.Log(destination.y - player.position.y);

            //jitter = new Vector2(jitter.x / 2, jitter.y / 2);

            if (!this.runOnlyOnce)
            {
                this.CheckScreenType();
            }
        }
        else
        {
            //Camera camera = GameObject.FindGameObjectWithTag("MainCamera");
            
            //camera.rect.x
        }
    }

    public enum CameraView
    {
        Free = 0,
        Square
    }

    [SerializeField]
    CameraView cameraView = CameraView.Square;
    [SerializeField]
    bool center = true;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float scale = 1.0f;
    [SerializeField]
    bool runOnlyOnce = false;

    // Internal
    float _cachedHeight = 0.0f;
    float _cachedWidth = 0.0f;

    void Start()
    {
        if (this.gameObject.GetComponent<Camera>() != null)
        {
            this.CheckScreenType();
        }
        else
        {
            this.transform.localPosition = new Vector3(offset.x, offset.y, offset.z);

        }
    }

    void CheckScreenType()
    {
        switch (this.cameraView)
        {
            case CameraView.Square:
                this.SetSquare();
                break;
            case CameraView.Free:
                {
                    this.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Gets the size of the screen.
    /// </summary>
    void RefreshScreenSize()
    {
        this._cachedHeight = Screen.height;
        this._cachedWidth = Screen.width;
    }

    /// <summary>
    /// Sets the square.
    /// </summary>
    void SetSquare()
    {
        this.RefreshScreenSize();
        if (this._cachedHeight < this._cachedWidth)
        {
            float ratio = this._cachedHeight / this._cachedWidth;

            this.GetComponent<Camera>().rect = new Rect(this.GetComponent<Camera>().rect.x, this.GetComponent<Camera>().rect.y, ratio, 1.0f);

            if (this.center == true)
            {
                this.GetComponent<Camera>().rect = new Rect(((1.0f - ratio * this.scale) / 2), this.GetComponent<Camera>().rect.y * this.scale, this.GetComponent<Camera>().rect.width * this.scale, this.GetComponent<Camera>().rect.height * this.scale);
            }
        }
        else
        {
            float ratio = this._cachedWidth / this._cachedHeight;

            this.GetComponent<Camera>().rect = new Rect(this.GetComponent<Camera>().rect.x, this.GetComponent<Camera>().rect.y, 1.0f, ratio);

            if (this.center == true)
            {
                this.GetComponent<Camera>().rect = new Rect(this.GetComponent<Camera>().rect.x, (1.0f - ratio) / 2, this.GetComponent<Camera>().rect.width, this.GetComponent<Camera>().rect.height);
            }
        }
    }

    public void ScrictView(CameraView cameraView)
    {
        this.cameraView = cameraView;
    }
}