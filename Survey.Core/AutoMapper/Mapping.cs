namespace Survey.Core.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Poll,PollResponse>().ReverseMap();
            CreateMap<Question,QuestionResponse>().ReverseMap();

            CreateMap<CreatePollRequest,Poll>().ReverseMap();

            CreateMap<QuestionRequest, Question>()
                      .ForMember(dest => dest.Answers, options => options.MapFrom(src =>
                                         src.answers.Select(answer => new Answer { Content = answer })));

            CreateMap<Question, QuestionResponse>()
                      .ForMember(des => des.answers, options => options.MapFrom(src => 
                                        src.Answers.Select(answer => new AnswerResponse { Id = answer.Id ,Content = answer.Content})));
        
            CreateMap<UserProfileResponse,ApplicationUser>().ReverseMap();
            CreateMap<ApplicationRole, RoleResponse>().ReverseMap();
            CreateMap<ApplicationRole, RoleResponseDetail>().ReverseMap();
        }
    }
}
