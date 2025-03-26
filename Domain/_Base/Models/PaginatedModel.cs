namespace Domain._Base.Models
{
    public record PaginatedModel<TResult>(int PageNumber, int PageSize, int TotalCount, IList<TResult> Result);
}
