namespace PerformanceTrackingService.Model
{
    public class PlayerStat
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }     // Links to Player service
        public DateTime GameDate { get; set; } = DateTime.UtcNow;

        // Passing stats
        public int PassingYards { get; set; }
        public int PassingTouchdowns { get; set; }
        public int InterceptionsThrown { get; set; }

        // Rushing stats
        public int RushingYards { get; set; }
        public int RushingTouchdowns { get; set; }

        // Receiving stats
        public int Receptions { get; set; }
        public int ReceivingYards { get; set; }
        public int ReceivingTouchdowns { get; set; }

        // Turnovers
        public int FumblesLost { get; set; }

        // Fantasy Points
        public int FantasyPoints {  get; set; }
    }
}
