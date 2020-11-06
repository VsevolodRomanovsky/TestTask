using System;
using System.ComponentModel.DataAnnotations;

namespace Danfoss.Entities
{
    public class House
    {
        /// <summary>
        /// Идентификатор дома на уровне БД
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// улица
        /// </summary>
        [Required]
        public string Street { get; set; }

        /// <summary>
        /// номер дома
        /// </summary>
        [Required]
        public string HouseNumber { get; set; }

        public Counter Counter { get; set; }
    }
}
