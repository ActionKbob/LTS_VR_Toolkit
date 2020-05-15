using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.Entities;

namespace LTSVRToolkit
{
	public class XRTracker : MonoBehaviour, IReceiveEntity
	{
		protected Entity m_receivedEntity = Entity.Null;
		protected EntityManager m_manager;

		public InputDeviceCharacteristics characteristics;

		protected List<InputDevice> devices = new List<InputDevice>();

		public void ReceiveEntity( Entity entity )
		{
			m_receivedEntity = entity;
			m_manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		}

		protected virtual void Awake()
		{
			List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
			SubsystemManager.GetInstances<XRInputSubsystem>( subsystems );
			for( int i = 0; i < subsystems.Count; i++ )
			{
				if( subsystems[i].GetTrackingOriginMode() != TrackingOriginModeFlags.Floor )
					subsystems[i].TrySetTrackingOriginMode( TrackingOriginModeFlags.Floor );
			}

			Application.onBeforeRender += BeforeRender;
		}

		void Update()
		{
			
		}

		void FixedUpdate()
		{
			UpdateTracker();
		}

		protected virtual void BeforeRender()
		{
			UpdateTracker();
		}

		protected virtual void UpdateTracker()
		{
			if( devices.Count == 0 )
			{
				InputDevices.GetDevicesWithCharacteristics( characteristics, devices );
			}
		}
	}
}