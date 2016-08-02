using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁档案资料信息
    /// </summary>
    class BridgeDazl
    {
        public string BriDazlId { get; set; }
        public string BriId { get; set; }
        public string BriDazlDesigndraw { get; set; }
        public string BriDazlDesignfile { get; set; }
        public string BriDazlAcceptancefile { get; set; }

        public string BriDazlExecfile { get; set; }
        public string BriDazlSuccfile { get; set; }
        public string BriDazlAdminfile { get; set; }
        public string BriDazlRegulardoc { get; set; }

        public string BriDazlSpecialdoc { get; set; }
        public string BriDazlRepairdoc { get; set; }
        public string BriDazlArchiveno { get; set; }
        public string BriDazlArchives { get; set; }
        public DateTime BriDazlBuilddate { get; set; }

    }
}
