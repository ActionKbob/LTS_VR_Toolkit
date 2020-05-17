using System.Collections.Generic;
using UnityEngine;

namespace LTSVRToolkit
{
	public class GizmoDebugger : MonoBehaviour
	{
		public static GizmoDebugger Instance{ get; private set; }

		private static Dictionary<PrimitiveType, Mesh> m_primitiveCollection = new Dictionary<PrimitiveType, Mesh>();

		private List<PrimitiveData> primitiveDebugs;
		private List<LineData> lineDebugs;
		private List<RayData> rayDebugs;

		private struct PrimitiveData
		{
			public PrimitiveType Type;
			public Color Color;
			public Vector3 Position;
			public Quaternion Rotation;
			public Vector3 Scale;
		}

		private struct LineData
		{
			public Color Color;
			public Vector3 Start;
			public Vector3 End;
		}

		private struct RayData
		{
			public Color Color;
			public Vector3 Position;
			public Vector3 Direction;
		}

		void Awake()
		{
			Instance = this;

			primitiveDebugs = new List<PrimitiveData>();
			lineDebugs = new List<LineData>();
			rayDebugs = new List<RayData>();
		}

		void OnDrawGizmos()
		{
			if( primitiveDebugs != null )
			{
				for( int i = 0; i < primitiveDebugs.Count; i++ )
				{
					Gizmos.color = primitiveDebugs[i].Color;
					Gizmos.DrawWireMesh( GetPrimitiveMesh( primitiveDebugs[i].Type ), 0, primitiveDebugs[i].Position, primitiveDebugs[i].Rotation, primitiveDebugs[i].Scale );
				}

				primitiveDebugs.Clear();
			}

			if( lineDebugs != null )
			{
				for( int i = 0; i < lineDebugs.Count; i++ )
				{
					Gizmos.color = lineDebugs[i].Color;
					Gizmos.DrawLine( lineDebugs[i].Start, lineDebugs[i].End );
				}

				lineDebugs.Clear();
			}

			if( rayDebugs != null )
			{
				for( int i = 0; i < rayDebugs.Count; i++ )
				{
					Gizmos.color = rayDebugs[i].Color;
					Gizmos.DrawRay( rayDebugs[i].Position, rayDebugs[i].Direction );
				}

				rayDebugs.Clear();
			}
		}

		public static void DrawPrimitive( PrimitiveType type, Color color, Vector3 position, Quaternion rotation )
		{
			DrawPrimitive( type, color, position, rotation, Vector3.one );
		}

		public static void DrawPrimitive( PrimitiveType type, Color color, Vector3 position, Quaternion rotation, Vector3 scale )
		{
			if( Instance != null )
			{
				Instance.primitiveDebugs.Add( new PrimitiveData{
					Type = type,
					Color = color,
					Position = position,
					Rotation = rotation,
					Scale = scale
				} );
			}
		}

		public static void DrawLine( Color color, Vector3 start, Vector3 end )
		{
			if( Instance != null )
			{
				Instance.lineDebugs.Add( new LineData{
					Color = color,
					Start = start,
					End = end
				} );
			}
		}

		public static void DrawRay( Color color, Vector3 position, Vector3 direction )
		{
			if( Instance != null )
			{
				Instance.rayDebugs.Add( new RayData{
					Color = color,
					Position = position,
					Direction = direction
				} );
			}
		}

		public static Mesh CreatePrimitiveMesh( PrimitiveType type )
		{
			GameObject primitiveGameObject = GameObject.CreatePrimitive( type );
			Mesh primitiveMesh = primitiveGameObject.GetComponent<MeshFilter>().sharedMesh;
			GameObject.DestroyImmediate( primitiveGameObject );

			m_primitiveCollection[ type ] = primitiveMesh;

			return primitiveMesh;
		}

		public static Mesh GetPrimitiveMesh( PrimitiveType type )
		{
			if( m_primitiveCollection.TryGetValue( type, out Mesh mesh ) )
			{
				return mesh;
			}
			else
			{
				return CreatePrimitiveMesh( type );
			}
		}
	}
}