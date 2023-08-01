namespace Apps.Hubspot.Crm.Models.Pagination
{
    public class MultipleObjects<TEntity>
    {
        public Paging? Paging { get; set; }
        public IEnumerable<TEntity> Results { get; set; }
    }
}
