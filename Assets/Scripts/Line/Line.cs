using System;
using System.Collections.Generic;
using CustomEventBus.Signals;
using UnityEngine;
using EventBus = CustomEventBus.EventBus;


public class Line: MonoBehaviour
{
   private LineRenderer LineRenderer => gameObject.GetComponent<LineRenderer>();
   private Rigidbody2D Rigidbody => gameObject.GetComponent<Rigidbody2D>();
   private EdgeCollider2D EdgeCollider2D=> gameObject.GetComponent<EdgeCollider2D>();
   
   private Vector3 _previousPosition;
   private float _minDistance = 1f;
   private List<Vector2> points = new List<Vector2>();
   private int _pointsCount = 0;
   private float _circleCollider2DRadius;
   private EventBus _eventBus;
   public int PointsCount => _pointsCount;

   public void Awake()
   {
      _eventBus = ServiceLocator.Current.Get<EventBus>();
      _eventBus.Subscribe<GameClearSignal>(OnClean);
   }
   

   public void AddPoint(Vector2 newPoint)
   {
      if (_pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < _minDistance)
         return;
      points.Add(newPoint);
      _pointsCount++;

      CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
      circleCollider2D.offset = newPoint;
      circleCollider2D.radius = _circleCollider2DRadius;

      LineRenderer.positionCount = _pointsCount;
      LineRenderer.SetPosition(_pointsCount  - 1, newPoint);

      if (_pointsCount > 1)
         EdgeCollider2D.points = points.ToArray();
   }

   private Vector2 GetLastPoint() => (Vector2)LineRenderer.GetPosition(_pointsCount - 1);

   public void UsePhysics()
   {
      Rigidbody.bodyType = RigidbodyType2D.Dynamic;
   }

   public void SetPointMinDistance(float distance)
   {
      _minDistance = distance;
   }

   public void SetLineWidth(float width)
   {
      LineRenderer.startWidth = width;
      LineRenderer.endWidth = width;

      _circleCollider2DRadius = width / 2f;
      EdgeCollider2D.edgeRadius = _circleCollider2DRadius;
   }

   private void OnClean (GameClearSignal signal)
   {
      Destroy(this.gameObject);      
   }

   private void OnDestroy()
   {
      _eventBus.Unsubscribe<GameClearSignal>(OnClean);
   }
}