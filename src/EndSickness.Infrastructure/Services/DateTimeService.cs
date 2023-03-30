using EndSickness.Application.Common.Interfaces;

namespace EndSickness.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
