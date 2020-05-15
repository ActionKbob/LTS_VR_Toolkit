using UnityEngine;
using UnityEngine.XR;
using Unity.Entities;
using Unity.Transforms;

namespace LTSVRToolkit
{
	public class XRTrackerTransform : XRTracker
	{
		protected override void UpdateTracker()
		{
			base.UpdateTracker();

			Vector3 position = transform.localPosition;
			Quaternion rotation = transform.localRotation;

			if( devices.Count > 0 )
			{
				InputDevice device = devices[0];
				
				device.TryGetFeatureValue( CommonUsages.devicePosition, out position );
				device.TryGetFeatureValue( CommonUsages.deviceRotation, out rotation );
			}

			if( m_receivedEntity != Entity.Null )
			{
				Translation entityPosition = m_manager.GetComponentData<Translation>( m_receivedEntity );
				Rotation entityRotation = m_manager.GetComponentData<Rotation>( m_receivedEntity );

				entityPosition.Value = position;
				entityRotation.Value = rotation;

				m_manager.SetComponentData<Translation>( m_receivedEntity, entityPosition );
				m_manager.SetComponentData<Rotation>( m_receivedEntity, entityRotation );
			}

			transform.localPosition = position;
			transform.localRotation = rotation;

			GizmoDebugger.DrawPrimitive( PrimitiveType.Cylinder, Color.magenta, new Vector3( transform.position.x, 0, transform.position.z ), Quaternion.identity, new Vector3( 1, 0, 1 ) );
		}
	}
}