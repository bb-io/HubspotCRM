namespace Apps.Hubspot.Crm.Models
{
    public class MultipleObjects<TEntity>
    {
        public IEnumerable<TEntity> Results { get; set; }
    }
}
