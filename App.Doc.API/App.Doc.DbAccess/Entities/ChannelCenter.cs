namespace App.Doc.DbAccess.Entities
{
    public class ChannelCenter
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
