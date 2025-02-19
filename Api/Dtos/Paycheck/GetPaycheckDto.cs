namespace Api.Dtos.Paycheck;

public class GetPaycheckDto
{
    public decimal GrossPay { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetPay { get; set; }
}