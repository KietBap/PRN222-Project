namespace PRN222.RoomBooking.AdminBlazor.ViewModels
{
    public class UserInfo
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public DateTime ExpiryTime { get; set; } 
        public bool IsExpired => DateTime.UtcNow > ExpiryTime;
    }
}
