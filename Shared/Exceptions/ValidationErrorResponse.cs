namespace Shared.Exceptions
{
    public class ValidationErrorResponse : Exception
    {
        #region Constructors

        public ValidationErrorResponse()
        {
            Errors = new List<string>();
        }

        #endregion

        #region Members

        public IEnumerable<string> Errors { get; set; }

        #endregion
    }
}
