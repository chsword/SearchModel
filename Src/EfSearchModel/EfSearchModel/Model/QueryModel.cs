namespace EfSearchModel.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// �û��Զ��ռ�����������Model
    /// </summary>
    public class QueryModel
    {
        public QueryModel()
        {
            Items = new List<ConditionItem>();
        }
        /// <summary>
        /// ��ѯ����
        /// </summary>
        public List<ConditionItem> Items { get; set; }     

    }
}