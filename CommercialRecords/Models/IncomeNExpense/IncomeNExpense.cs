using CommercialRecords.Models;
using System;

namespace CommercialRecords.Models.IncomeNExpense
{
    class IncomeNExpense : ModelBase
    {
        public enum MODE { INCOME, EXPENSE }
        public MODE Mode { get; set; }
        public int Type { get; set; }
        public int AccountId { get; set; }
        public string Details { get; set; }
        public double Cost { get; set; }
        public DateTime Date { get; set; }
    }
}
