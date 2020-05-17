// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR;

// /** ------------------------------------------------------------
// 	THIS DOES NOT WORK WITH OCULUS QUEST / LINK
// 	COME BACK TO THIS WHEN IT IS CONFIRMED TO WORK
//     ------------------------------------------------------------ **/

// namespace LTSVRToolkit
// {
// 	public class XRTrackerBounds : MonoBehaviour
// 	{
// 		private List<Vector3> boundaryPoints;

// 		void Start()
// 		{
// 			List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();;
// 			SubsystemManager.GetInstances<XRInputSubsystem>( subsystems );

// 			if( subsystems.Count > 0 )
// 			{
// 				Debug.Log("SS: " + subsystems.Count);
// 				OnBoundaryChanged( subsystems[0] );
// 				subsystems[0].boundaryChanged += OnBoundaryChanged;
// 			}
// 		}

// 		private void OnBoundaryChanged( XRInputSubsystem subsystem )
// 		{
// 			List<Vector3> boundary = new List<Vector3>();

// 			if( subsystem.TryGetBoundaryPoints( boundary ) )
// 			{
// 				for( int i = 0; i < boundary.Count; i++ )
// 				{
// 					Vector3 p1 = boundary[i];
// 					Vector3 p2 = ( i + 1 < boundary.Count ) ? boundary[ i + 1 ] : boundary[0];

// 					GizmoDebugger.DrawLine( Color.green, p1, p2 );
// 				}
// 			}
// 		}
// 	}
// }