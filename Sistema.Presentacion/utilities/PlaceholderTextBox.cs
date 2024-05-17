using System;
using System.Drawing;
using System.Windows.Forms;

public class PlaceholderTextBox
    {
    private TextBox _textBox;
    private string _placeholderText;
    private Color _placeholderColor;
    private Color _textColor;

    public PlaceholderTextBox(TextBox textBox, string placeholderText, Color placeholderColor, Color textColor)
        {
        _textBox = textBox;
        _placeholderText = placeholderText;
        _placeholderColor = placeholderColor;
        _textColor = textColor;

        _textBox.Text = _placeholderText;
        _textBox.ForeColor = _placeholderColor;

        _textBox.Enter += RemovePlaceholder;
        _textBox.Leave += SetPlaceholder;
        }

    private void RemovePlaceholder(object sender, EventArgs e)
        {
        if (_textBox.Text == _placeholderText)
            {
            _textBox.Text = "";
            _textBox.ForeColor = _textColor;
            }
        }

    private void SetPlaceholder(object sender, EventArgs e)
        {
        if (string.IsNullOrWhiteSpace(_textBox.Text))
            {
            _textBox.Text = _placeholderText;
            _textBox.ForeColor = _placeholderColor;
            }
        }

    public void Apply()
        {
        SetPlaceholder(null, null);
        }
    }
