namespace Sicma.Common
{
    public static class Helpers
    {
        public static int CalculatePageNumber(int totalRecords, int totalRows)
        {
            return (int)Math.Ceiling((double)totalRecords / totalRows);
        }
    }
}
