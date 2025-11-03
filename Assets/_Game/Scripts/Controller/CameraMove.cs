using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float duration = 1f; 
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;

    private Transform target;
    private bool isMoving = false;
    private Vector3 startPos;
    private float elapsed = 0f;

    public void MoveCamera1()
    {
        transform.position = pos1.position;
        target = pos2;
        Invoke(nameof(Move), 0.7f);
    }
    public void MoveCamera2()
    {
        target = pos3;
        Invoke(nameof(Move), 0.3f);
    }

    public void Move()
    {
        startPos = transform.position;
        elapsed = 0f;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving && target != null)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = Mathf.Clamp01(t);

            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(startPos, target.position, smoothT);

            if (t >= 1f)
            {
                transform.position = target.position;
                isMoving = false;
            }
        }
    }
}
