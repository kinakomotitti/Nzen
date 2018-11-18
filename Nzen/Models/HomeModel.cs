namespace Nzen.Models
{
    #region using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion

    public class HomeModel
    {
        public string UserName { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
