namespace HillarysHairCare.Models.DTOs;

public class AppointmentDTO
{
    public int Id { get; set; }
    public int StylistId { get; set; }
    public int CustomerId { get; set; }
    public DateTime Time { get; set; }
}