using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using StylizedComponents.Core.models;

[ToolboxItem(false)]
public class IconRelationPicker : Control
{
    public event EventHandler ValueSelected;

    public IconAlignment SelectedValue;

    private Rectangle _left;
    private Rectangle _right;
    private Rectangle _top;
    private Rectangle _bottom;

    private int _rectSize { get => 30; }
    private int _xCenter { get => (Size.Width - _rectSize) / 2; }
    private int _yCenter { get => (Size.Height - _rectSize) / 2; }

    public IconRelationPicker()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.UserPaint, true);

        Size = new Size(135, 90);

        _left = new Rectangle(_xCenter - _rectSize, _yCenter, _rectSize, _rectSize);
        _right = new Rectangle(_xCenter + _rectSize, _yCenter, _rectSize, _rectSize);
        _top = new Rectangle(_xCenter, 0, _rectSize, _rectSize);
        _bottom = new Rectangle(_xCenter, Size.Height - _rectSize, _rectSize, _rectSize - 1);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        DrawCell(e.Graphics, _left);
        DrawCell(e.Graphics, _right);
        DrawCell(e.Graphics, _top);
        DrawCell(e.Graphics, _bottom);

        DrawSelected(e.Graphics);
        DrawCross(e.Graphics);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        base.OnPaintBackground(pevent);
        pevent.Graphics.Clear(SystemColors.Control);
    }

    private void DrawCell(Graphics g, Rectangle r)
    {
        SolidBrush b = new SolidBrush(Color.White);
        g.FillRectangle(b, r);
        b.Dispose();

        g.DrawRectangle(Pens.Gray, r);
    }

    private void DrawSelected(Graphics g)
    {
        Rectangle r = Rectangle.Empty;

        if (SelectedValue == IconAlignment.Left)
            r = _left;
        else if (SelectedValue == IconAlignment.Right)
            r = _right;
        else if (SelectedValue == IconAlignment.Top)
            r = _top;
        else if (SelectedValue == IconAlignment.Bottom)
            r = _bottom;

        if (!r.IsEmpty)
        {
            SolidBrush b = new SolidBrush(Color.FromArgb(70, Color.DodgerBlue));
            g.FillRectangle(b, r);
            b.Dispose();
        }
    }

    private void DrawCross(Graphics g)
    {
        Pen pen = new Pen(Color.LightGray, 1);

        g.DrawLine(pen, _xCenter + (_rectSize / 2), 1, _xCenter + (_rectSize / 2), Size.Height - 2);
        g.DrawLine(pen, _xCenter - _rectSize + 1, _yCenter + (_rectSize / 2), _xCenter + (_rectSize * 2) - 1, _yCenter + (_rectSize / 2));

        pen.Dispose();
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);

        if (_left.Contains(e.Location))
            SelectedValue = IconAlignment.Left;
        else if (_right.Contains(e.Location))
            SelectedValue = IconAlignment.Right;
        else if (_top.Contains(e.Location))
            SelectedValue = IconAlignment.Top;
        else if (_bottom.Contains(e.Location))
            SelectedValue = IconAlignment.Bottom;

        ValueSelected?.Invoke(this, EventArgs.Empty);

        Invalidate();
    }
}