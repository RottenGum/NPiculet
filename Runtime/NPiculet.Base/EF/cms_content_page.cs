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
    
    public partial class cms_content_page
    {
        public int Id { get; set; }
        public string GroupCode { get; set; }
        public Nullable<int> OrgId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public string Thumb { get; set; }
        public string Source { get; set; }
        public int Click { get; set; }
        public int IsEnabled { get; set; }
        public string Author { get; set; }
        public Nullable<decimal> Point { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
