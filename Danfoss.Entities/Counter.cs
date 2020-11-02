using System;
using System.Collections.Generic;
using System.Text;

namespace Danfoss.Entities
{
    public class Counter
    {
        /// <summary>
        /// Идентификатор счетчика на уровне БД
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Идентификатор счетчика (заводской номер)
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Показания счетчика
        /// </summary>
        public long Value { get; set; }

        public House House { get; set; }
        public int HouseId { get; set; }
    }
}
