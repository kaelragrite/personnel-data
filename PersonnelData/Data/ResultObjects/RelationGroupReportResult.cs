using PersonnelData.Shared.Enums;

namespace PersonnelData.Data.ResultObjects;

public class RelationGroupReportResult
{
    public RelationGroupReportResult(List<GroupReportObject> groupObjects) => GroupObjects = groupObjects;

    public List<GroupReportObject> GroupObjects { get; }
    
    public class GroupReportObject
    {
        public GroupReportObject(RelationType relationType, int count)
        {
            RelationType = relationType;
            Count = count;
        }
        
        public RelationType RelationType { get; }
        public int Count { get; }
    }
}