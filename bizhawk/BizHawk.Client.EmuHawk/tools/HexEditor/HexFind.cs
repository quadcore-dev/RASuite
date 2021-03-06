﻿using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BizHawk.Client.EmuHawk
{
	public partial class HexFind : Form
	{
		private Point _location;

		public HexFind()
		{
			InitializeComponent();
			ChangeCasing();
		}

		public void SetInitialValue(string value)
		{
			FindBox.Text = value;
		}

		public void SetLocation(Point p)
		{
			_location = p;
		}

		private void HexFind_Load(object sender, EventArgs e)
		{
			if (_location.X > 0 && _location.Y > 0)
			{
				Location = _location;
			}
		}

		private string GetFindBoxChars()
		{
			if (string.IsNullOrWhiteSpace(FindBox.Text))
			{
				return string.Empty;
			}
			
			if (HexRadio.Checked)
			{
				return FindBox.Text;
			}
			
			
			var bytes = GlobalWin.Tools.HexEditor.ConvertTextToBytes(FindBox.Text);

			var bytestring = new StringBuilder();
			foreach (var b in bytes)
			{
				bytestring.Append(string.Format("{0:X2}", b));
			}

			return bytestring.ToString();
		}

		private void Find_Prev_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.HexEditor.FindPrev(GetFindBoxChars(), false);
		}

		private void Find_Next_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.HexEditor.FindNext(GetFindBoxChars(), false);
		}

		private void ChangeCasing()
		{
			var text = FindBox.Text;
			var location = FindBox.Location;
			var size = FindBox.Size;

			Controls.Remove(FindBox);
			if (HexRadio.Checked)
			{
				FindBox = new HexTextBox { CharacterCasing = CharacterCasing.Upper };
				(FindBox as HexTextBox).Nullable = true;
			}
			else
			{
				FindBox = new TextBox { CharacterCasing = CharacterCasing.Normal };
			}

			FindBox.Text = text;
			FindBox.Size = size;
			Controls.Add(FindBox);
			FindBox.Location = location;
		}

		private void HexRadio_CheckedChanged(object sender, EventArgs e)
		{
			ChangeCasing();
		}

		private void TextRadio_CheckedChanged(object sender, EventArgs e)
		{
			ChangeCasing();
		}

		private void FindBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				GlobalWin.Tools.HexEditor.FindNext(GetFindBoxChars(), false);
			}
		}
	}
}
