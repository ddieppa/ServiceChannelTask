namespace ServiceChannel.Test.Domain.Requests;

public class Covid19DataFilterDto
{
    public Location? Location { get; set; }

    public DateRange? DateRange { get; set; }
}
