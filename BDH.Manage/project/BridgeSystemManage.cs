using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.Sql;
using BDH.MdHelper;


namespace BDH.Manage
{
    public class BridgeSystemManage
    {
        #region 桥梁管理部分
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
            MdDirHelper.CreateDir(driver + briId);
            return briId;
        }

        /// <summary>
        /// 新建桥梁行政识别数据
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int AddBridge(Bridge x)
        {
            String sqlCon = string.Format("INSERT INTO mdtbl_bridge_xzsb(bri_id ,bri_code,bri_name," + 
                "bri_pic_name,bri_xzsb_number,bri_xzsb_name,bri_xzsb_level,bri_xzsb_pile,bri_xzsb_funtype," + 
                "bri_xzsb_downchannel,bri_xzsb_downpile , bri_xzsb_designload, bri_xzsb_goload, " +
                "bri_xzsb_slopelend, bri_xzsb_pavement, bri_xzsb_unit, bri_xzsb_date, is_cancel, prj_id) " +
                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', " +
                "'{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}')",
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
        public static int UpdateBridge(Bridge x)
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
        public static int Cancel(String briId, int isCancel)
        {
            String sqlCon = string.Format("UPDATE mdtbl_bridge_xzsb  SET  is_cancel ='{0}' WHERE bri_id = '{1}'", briId, isCancel);
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return sql_helper.ExecuteNonQuery(sqlCon);
        }

        public static bool ExistsBridge(string bridgeId)
        {
            string sql = string.Format("select count(*) from mdtbl_bridge_xzsb where bri_id='{0}'", bridgeId);
            int count = SqlHelperFactory.GetDefaultSqlHelper().ExecuteInt32(sql);

            return count > 0;
        }

        #endregion 桥梁管理部分

        #region 子桥梁管理部分
        /// <summary>
        ///  创建子桥梁编号
        /// </summary>
        /// <returns>ql+s(t)+002</returns>
        public static String CreateChildBridgeId()
        {
            String strDate = DateTime.Now.ToFlagString();
            String sql = "select count(bridge_children_id) from mdtbl_bridge_children ";
            int count = SqlHelperFactory.GetDefaultSqlHelper().ExecuteInt32(sql);
            return string.Format("ZQ{0:s}{1:D3}", strDate, count);
        }
        /// <summary>
        /// 获取所有子桥梁信息，只显示未注销桥梁信息，已注销的当删除
        /// </summary>
        /// <returns></returns>
        public static List<ChildBridge> GetChildBridges()
        {
            // String sqlCon = "SELECT bridge_children_id,bri_id,type_id,bridge_children_name,bridge_children_discribe,bridge_pile FROM mdtbl_bridge_children";
            String sql = "SELECT bridge_children_id, a.bri_id,type_id,bridge_children_name,bridge_children_discribe,bridge_pile FROM mdtbl_bridge_children a LEFT JOIN mdtbl_bridge_xzsb b on a.bri_id = b.bri_id where b.is_cancel = 1";
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return (MdDataTableHelper<ChildBridge>.ConvertToModel(
                sql_helper.ExecuteDataTable(sql))).ToList<ChildBridge>();
        }

        /// <summary>
        /// 根据子桥梁ID或者桥梁名称进行查找
        /// </summary>
        /// <param name="bridgeChildrenId"></param>
        /// <param name="bridgeChildrenName"></param>
        /// <returns></returns>
        public static  List<ChildBridge> GetChildBridges(string name, string pileId)
        {
            // String sqlCon = "SELECT bridge_children_id,bri_id,type_id,bridge_children_name,bridge_children_discribe,bridge_pile FROM mdtbl_bridge_children  where 1=1";
            String sql = "SELECT bridge_children_id, a.bri_id,type_id,bridge_children_name, " +
                "bridge_children_discribe,bridge_pile FROM mdtbl_bridge_children a LEFT JOIN " +
                "mdtbl_bridge_xzsb b on a.bri_id = b.bri_id where b.is_cancel = 1 ";
            string sql_filter = "";
            //if (!string.IsNullOrWhiteSpace(id))
            //    sql_filter += string.Format("and bridge_children_id = '{0}'  ", id);
            if (!string.IsNullOrWhiteSpace(name))
                sql_filter += string.Format("and bridge_children_name like '%{0}%' ", name);
            if (!string.IsNullOrWhiteSpace(pileId))
                sql_filter += string.Format("and bridge_pile like '%{0}%' ", pileId);
            sql  +=  sql_filter;
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return (MdDataTableHelper<ChildBridge>.ConvertToModel(
                sql_helper.ExecuteDataTable(sql))).ToList<ChildBridge>();
        }
        /// <summary>
        /// 添加一个子桥梁
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        public static int AddChildBridge(ChildBridge child)
        {
            String sql = string.Format("INSERT INTO mdtbl_bridge_children(bridge_children_id,bri_id," +
                "type_id,bridge_children_name,bridge_children_discribe,bridge_pile) " +
                "VALUES('{0}','{1}','{2}','{3}','{4}', '{5}')",
                child.Id, child.ParentId, child.TypeId, child.Name, 
                child.Description, child.PileId);
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return sql_helper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改指定子桥梁信息
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        public static int UpdateChildBridge(ChildBridge child)
        {
            String sql = string.Format("UPDATE mdtbl_bridge_children  SET  bri_id ='{0}', type_id = '{1}' , bridge_children_name = '{2}', bridge_children_discribe = '{3}', bridge_pile = '{4}' WHERE bridge_children_id = '{5}'",
                 child.ParentId, child.TypeId, child.Name, child.Description, child.PileId, child.Id);
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return sql_helper.ExecuteNonQuery(sql);
        }

        public static bool DeleteChildBridge(string childBridgeId)
        {
            string sql = string.Format("delete mdtbl_bridge_children where " +
                "bridge_children_id='{0}'", childBridgeId);
            int count = SqlHelperFactory.GetDefaultSqlHelper().ExecuteNonQuery(sql);

            return count > 0;
        }
        #endregion 子桥梁管理部分

        public static List<BridgeType> GetBridgeTypes()
        {
            string sql = "select * from mdtbl_bridge_type";
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return (MdDataTableHelper<BridgeType>.ConvertToModel(
                sql_helper.ExecuteDataTable(sql))).ToList<BridgeType>();
        }
    }
}
