using UnityEngine;
using UnityEngine.XR;
using Unity.Entities;

namespace LTSVRToolkit
{
	public class XRTrackerInput : XRTracker
	{
		protected override void UpdateTracker()
		{
			XRInput inputComponent = new XRInput();

			if( devices.Count > 0 )
			{
				InputDevice device = devices[0];

				Vector2 primaryAxis = Vector2.zero;
				device.TryGetFeatureValue( CommonUsages.primary2DAxis, out primaryAxis );
				
				inputComponent.PrimaryAxis = primaryAxis;
			}

			if( m_receivedEntity != Entity.Null )
			{
				m_manager.SetComponentData<XRInput>( m_receivedEntity, inputComponent );
			}
		}
	}
}