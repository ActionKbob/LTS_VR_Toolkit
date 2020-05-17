using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace LTSVRToolkit
{
	public static class XRUtilities
	{
		public static void SetTrackingOrigin( TrackingOriginModeFlags trackingOriginMode )
		{
			List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
			SubsystemManager.GetInstances<XRInputSubsystem>( subsystems );
			for( int i = 0; i < subsystems.Count; i++ )
			{
				if( subsystems[i].GetTrackingOriginMode() != trackingOriginMode )
					subsystems[i].TrySetTrackingOriginMode( trackingOriginMode );
			}
		}
	}
}