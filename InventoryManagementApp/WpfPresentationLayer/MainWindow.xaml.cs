using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayerInterfaces;

namespace WpfPresentationLayer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private IUserManager _userManager;
		private User _user = null;
		private IPartManager _partManager;


		public MainWindow()
		{
			InitializeComponent();
			_userManager = new UserManager();
			_partManager = new PartManager();
		}

		private void hideAllTabs()
		{
			foreach (TabItem item in tabsetMain.Items)
			{
				item.Visibility = Visibility.Collapsed;
			}
		}// End hideAllTabs()

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			hideAllTabs();
		}// End Window_Loaded()

		private void showUserTabs()
		{
			foreach (var role in _user.Roles)
			{
				switch (role)
				{
					case "Manager":
						tabLocate.Visibility = Visibility.Visible;
						tabNewPart.Visibility = Visibility.Visible;
						tabRecieve.Visibility = Visibility.Visible;
						tabInformation.Visibility = Visibility.Visible;
						break;
					case "Buyer":
						tabNewPart.Visibility = Visibility.Visible;
						tabInformation.Visibility = Visibility.Visible;
						break;
					case "MaterialHandler":
						tabLocate.Visibility = Visibility.Visible;
						break;
					case "RecievingClerk":
						tabRecieve.Visibility = Visibility.Visible;
						break;
					default:
						break;
				}
			}
		}// End showUserTabs()

		private void DgPartList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Part part = (Part)dgPartList.SelectedItem;
			var moveScreen = new frmMoveScreen(part, _partManager);
			if (moveScreen.ShowDialog() == true)
			{
				populatePartList();
			}

		}// End DgPartList_MouseDoubleClick()

		private void populatePartList()
		{
			try
			{
				dgPartList.ItemsSource = _partManager.RetrieveAllParts();
				dgPartList.Columns.Remove(dgPartList.Columns[6]);
				dgPartList.Columns.Remove(dgPartList.Columns[5]);
				dgPartList.Columns.Remove(dgPartList.Columns[4]);
				dgPartList.Columns[0].Header = "Part Number";
				dgPartList.Columns[1].Header = "Location";
				dgPartList.Columns[2].Header = "Description";
				dgPartList.Columns[3].Header = "Quantity";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}
		}// End populatePartList()

		private void partInformationList()
		{
			try
			{
				dgAllPartList.ItemsSource = _partManager.RetrieveAllPartInformation();
				dgAllPartList.Columns.RemoveAt(1);
				dgAllPartList.Columns.RemoveAt(2);
				dgAllPartList.Columns.RemoveAt(4);
				dgAllPartList.Columns[0].Header = "Part Number";
				dgAllPartList.Columns[1].Header = "Part Name";
				dgAllPartList.Columns[2].Header = "Cost";
				dgAllPartList.Columns[3].Header = "Description";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}
		}// End partInformationList()

		private void DgAllPartList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Part part = (Part)dgAllPartList.SelectedItem;
			var updateScreen = new frmUpdatePart(part, _partManager);
			if (updateScreen.ShowDialog() == true)
			{
				partInformationList();
			}
		}// End DgAllPartList_MouseDoubleClick()

		//All Button Methods Below Here
		private void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			var email = txtEmail.Text;
			var password = pwdPassword.Password;

			if (btnLogin.Content.ToString() == "Logout")
			{
				_user = null;
				txtEmail.Text = "@mwb.com";
				pwdPassword.Password = "";
				txtEmail.Visibility = Visibility.Visible;
				pwdPassword.Visibility = Visibility.Visible;
				btnLogin.Content = "Login";
				lblUsername.Visibility = Visibility.Visible;
				lblPassword.Visibility = Visibility.Visible;
				lblStatusMessage.Content = "You Must Login to Continue.";
				hideAllTabs();
				gridLocate.Visibility = Visibility.Hidden;
				gridNewPart.Visibility = Visibility.Hidden;
				gridInformation.Visibility = Visibility.Hidden;
				gridRecieve.Visibility = Visibility.Hidden;

				return;
			}

			if (email.Length < 7 || password.Length < 7)
			{
				MessageBox.Show("Invalid Email or Password",
					"Invalid Login.", MessageBoxButton.OK,
					MessageBoxImage.Exclamation);
				txtEmail.Text = "";
				pwdPassword.Password = "";
				txtEmail.Focus();

				return;
			}

			try
			{
				_user = _userManager.AuthenticateUser(email, password);

				string roles = "";
				for (int i = 0; i < _user.Roles.Count; i++)
				{
					roles += _user.Roles[i];
					if (i < _user.Roles.Count - 1)
					{
						roles += ", ";
					}
				}

				lblStatusMessage.Content = "Hello, " + _user.FirstName + ".  You are logged in as: " + roles;

				if (pwdPassword.Password.ToString() == "newuser")
				{
					var resetPassword = new frmUpdatePassword(_user, _userManager);
					if (resetPassword.ShowDialog() == true)
					{

					}
					else
					{

					}
				}

				txtEmail.Visibility = Visibility.Hidden;
				pwdPassword.Visibility = Visibility.Hidden;
				btnLogin.Content = "Logout";
				lblUsername.Visibility = Visibility.Hidden;
				lblPassword.Visibility = Visibility.Hidden;
				showUserTabs();
				gridLocate.Visibility = Visibility.Visible;
				gridNewPart.Visibility = Visibility.Visible;
				gridInformation.Visibility = Visibility.Visible;
				gridRecieve.Visibility = Visibility.Visible;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n"
					+ ex.InnerException.Message,
					"Login Failed!",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}// End BtnLogin_Click()

		private void BtnFindPart_Click(object sender, RoutedEventArgs e)
		{
			var partNumber = txtPartNumber.Text;

			if (partNumber == "" || partNumber.Length < 8)
			{
				MessageBox.Show("Please Enter a Valid Part Number",
					"Invalid Part Number",
					MessageBoxButton.OK,
					MessageBoxImage.Exclamation);
				txtPartNumber.Text = "";
				txtPartNumber.Focus();
				return;
			}
			try
			{
				dgPartList.ItemsSource = _partManager.RetrievePartByPartNumber(partNumber);
				dgPartList.Columns.Remove(dgPartList.Columns[6]);
				dgPartList.Columns.Remove(dgPartList.Columns[5]);
				dgPartList.Columns.Remove(dgPartList.Columns[4]);
				dgPartList.Columns[0].Header = "Part Number";
				dgPartList.Columns[1].Header = "Location";
				dgPartList.Columns[2].Header = "Description";
				dgPartList.Columns[3].Header = "Quantity";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}

		}// End BtnFindPart_Click()

		private void BtnAllParts_Click(object sender, RoutedEventArgs e)
		{
			populatePartList();
		}// End BtnAllParts_Click()

		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (txtAddPartNumber.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a valid Part Number");
				txtAddPartNumber.Focus();
				return;
			}
			if (txtAddPartQuantity.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a Quantity");
				txtAddPartQuantity.Focus();
				return;
			}
			if (txtAddPartLocation.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a valid Location");
				txtAddPartLocation.Focus();
				return;
			}

			Part part = new Part()
			{
				PartNumber = txtAddPartNumber.Text.ToString(),
				Quantity = Convert.ToInt32(txtAddPartQuantity.Text),
				Location = txtAddPartLocation.Text.ToString()
			};

			try
			{
				_partManager.AddPart(part);
				MessageBox.Show("Added " + txtAddPartNumber.Text + " pieces of " 
					+ txtAddPartNumber.Text + " to Location " + txtAddPartLocation.Text);
				txtAddPartNumber.Text = "";
				txtAddPartQuantity.Text = "";
				txtAddPartLocation.Text = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}
		}// EndBtnAdd_Click()

		private void BtnCancelPartAdd_Click(object sender, RoutedEventArgs e)
		{
			txtAddPartNumber.Text = "";
			txtAddPartQuantity.Text = "";
			txtAddPartLocation.Text = "";
		}// End BtnCancelPartAdd_Click()

		private void BtnSubmitNewPart_Click(object sender, RoutedEventArgs e)
		{
			if (txtNewPartNumber.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a Part Number");
				txtAddPartNumber.Focus();
				return;
			}
			if (txtNewPartName.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a Part Name");
				txtAddPartQuantity.Focus();
				return;
			}
			if (txtNewPartCost.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a Cost of the new Part");
				txtAddPartLocation.Focus();
				return;
			}
			if (txtNewPartDescription.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a Description of the new Part");
				txtAddPartLocation.Focus();
				return;
			}

			Part part = new Part()
			{
				PartNumber = txtNewPartNumber.Text.ToString(),
				PartName = txtNewPartName.Text.ToString(),
				Cost = Convert.ToDecimal(txtNewPartCost.Text),
				Description = txtNewPartDescription.Text.ToString()
			};

			try
			{
				_partManager.AddNewPart(part);
				MessageBox.Show("Part Added to the System");
				txtNewPartNumber.Text = "";
				txtNewPartName.Text = "";
				txtNewPartCost.Text = "";
				txtNewPartDescription.Text = "";
				txtNewPartNumber.Focus();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}

		}// End BtnSubmitNewPart_Click()

		private void BtnCanceltNewPart_Click(object sender, RoutedEventArgs e)
		{
			txtNewPartNumber.Text = "";
			txtNewPartName.Text = "";
			txtNewPartCost.Text = "";
			txtNewPartDescription.Text = "";
		}// End BtnCanceltNewPart_Click()


		// All the tab_GotFocus methods
		private void TabLocate_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dgPartList.ItemsSource == null)
				{
					populatePartList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.InnerException.Message);
			}
		}// End TabLocate_GotFocus()
		private void TabRecieve_GotFocus(object sender, RoutedEventArgs e)
		{

		}// TabRecieve_GotFocus()
		private void TabOrder_GotFocus(object sender, RoutedEventArgs e)
		{
			tabNewPart.Visibility = Visibility.Visible;
			txtNewPartNumber.Focus();
		}// End TabOrder_GotFocus()
		private void TabNewPart_GotFocus(object sender, RoutedEventArgs e)
		{
			tabNewPart.Visibility = Visibility.Visible;
		}// End TabNewPart_GotFocus()
		private void TabInformation_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				if (dgAllPartList.ItemsSource == null)
				{
					partInformationList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}
		}// End TabInformation_GotFocus()


		// Below Here are the methods to Highlight Text when Tabbed in Different Panels
		private void TxtAddPartNumber_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (e.KeyboardDevice.IsKeyDown(Key.Tab))
			{
				((TextBox)sender).SelectAll();
			}
		}
		private void TxtAddPartQuantity_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (e.KeyboardDevice.IsKeyDown(Key.Tab))
			{
				((TextBox)sender).SelectAll();
			}
		}
		private void TxtAddPartLocation_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (e.KeyboardDevice.IsKeyDown(Key.Tab))
			{
				((TextBox)sender).SelectAll();
			}
		}
		private void PwdPassword_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (e.KeyboardDevice.IsKeyDown(Key.Tab))
			{
				((PasswordBox)sender).SelectAll();
			}
		}
		private void TxtNewPartDescription_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (e.KeyboardDevice.IsKeyDown(Key.Tab))
			{
				((TextBox)sender).SelectAll();
			}
		}

		private void BtnFind_Click(object sender, RoutedEventArgs e)
		{
			var partNumber = txtAllPartNumber.Text;

			if (partNumber == "" || partNumber.Length < 8)
			{
				MessageBox.Show("Please Enter a Valid Part Number",
					"Invalid Part Number",
					MessageBoxButton.OK,
					MessageBoxImage.Exclamation);
				txtPartNumber.Text = "";
				txtPartNumber.Focus();
				return;
			}
			try
			{
				dgAllPartList.ItemsSource = _partManager.RetrievePartByPartNumber(partNumber);
				dgAllPartList.Columns.RemoveAt(6);
				dgAllPartList.Columns.RemoveAt(5);
				dgAllPartList.Columns.RemoveAt(4);
				dgAllPartList.Columns[0].Header = "Part Number";
				dgAllPartList.Columns[1].Header = "Part Name";
				dgAllPartList.Columns[2].Header = "Cost";
				dgAllPartList.Columns[3].Header = "Description";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}
		}

		private void BtnParts_Click(object sender, RoutedEventArgs e)
		{
			partInformationList();
		}
	}// End MainWindow Class

}// End WpfPresentationLayer namespace
