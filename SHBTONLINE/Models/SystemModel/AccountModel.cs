using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Models.SystemModel
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "账号")]
        public string LoginName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住登录状态")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "账号名字不符合规则啊兄弟。", MinimumLength = 2)]
        [Display(Name = "账号")]

        public string LoginName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "这并不是你的名字吧。", MinimumLength = 2)]
        [Display(Name = "真实姓名")]
        public string userName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Display(Name = "绝地求生ID")]
        public string PubgID { get; set; }
        [Display(Name = "刀塔2数字ID")]
        public string DOTA2ID { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
    public class UserLoginInfo
    {
        public string LoginName { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string MateName { get; set; }
        public string MateLoginName { get; set; }
        public string Key { get; set; }
    }
    /// <summary>
    /// 个人信息页通用信息
    /// </summary>
    public class EditUserInfo
    {
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Display(Name = "账号")]

        public string LoginName { get; set; }

        [Display(Name = "昵称")]
        public string userName { get; set; }
        [Display(Name = "另一半")]
        public string MateName { get; set; }
        [Display(Name = "密钥")]
        public string Key { get; set; }
        [Display(Name = "绝地求生ID")]
        public string PubgID { get; set; }
        [Display(Name = "刀塔2数字ID")]
        public string DOTA2ID { get; set; }
        public string IMG { get; set; }
    }
    /// <summary>
    /// 修改密码页面
    /// </summary>
    public class ModiftPassword
    {

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string Used { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string New { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("New", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// 社交
    /// </summary>
    public class Social
    {
        [Required]
        [Display(Name = "对方的用户名或名字")]
        public string Mate { get; set; }
        [Required]
        [StringLength(36, ErrorMessage = "{0} 必须只能包含 {2} 个字符。", MinimumLength = 36)]
        [Display(Name = "对方的密钥")]
        public string Key { get; set; }
    }
}