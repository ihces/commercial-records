using SQLite;
using System;

namespace CommercialRecords.Models
{
    public class ModelBase
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class InternalAttribute : Attribute
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
    }
}
