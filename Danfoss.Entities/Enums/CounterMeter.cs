using System.ComponentModel;
using System.Runtime.Serialization;

namespace Danfoss.Entities.Enums
{
    /// <summary>
    /// Тип показаний счетчика воды
    /// </summary>
    public enum CounterMeter
    {
        /// <summary>
        /// Все значения"
        /// </summary>
        [Description("Все значения")]
        AllValues,

        /// <summary>
        /// Максимальные значения"
        /// </summary>
        [Description("Максимальные значения")]
        MaxValues,

        /// <summary>
        /// Минимальное значение
        /// </summary>
        [Description("Минимальные значения")]
        MinValues = 2
    }
}
