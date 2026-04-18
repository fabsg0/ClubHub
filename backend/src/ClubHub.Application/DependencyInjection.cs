using ClubHub.Application.Interfaces;
using ClubHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClubHub.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddScoped<IMemberService, MemberService>();

		return services;
	}
}