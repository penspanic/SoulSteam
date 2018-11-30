using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Common.StaticData
{
	[Serializable]
	[XmlType("StarDustInfo")]
	public class StarDustInfo : EntityInfo
	{
	}
	[Serializable]
	[XmlType("PlanetInfo")]
	public class PlanetInfo : EntityInfo
	{
	}
	[XmlType("StarInfo")]
	public class StarInfo : EntityInfo
	{
	}
	[XmlType("BlackHoleInfo")]
	public class BlackHoleInfo : EntityInfo
	{
	}
	[Serializable]
	[XmlType("EntityInfo")] // define Type
	[
		XmlInclude(typeof(StarDustInfo)),
		XmlInclude(typeof(PlanetInfo)),
		XmlInclude(typeof(StarInfo)),
		XmlInclude(typeof(BlackHoleInfo)),
	]
	public abstract class EntityInfo : StringKeyData
	{
	}
}
