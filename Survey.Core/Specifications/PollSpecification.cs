

namespace Survey.Core.Specifications
{
    public class PollSpecification: BaseSpecification<Poll>
    {
        public PollSpecification():base()
        {
            Includes = [];
        }

        public PollSpecification(Expression<Func<Poll, bool>> predicate):base(predicate) 
        {
            Predicate = predicate;
            Includes = [];
        }
    }
}
