using System.Collections.Generic;
using UnityEngine;

namespace Data.Story
{
    /// <summary>
    /// 一段剧情情节。
    /// </summary>
    [CreateAssetMenu(fileName = "PlotData", menuName = "Data/Plot Data")]
    public class PlotDataSO : ScriptableObject
    {
        // 用于选择时显示
        public string plotName;
        
        public List<PlotSection> sections = new List<PlotSection>();

        public int Count => sections.Count;
        
        public PlotSection this[int i] {
            get => sections[i];
            set => sections[i] = value;
        }
        
        public void Add(PlotSection section)
        {
            sections.Add(section);
        }
    }
}
