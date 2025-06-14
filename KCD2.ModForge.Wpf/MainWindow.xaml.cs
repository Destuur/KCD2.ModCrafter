﻿using KCD2.ModForge.Shared;
using KCD2.ModForge.Shared.Services;
using KCD2.ModForge.Wpf.Services;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using System.Windows.Input;

namespace KCD2.ModForge.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddWpfBlazorWebView();
			serviceCollection.AddMudServices()
							 .AddModForgeServices()
							 .AddModForgeAdapters()
							 .AddSingleton<IFolderPickerService, FolderPickerService>();

#if DEBUG
			serviceCollection.AddBlazorWebViewDeveloperTools();
#endif

			var serviceProvider = serviceCollection.BuildServiceProvider();
			Resources.Add("services", serviceProvider);

			InitializeComponent();
		}

		#region WPF Window Methods
		private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				if (e.ClickCount == 2)
				{
					// Doppelklick: Maximieren oder wiederherstellen
					this.WindowState = this.WindowState == WindowState.Maximized
						? WindowState.Normal
						: WindowState.Maximized;
				}
				else
				{
					// Einfaches DragMove für Fenster verschieben
					this.DragMove();
				}
			}
		}

		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = this.WindowState == WindowState.Maximized
				? WindowState.Normal
				: WindowState.Maximized;
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		#endregion
	}
}