using Tailoring.Entities;

namespace Tailoring.Authentication;

public interface IJwtProvider
{
     Task <string> Generate(User user);

}