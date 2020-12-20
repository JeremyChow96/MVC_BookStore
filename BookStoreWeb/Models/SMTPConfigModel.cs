namespace BookStoreWeb.Models
{
    public class SMTPConfigModel
    {
        public string SenderAddress { get; set; }

        public string SenderDisplayName { get; set; }

        public string UserName { get; set; }

        /// <summary>
        ///  以163邮箱举例 密码为开通SMTP时的授权码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///  邮件服务器smtp.163.com表示网易邮箱服务器
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }

        public bool EnableSSL { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public bool IsBodyHtml { get; set; }
    }
}