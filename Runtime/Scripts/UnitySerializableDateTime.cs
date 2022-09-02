using System;
using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    [Serializable]
    public struct UnitySerializableDateTime : ISerializationCallbackReceiver
    {
        private DateTime dateTime;

        [SerializeField]
        private string ticks;

        public UnitySerializableDateTime(DateTime dateTime)
        {
            this.dateTime = dateTime;
            ticks = dateTime.Ticks.ToString();
        }

        public DateTime DateTime => dateTime;

        public void OnBeforeSerialize() => ticks = dateTime.Ticks.ToString();

        public void OnAfterDeserialize() => dateTime = new DateTime(long.Parse(ticks));

        public static implicit operator DateTime(UnitySerializableDateTime sdt) => sdt.DateTime;
        public static implicit operator UnitySerializableDateTime(DateTime dt) => new(dt);
    }
}