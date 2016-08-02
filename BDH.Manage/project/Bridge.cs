using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁行政识别数据
    /// </summary>
   public  class Bridge
    {
        //桥梁ID
        public string Id { get; set; }
        //项目ID
        public string ProjectId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PictureName { get; set; }
        public string XzsbNumber { get; set; }

        public string XzsbName { get; set; }
        public int XzsbLevel { get; set; }

        public string XzsbPile { get; set; }
        public string XzsbFuntype { get; set; }
        public string XzsbDownchannel { get; set; }
        public string XzsbDownpile { get; set; }
        public string XzsbDesignload { get; set; }
        public float XzsbGoload { get; set; }

        public string XzsbSlopelend { get; set; }
        public string XzsbPavement { get; set; }
        public string XzsbUnit { get; set; }
        public DateTime XzsbDate { get; set; }

        public int IsCancel { get; set; }
    }
}
