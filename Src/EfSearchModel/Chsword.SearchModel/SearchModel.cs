using System.Collections.Generic;

namespace Chsword
{
    /// <summary>
    /// �û��Զ��ռ�����������Model
    /// </summary>
    public class SearchModel
    {
        public SearchModel()
        {
            Items = new List<ConditionItem>();
        }
        /// <summary>
        /// ��ѯ����
        /// </summary>
        public ICollection<ConditionItem> Items { get; internal set; }     

    }
}