namespace Nzen.Models
{
    #region using
    using Nzen.Manager;
    #endregion

    public class HomeModel
    {
        public string UserName { get; set; } = string.Empty;

        public string GroupId { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ErrorMesage { get; set; } = string.Empty;

        public ApplicationEnv Env { get; set; }
    }
}
