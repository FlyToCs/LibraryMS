using LibraryMS.Domain.Enums;

namespace LibraryMS.Application_Service.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRoleEnum Roll { get; set; }
    public bool Status { get; set; }
    public decimal PenaltyAmount { get; set; }
}