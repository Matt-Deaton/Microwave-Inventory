using DataObjects;
using LogicLayer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicLayerInterfaces;

namespace WpfPresentationLayer
{
	/// <summary>
	/// Interaction logic for frmMoveScreen.xaml
	/// </summary>
	public partial class frmMoveScreen : Window
	{
		private Part _part = null;
		private IPartManager _partManager = null;
		private bool _moveMode = true;

		public frmMoveScreen()
		{
			InitializeComponent();
		}

		public frmMoveScreen(Part part, IPartManager partManager)
		{
			InitializeComponent(); 

			_part = part;
			_partManager = partManager;
			_moveMode = false;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			txtPartNumber.IsReadOnly = true;
			txtPartName.IsReadOnly = true;
			txtLocationFrom.IsReadOnly = true;
			txtQuantityAvaiable.IsReadOnly = true;

			if (_moveMode == false)
			{
				txtPartNumber.Text =  _part.PartNumber.ToString();
				txtPartName.Text = _part.PartName.ToString();
				txtLocationFrom.Text = _part.Location.ToString();
				txtQuantityAvaiable.Text = _part.Quantity.ToString();
				txtQuantityMoving.Focus();
				txtNewLocation.Text = "001A";
			}
			else
			{
				txtQuantityMoving.Focus();
			}
		}// End Window_Loaded()

		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}// End BtnCancel_Click()

		private void BtnConfirm_Click(object sender, RoutedEventArgs e)
		{
			if (txtQuantityMoving.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a Quantity.");
				txtQuantityMoving.Focus();
				return;
			}
			if (txtNewLocation.Text.ToString() == "")
			{
				MessageBox.Show("You must enter a Location");
				txtNewLocation.Focus();
				return;
			}
			if (int.Parse(txtQuantityMoving.Text) > _part.Quantity)
			{
				MessageBox.Show("Pieces moving cannot exceed pieces avaiable.");
				txtQuantityMoving.Focus();
				txtQuantityMoving.SelectAll();
				return;
			}

			string oldLocation = txtLocationFrom.Text;
			string newLocation = txtNewLocation.Text;
			int originalQuantity = int.Parse(txtQuantityAvaiable.Text);
			int movingQuantity = int.Parse(txtQuantityMoving.Text);


			Part part = new Part() {
				PartNumber = txtPartNumber.Text.ToString(),
				PartName = txtPartName.Text.ToString(),
				Location = txtNewLocation.Text.ToString(),
				Quantity = int.Parse(txtQuantityMoving.Text)
			};

			try
			{
				if (_partManager.MovePart(_part.PartNumber, oldLocation, newLocation, originalQuantity, movingQuantity))
				{
					originalQuantity = originalQuantity - movingQuantity;
					MessageBox.Show("Moved " + movingQuantity + " pieces of " + txtPartNumber.Text.ToString() + " to " + newLocation);
					this.DialogResult = true;
					this.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}

		}// End BtnConfirm_Click()

		private void TxtQuantityMoving_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
		{
			if (e.KeyboardDevice.IsKeyDown(Key.Tab))
			{
				((TextBox)sender).SelectAll();
			}
		}

		private void TxtNewLocation_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (e.KeyboardDevice.IsKeyDown(Key.Tab))
			{
				((TextBox)sender).SelectAll();
			}
		}
	}// End frmMoveScreen class

}// End WpfPresentationLayer namespace
