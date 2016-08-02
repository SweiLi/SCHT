using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 项目组员表
    /// </summary>
    class Emp
    {

        //项目组员ID
        public string PrjUserId { get; set; }
        ////项目ID
        public string PrjId { get; set; }
        //所担任职位
        public string UserPosition { get; set; }
        //任务描述
        public string JobDescribe { get; set; }
        ////担任职位起始时间
        public DateTime Job_BeginTime { get; set; }
        //担任职位终止时间
        public DateTime JobEndTime { get; set; }

        //组员状态
        public int JobStatus { get; set; }
        ////组员姓名
        public string JobEmpName { get; set; }
        //用户ID
        public string JobUserId { get; set; }
    }
}
