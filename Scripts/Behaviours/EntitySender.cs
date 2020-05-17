using UnityEngine;
using Unity.Entities;

namespace LTSVRToolkit
{
	public class EntitySender : MonoBehaviour, IConvertGameObjectToEntity
	{
		public GameObject[] receivers;

		public void Convert( Entity entity, EntityManager manager, GameObjectConversionSystem conversionSystem )
		{
			foreach( GameObject go in receivers )
			{
				MonoBehaviour[] behaviours = go.GetComponents<MonoBehaviour>();

				foreach( MonoBehaviour mb in behaviours )
				{
					if( mb is IReceiveEntity )
					{
						( mb as IReceiveEntity ).ReceiveEntity( entity );
					}
				}
			}
		}
	}
}