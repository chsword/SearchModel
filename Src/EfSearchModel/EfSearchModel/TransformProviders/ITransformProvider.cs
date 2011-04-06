namespace EfSearchModel
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface ITransformProvider
    {
        bool Match(ConditionItem item, Type type);
        IEnumerable<ConditionItem> Transform(ConditionItem item, Type type);
    }
}