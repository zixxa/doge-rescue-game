using System;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class LineDrawer: MonoBehaviour
{
    private bool lockDraw = false;
    private EventBus _eventBus;
    private int cantDrawOverLayerIndex;
    [SerializeField] private LayerMask cantDrawOverLayer;
    [SerializeField] public Line linePrefab;
    [SerializeField] public float minDistance;
    [SerializeField] private float width;
    Line currentLine;
    private Camera camera;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<NextLevelSignal>(UnlockDraw);
        camera = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }
    
    private void UnlockDraw(NextLevelSignal signal)
    {
        lockDraw = false;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !lockDraw)
            BeginDraw();
        else if (Input.GetMouseButtonUp(0))
            EndDraw();
        if (currentLine != null)
            Draw();
    }
    
    private void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        currentLine.SetPointMinDistance(minDistance);
        currentLine.SetLineWidth(width);
    }

    private void Draw()
    {
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, width/2f, Vector2.zero, 1f, cantDrawOverLayer);
        
        if (hit)
            EndDraw();
        else
            currentLine.AddPoint(mousePosition);
    }

    private void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.PointsCount<3)
                Destroy(currentLine.gameObject);
            else
            {
                currentLine.transform.gameObject.layer = cantDrawOverLayerIndex;
                currentLine.UsePhysics();
                currentLine = null;
                lockDraw = true;
                _eventBus.Invoke(new SpawnEnemiesSignal());
            }
        }
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<GameStartSignal>(signal => lockDraw = true);
    }
}