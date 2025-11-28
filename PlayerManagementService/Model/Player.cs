namespace PlayerManagementService.Model
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public bool IsDrafted { get; set; } = false;
    }
}
