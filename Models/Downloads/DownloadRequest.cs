namespace Models.Downloads
{
    public class DownloadRequest
    {
        public IEnumerable<DownloadRequestThreadDetails> Threads { get; set; }
    }
}
