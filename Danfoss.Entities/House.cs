using System;

namespace Danfoss.Entities
{
    public class House
    {
        /// <summary>
        /// Идентификатор дома на уровне БД
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// адрес дома дома
        /// </summary>
        public string AddressLine { get; set; }
        
        /// <summary>
        /// улица
        /// </summary>
        public string Street { get; set; }
        
        /// <summary>
        /// номер дома
        /// </summary>
        public string HouseNumber { get; set; }

        public Counter Counter { get; set; }
    }
}
