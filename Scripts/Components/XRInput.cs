using Unity.Entities;
using Unity.Mathematics;

namespace LTSVRToolkit
{
	public struct XRInput : IComponentData
	{
		public float2 PrimaryAxis;
	}
}