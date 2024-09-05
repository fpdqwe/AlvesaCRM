﻿using DesktopUI.Utilities;
using System.Windows;

namespace DesktopUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			AuthService.InitRepositories();
			InitializeComponent();
		}
	}
}