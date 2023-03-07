using System;
using UnityEngine;
using Object = UnityEngine.Object;


public static class BuilderExtension
    {
        public static GameObject SetName(this GameObject gameObject, string name)
        {
            gameObject.name = name;
            return gameObject;
        }

        public static GameObject AddRigidbody(this GameObject gameObject, float mass, float angularDrag, bool isGravity, bool isFreeze)
        {
            var component = gameObject.GetOrAddComponent<Rigidbody>();
            component.mass = mass;
            component.angularDrag = angularDrag;
            component.useGravity = isGravity;
            component.freezeRotation = isFreeze;
            return gameObject;
        }

        public static GameObject AddBoxCollider2D(this GameObject gameObject)
        {
            gameObject.GetOrAddComponent<BoxCollider2D>();
            return gameObject;
        }

        public static GameObject AddCircleCollider2D(this GameObject gameObject)
        {
            gameObject.GetOrAddComponent<CircleCollider2D>();
            return gameObject;
        }

        public static GameObject AddSprite(this GameObject gameObject, Sprite sprite)
        {
            var component = gameObject.GetOrAddComponent<SpriteRenderer>();
            component.sprite = sprite;
            return gameObject;
        }
        
        public static GameObject AddUnit(this GameObject gameObject, CharacterData character)
        {
            gameObject = Object.Instantiate(character.Player, character.TransformSpawn.position, Quaternion.identity);
            return gameObject;
        }

        public static GameObject AddCursor(this GameObject gameObject, CursorData cursor)
        {
            gameObject = Object.Instantiate(cursor.Cursor, cursor.Cursor.transform.position, Quaternion.identity);
            return gameObject;
        }

        public static GameObject AddTrailRenderer(this GameObject gameObject)
        {
            var componentInChildren = gameObject.GetComponentInChildren<TrailRenderer>();
            if (componentInChildren)
            {
                return gameObject;
            }
            var lineRendererGameObject = new GameObject("TrailRenderer");
            var lineRenderer = lineRendererGameObject.AddComponent<TrailRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.time = 0.1f;
            lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.blue;
            lineRendererGameObject.transform.SetParent(gameObject.transform);
            return gameObject;
        }

        private static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var result = gameObject.GetComponent<T>();
            if (!result)
            {
                result = gameObject.AddComponent<T>();
            }

            return result;
        }
        
        public static Camera AddComponentCamera(this GameObject gameObject)
        {
            var camera = gameObject.AddComponent<Camera>();
            camera.depth = -5;
            camera.cullingMask = 1;
            camera.orthographic = true;
            camera.farClipPlane = 100;
            camera.useOcclusionCulling = false;

            return camera;
        }
        
        public static bool TryBool(this string self)
        {
            return Boolean.TryParse(self, out var res) && res;
        }
    }