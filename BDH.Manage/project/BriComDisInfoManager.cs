using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.Sql;
using System.Globalization;

namespace BDH.Manage
{
    public class BriComDisInfoManager
    {
        #region 获取桥梁病害指标的最大检测标度
        public Dictionary<string,int> getDiseasesMaxScale(String bridgetype,string component,string member)
        {
            //桥梁类型：梁式桥--（混凝土梁式桥和钢架梁式桥）上部结构、1#橡胶支座

            Dictionary<string, int> diseasesMaxScales = new Dictionary<string, int>();

            return diseasesMaxScales;
        }

        #endregion 获取桥梁病害指标的最大检测标度
    }
}
