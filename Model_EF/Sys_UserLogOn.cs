//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model_EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sys_UserLogOn
    {
        public string F_Id { get; set; }
        public string F_UserId { get; set; }
        public string F_UserPassword { get; set; }
        public string F_UserSecretkey { get; set; }
        public Nullable<System.DateTime> F_AllowStartTime { get; set; }
        public Nullable<System.DateTime> F_AllowEndTime { get; set; }
        public Nullable<System.DateTime> F_LockStartDate { get; set; }
        public Nullable<System.DateTime> F_LockEndDate { get; set; }
        public Nullable<System.DateTime> F_FirstVisitTime { get; set; }
        public Nullable<System.DateTime> F_PreviousVisitTime { get; set; }
        public Nullable<System.DateTime> F_LastVisitTime { get; set; }
        public Nullable<System.DateTime> F_ChangePasswordDate { get; set; }
        public Nullable<bool> F_MultiUserLogin { get; set; }
        public Nullable<int> F_LogOnCount { get; set; }
        public Nullable<bool> F_UserOnLine { get; set; }
        public string F_Question { get; set; }
        public string F_AnswerQuestion { get; set; }
        public Nullable<bool> F_CheckIPAddress { get; set; }
        public string F_Language { get; set; }
        public string F_Theme { get; set; }
    }
}
