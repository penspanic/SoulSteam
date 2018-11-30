
using Common.Logic;
using System.Collections.Generic;
namespace Common.StaticData
{
    public class UnitInfo : IntegerKeyData
    {
        public Types.UnitType Type;
        public int ActionId;
        public string Name;
        public float DefenseRadius;
        public int MaxHp;
        public int Cost;
        public float MoveSpeed;
        
        public VAPathInfo AppearEffectPath;
        public VAPathInfo DeadEffectPath;
    }
}