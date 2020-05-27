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
using System.Windows.Shapes;
using LogicLayerInterfaces;

namespace WpfPresentationLayer
{
	/// <summary>
	/// Interaction logic for frmUpdatePart.xaml
	/// </summary>
	public partial class frmUpdatePart : Window
	{
		private Part _part = null;
		private IPartManager _partManager;

		public frmUpdatePart(IPartManager partManager)
		{
			InitializeComponent();
			_partManager = partManager;
		}

		public frmUpdatePart(Part part, IPartManager partManager)
		{
			InitializeComponent();
			_part = part;
			_partManager = partManager;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			txtUpdatePartNumber.IsReadOnly = true;

			txtUpdatePartNumber.Text = _part.PartNumber.ToString();
			txtUpdatePartName.Text = _part.PartName.ToString();
			txtUpdatePartCost.Text = _part.Cost.ToString("C");
			txtUpdatePartDescription.Text = _part.Description.ToString();

			txtUpdatePartName.Focus();
			txtUpdatePartName.SelectAll();
		}// End Window_Loaded()

		private void BtnSubmitUpdatedPart_Click(object sender, RoutedEventArgs e)
		{
			if (txtUpdatePartName.Text == "")
			{
				MessageBox.Show("You must enter a Part Name.");
				txtUpdatePartName.Focus();
				return;
			}
			if (txtUpdatePartDescription.Text == "")
			{
				MessageBox.Show("You must enter a Description.");
				txtUpdatePartName.Focus();
				return;
			}
			if (txtUpdatePartCost.Text == "")
			{
				MessageBox.Show("You must enter a Cost.");
				txtUpdatePartName.Focus();
				return;
			}

			Part part = new Part()
			{
				PartNumber = txtUpdatePartNumber.Text.ToString(),
				PartName = txtUpdatePartName.Text.ToString(),
				Cost = decimal.Parse(txtUpdatePartCost.Text),
				Description = txtUpdatePartDescription.Text.ToString()
			};

			try
			{
				if (_partManager.EditPartInformation(part))
				{
					this.DialogResult = true;
					this.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
			}

		}// End BtnSubmitUpdatedPart_Click()

		private void BtnCanceltUpdatedPart_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}
	}// End frmUpdatePart class

}// End WpfPresentationLayer namespace
