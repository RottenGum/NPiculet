//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NPiculet.Base.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class cms_points_log
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public Nullable<int> ActionUserId { get; set; }
        public Nullable<int> ActionOrgId { get; set; }
        public string TargetCode { get; set; }
        public string TargetId { get; set; }
        public string IP { get; set; }
        public string Tag { get; set; }
        public Nullable<int> Point { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Creator { get; set; }
    }
}