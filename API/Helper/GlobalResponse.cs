namespace API.Helper
{
    public class GlobalResponse<Result>
    {
        #region Constructors
        public GlobalResponse()
        {

        }

        #endregion

        #region Fields

        public string Message { get; set; }

        public Result Data { get; set; }

        #endregion
    }
}
