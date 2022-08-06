using UnityEngine;

public class ScreenBorders
{
    public static ScreenBorders Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScreenBorders();
                _instance._playerTransf = MonoBehaviour.FindObjectOfType<Player>().Tranform;
                _instance.CalculateBorders();
            }
            return _instance;
        }
    }
    private static ScreenBorders _instance;

    private const int SafeZoneSize = 5;
    private Transform _playerTransf;
    private Borders _borders;

    public Borders Borders => _borders;

    private void CalculateBorders()
    {
        float camSize = Camera.main.orthographicSize;
        Vector2 camPosition = Camera.main.transform.position;

        float width = Screen.currentResolution.width;
        float height = Screen.currentResolution.height;
        float WToH = width / height;
        
        _borders = new Borders();
        _borders.Top = camSize + camPosition.y;
        _borders.Bottom = -camSize + camPosition.y;
        _borders.Right = camSize * WToH + camPosition.x;
        _borders.Left = -camSize * WToH + camPosition.x;
    }

    public Vector2 RandomPoint()
    {
        Vector2 point = Vector2.zero;
        Vector2 safeZone = _playerTransf.position;
        float SafeZoneLeft = safeZone.x - SafeZoneSize;
        float SafeZoneRight = safeZone.x + SafeZoneSize;
        float SafeZoneBottom = safeZone.y - SafeZoneSize;
        float SafeZoneTop = safeZone.y + SafeZoneSize;

        bool inSafeZone = false;
        do
        {
            point.x = Random.Range(_borders.Left, _borders.Right);
            point.y = Random.Range(_borders.Bottom, _borders.Top);
            if ((point.x > SafeZoneLeft && point.x < SafeZoneRight) 
                && (point.y > SafeZoneBottom && point.y < SafeZoneTop))
                inSafeZone = true;
            else
                inSafeZone = false;
        }
        while (inSafeZone);
        return point;
    }

    public void CheckBorderCrossing(Transform transform)
    {
        Vector3 position = transform.position;
        
        if(position.x > _borders.Right)
            position.x = _borders.Left;
        else if(position.x < _borders.Left)
            position.x = _borders.Right;
        else if(position.y > _borders.Top)
            position.y = _borders.Bottom;
        else if(position.y < _borders.Bottom)
            position.y = _borders.Top;
        
        if(position != transform.position)
            transform.position = position;
    }
}


public struct Borders
{
    public float Left, Right, Top, Bottom;
}
