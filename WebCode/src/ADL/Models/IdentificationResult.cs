namespace ADL.Models
{
    public class IdentificationResult
    {
        public IdentificationResult(bool successful, string username)
        {
            Successful = successful;
            Username = username;
        }
       public bool Successful { get; set; }
       public string Username { get; set; }
    }
}