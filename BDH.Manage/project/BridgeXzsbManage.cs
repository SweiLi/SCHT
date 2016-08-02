using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.Sql;
using BDH.MdHelper;
using System.IO;

namespace BDH.Manage
{
    public class BridgeXzsbManage
    {
        /// <summary>
        ///  创建桥梁编号
        /// </summary>
        /// <returns>ql+s(t)+002</returns>
        public static String CreateBridgeId()
        {
            String strDate = DateTime.Now.ToFlagString();
            String sql = "select count(bri_id) from mdtbl_bridge_xzsb ";
            int count = SqlHelperFactory.GetDefaultSqlHelper().ExecuteInt32(sql);
            String briId = string.Format("QL{0:s}{1:D3}", strDate, count);
            String driver = Directory.GetCurrentDirectory().Substring(0, 1) + @":\htBridge\";
            //创建一个文件夹，文件夹名称为briId
            MdDirHelper.CreateDir(driver+briId);
            return briId;
        }
        /// <summary>
        /// 新建桥梁行政识别数据
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int AddBridgeXzsb(Bridge x)
        {
            String sqlCon = string.Format("INSERT INTO mdtbl_bridge_xzsb(bri_id ,bri_code,bri_name,bri_pic_name,bri_xzsb_number,bri_xzsb_name,bri_xzsb_level,bri_xzsb_pile,bri_xzsb_funtype,bri_xzsb_downchannel,bri_xzsb_downpile , bri_xzsb_designload, bri_xzsb_goload, bri_xzsb_slopelend, bri_xzsb_pavement, bri_xzsb_unit, bri_xzsb_date, is_cancel, prj_id) " +
                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{19}')",
                x.Id, x.Code, x.Name, x.PictureName, x.XzsbNumber, x.XzsbName, x.XzsbLevel, x.XzsbPile,
                x.XzsbFuntype, x.XzsbDownchannel, x.XzsbDownpile, x.XzsbDesignload, x.XzsbGoload,
                x.XzsbSlopelend, x.XzsbPavement, x.XzsbUnit, x.XzsbDate, x.IsCancel, x.ProjectId);
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return sql_helper.ExecuteNonQuery(sqlCon);
        }
        /// <summary>
        /// 修改桥梁行政识别数据
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UpdateBridgeXzsb(Bridge x)
        {
            String sqlCon = string.Format("UPDATE mdtbl_bridge_xzsb   SET bri_code = '{0}' , bri_name = '{1}' , bri_pic_name = '{2}' , bri_xzsb_number = '{3}', bri_xzsb_name = '{4}', bri_xzsb_level = '{5}' ," +
                " bri_xzsb_pile = '{6}', bri_xzsb_funtype = '{7}', bri_xzsb_downchannel = '{8}', bri_xzsb_downpile = '{9}', bri_xzsb_designload = '{10}', bri_xzsb_goload = '{11}', bri_xzsb_slopelend = '{12}'," +
                "bri_xzsb_pavement = '{13}' , bri_xzsb_unit = '{14}' , bri_xzsb_date = '{15}' , is_cancel = '{16}', prj_id = '{17}'  WHERE bri_id = '{18}'",
                x.Code, x.Name, x.PictureName, x.XzsbNumber, x.XzsbName, x.XzsbLevel, x.XzsbPile, x.XzsbFuntype,
                x.XzsbDownchannel, x.XzsbDownpile, x.XzsbDesignload, x.XzsbGoload, x.XzsbSlopelend,
                x.XzsbPavement, x.XzsbUnit, x.XzsbDate, x.IsCancel, x.ProjectId, x.Id);
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return sql_helper.ExecuteNonQuery(sqlCon);
        }
        /// <summary>
        /// 注销数据
        /// </summary>
        /// <param name="briId"></param>
        /// <param name="isCancel">0 or 1</param>
        /// <returns></returns>
        public static int IsCancel(String briId,int isCancel)
        {
            String sqlCon = string.Format("UPDATE mdtbl_bridge_xzsb  SET  is_cancel ='{0}' WHERE bri_id = '{1}'", briId,isCancel);
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return sql_helper.ExecuteNonQuery(sqlCon);
        }

    }
}
