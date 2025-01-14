internal class DebtModel
{
    public int DebtId { get; set; }
    public DateTime DateTaken { get; set; }
    public DateTime DueDate { get; set; }
    public string? Source { get; set; }
    public string? Tags { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
    public string? Note { get; set; }

    public DebtModel()
    {
        Status = "Unpaid"; // Default value
    }
    public void ConvertAmount(decimal exchangeRate)
    {
        Amount = Math.Round(Amount * exchangeRate, 2); // Convert and round to 2 decimal places
    }
}
