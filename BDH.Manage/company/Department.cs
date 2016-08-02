namespace BDH.Manage
{
    /// <summary>
    /// 部门表
    /// </summary>
    public class Department
    {
        //部门编号
        public string DId { get; set; }
        //公司编号
        public string ComId { get; set; }
        //部门名称
        public string DName { get; set; }
        //部门简介
        public string DIntroduce { get; set; }
        //部门邮箱
        public string DMail { get; set; }
        //部门联系人
        public string DContact { get; set; }
        //部门负责人
        public string DHeader { get; set; }
        //部门负责人电话
        public string DDphone { get; set; }
        //部门联系人电话
        public string DCphone { get; set; }
        //部门联系电话
        public string DTel { get; set; }
        //部门常用邮寄地址
        public string DAddress { get; set; }
        //部门的邮编
        public string DPostcode { get; set; }
        //部门状态
        public int DStatus { get; set; }

    }
}
