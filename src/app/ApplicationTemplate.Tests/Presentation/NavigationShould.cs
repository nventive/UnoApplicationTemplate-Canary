﻿using System.Threading.Tasks;
using ApplicationTemplate.Presentation;
using Xunit;

namespace ApplicationTemplate.Tests;

public sealed partial class NavigationShould : NavigationTestsBase
{
	[Fact]
	public async Task NavigateToDifferentMenuSections()
	{
		// Arrange
		MenuViewModel vmBuilder = new MenuViewModel();

		await AssertNavigateFromTo<LoginPageViewModel, DadJokesPageViewModel>(() => new LoginPageViewModel(isFirstLogin: false), p => p.NavigateToHome);

		await AssertNavigateTo<PostsPageViewModel>(() => vmBuilder.ShowPostsSection);

		await AssertNavigateTo<SettingsPageViewModel>(() => vmBuilder.ShowSettingsSection);

		await AssertNavigateTo<DadJokesPageViewModel>(() => vmBuilder.ShowHomeSection);
	}

	[Fact]
	public async Task NavigateFromOnboardingToLoginPage()
	{
		await AssertNavigateFromTo<OnboardingPageViewModel, LoginPageViewModel>(() => new OnboardingPageViewModel(), p => p.NavigateToNextPage);
	}

	[Fact]
	public async Task NavigateFromSettingsToLoginPage()
	{
		// Arrange
		var sourceSection = "Settings";

		// Act
		var currentSection = await AssertSetActiceSection<SettingsPageViewModel, LoginPageViewModel>(() => new SettingsPageViewModel(), p => p.Logout, sourceSection);

		// Assert
		Assert.NotEqual(sourceSection, currentSection);
	}

	[Fact]
	public async Task NavigateToDiagnosticsPageAndBack()
	{
		var diagnosticsViewModel = await AssertNavigateFromTo<SettingsPageViewModel, DiagnosticsPageViewModel>(
			() => new SettingsPageViewModel(),
			p => p.NavigateToDiagnosticsPage
		);

		await AssertNavigateTo<SettingsPageViewModel>(() => diagnosticsViewModel.NavigateBack);
	}
}
