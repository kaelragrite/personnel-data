using PersonnelData.Data.ResultObjects;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

public class PersonRelationGroupReportResponse
{
    public PersonRelationGroupReportResponse(RelationGroupReportResult reportResult) =>
        ReportResults = reportResult.GroupObjects.Select(x => new ReportResult(x.RelationType, x.Count)).ToList();

    public List<ReportResult> ReportResults { get; }
    
    public class ReportResult
    {
        public ReportResult(RelationType relationType, int count)
        {
            RelationType = relationType;
            Count = count;
        }
        
        public RelationType RelationType { get; }
        public int Count { get; }
    }
}