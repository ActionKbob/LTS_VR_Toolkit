using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.Entities;
using Unity.Transforms;

namespace LTSVRToolkit
{
	public class XRTracker : MonoBehaviour, IReceiveEntity
	{
		[ System.Flags ]
		public enum TrackingValues
		{
			Nothing = 0,
			Position = 1,
			Rotation = 2
		}

		protected Entity m_receivedEntity = Entity.Null;
		protected EntityManager m_entityManager;

		public InputDeviceCharacteristics characteristics;
		public TrackingValues trackingValues;

		protected List<InputDevice> devices = new List<InputDevice>();

		public void ReceiveEntity( Entity entity )
		{
			m_receivedEntity = entity;
			m_entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
		}

		void Awake()
		{
			Application.onBeforeRender += BeforeRender;
		}

		void FixedUpdate()
		{
			UpdateTracker();
		}

		private void BeforeRender()
		{
			UpdateTracker();
		}

		private void UpdateTracker()
		{
			if( devices.Count == 0 )
			{
				InputDevices.GetDevicesWithCharacteristics( characteristics, devices );
			}

			if( devices.Count > 0 )
			{
				InputDevice device = devices[0];

				if( trackingValues.HasFlag( TrackingValues.Position ) )
				{
					device.TryGetFeatureValue( CommonUsages.devicePosition, out Vector3 position );
					transform.localPosition = position;
					UpdateEntityPosition();
				}

				if( trackingValues.HasFlag( TrackingValues.Rotation ) )
				{
					device.TryGetFeatureValue( CommonUsages.deviceRotation, out Quaternion rotation );
					transform.localRotation = rotation;
					UpdateEntityRotation();
				}
			}
		}

		private void UpdateEntityPosition()
		{
			if( m_receivedEntity != Entity.Null )
			{
				Translation position = m_entityManager.GetComponentData<Translation>( m_receivedEntity );
				position.Value = transform.position;
				m_entityManager.SetComponentData<Translation>( m_receivedEntity, position );
			}
		}

		private void UpdateEntityRotation()
		{
			if( m_receivedEntity != Entity.Null )
			{
				Rotation rotation = m_entityManager.GetComponentData<Rotation>( m_receivedEntity );
				rotation.Value = transform.rotation;
				m_entityManager.SetComponentData<Rotation>( m_receivedEntity, rotation );
			}
		}
	}
}