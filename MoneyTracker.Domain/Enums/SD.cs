namespace MoneyTracker.Domain.Enums
{
    public static class SD
    {
        public enum RecurrenceType
        {
            None = 0,
            Daily = 1,
            Weekly = 2,
            Monthly = 3,
            Yearly = 4
        }
        public enum CategoryType
        {
            Income = 0 ,
            Expense = 1
        }
        public enum TransactionType
        {
            Income,
            Expense,
        }
        public enum Gender
        {
            Male = 0 ,
            Female = 1 ,
            Other = 2 
        }
        public enum RoleType
        {
            Admin,
            User
        }
    }
    
}
