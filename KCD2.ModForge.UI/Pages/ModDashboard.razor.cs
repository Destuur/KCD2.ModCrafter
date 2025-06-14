﻿using KCD2.ModForge.Shared.Models.Mods;
using KCD2.ModForge.Shared.Services;
using KCD2.ModForge.UI.Components.DialogComponents;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace KCD2.ModForge.UI.Pages
{
	public partial class ModDashboard
	{
		[Inject]
		public NavigationManager Navigation { get; set; }
		[Inject]
		public UserConfigurationService UserConfigurationService { get; set; }
		[Inject]
		public IDialogService DialogService { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

			if (string.IsNullOrEmpty(UserConfigurationService.Current.GameDirectory))
			{
				var parameters = new DialogParameters<MoreModItemsDialog>
				{
					{ x => x.ButtonText, "Bring me the sacred data." }
				};

				var options = new DialogOptions() { CloseButton = false, MaxWidth = MaxWidth.ExtraSmall, BackdropClick = false, BackgroundClass = "entry-dialog", CloseOnEscapeKey = false, FullWidth = true };

				var dialog = await DialogService.ShowAsync<EntryDialog>("A Peasant With No Pitchfork entered", parameters, options);
				var result = await dialog.Result;

				if (result.Canceled == false)
				{
					Navigation.NavigateTo("/settings");
				}
			}
		}
	}
}
