

namespace Survey.Core.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Poll,PollResponse>().ReverseMap();

            CreateMap<CreatePollRequest,Poll>().ReverseMap();
        }
    }
}
