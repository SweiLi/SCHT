using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 项目信息表
    /// </summary>
    public class Project
    {
        //项目ID
        public string Id { get; set; }
        ////客户ID
        public string ClientId { get; set; }
        //项目名称
        public string Name { get; set; }
        //起始时间
        public DateTime BeginTime { get; set; }
        //终止时间
        public DateTime EndTime { get; set; }
        //项目描述
        public string Description { get; set; }
        //项目对应合同号
        public string ContractNumber { get; set; }
        ////项目新建人
        public string Creator { get; set; }
        //项目状态
        public int Status { get; set; }
        //审核人
        public string CheckMan { get; set; }
        //审核时间
        public DateTime CheckDate { get; set; }
        //项目负责人
        public string Principal { get; set; }
        //签订日期
        public DateTime SignedDate { get; set; }
        //项目地点
        public string Address { get; set; }

        //项目和客户联系，一个项目对应一个客户信息
        public BridgeClient Client { get; set; }



    }
}
