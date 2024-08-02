using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Item
{
    public interface IWorldItemTemplateList
    {
        IEnumerable<IWorldItemTemplateListEntry> WorldItemTemplates { get; }
    }
}
