namespace ContactApp.Report.Domain.Aggregates;

public class ReportData
{
    public string Location { get; private set; }
    public int TotalPerson { get; private set; }
    public int TotalPhoneNumber { get; private set; }

    public ReportData(string location, int totalPerson, int totalPhoneNumber)
    {
        Location = location;
        TotalPerson = totalPerson;
        TotalPhoneNumber = totalPhoneNumber;
    }
}