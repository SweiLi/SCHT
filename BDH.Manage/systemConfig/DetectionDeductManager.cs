using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.MdHelper;
using BDH.Sql;
using System.Globalization;
namespace BDH.Manage
{
   public  class DetectionDeductManager
    {
        #region 获取各检测指标扣分表
        public static List<DetectionDeduct> GetDetectionDeducts()
        {
            String sql = "SELECT detection_id, detection_max_scale,detection_cur_scale,detection_deduct_value FROM mdtbl_detection_deduct";
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return (MdDataTableHelper<DetectionDeduct>.ConvertToModel(sql_helper.ExecuteDataTable(sql))).ToList<DetectionDeduct>();
        }
        #endregion 获取各检测指标扣分表

        #region 获取某检测指标扣分值
        /// <summary>
        /// 获取某检测指标扣分值
        /// </summary>
        /// <param name="maxScale">构件最大标度</param>
        /// <param name="currentScale">当前标度</param>
        /// <returns></returns>
        public static float GetDetectionDeducts(int maxScale, int currentScale)
        {
            String sqlCon = String.Format("SELECT  detection_deduct_value FROM mdtbl_detection_deduct where detection_max_scale={0} and detection_cur_scale={1}", maxScale, currentScale);
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            float f = float.Parse(sql_helper.ExecuteObject(sqlCon).ToString());
            f = (float)Math.Round(f,2);
            return f;
        }
        #endregion 获取某检测指标扣分值

        public static float  GetMaxScaleByDiseases()
        {

            return 0.0f;
        }
    }
}
