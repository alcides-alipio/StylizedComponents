using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using StylizedComponents.Core.models;

public class IconRelationPicker : Control
{
    public event EventHandler ValueSelected;

    public IconAlignment SelectedValue;

    private Rectangle _left;
    private Rectangle _right;
    private Rectangle _top;
    private Rectangle _bottom;

    public IconRelationPicker()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.UserPaint, true);

        Size = new Size(120, 120);

        int w = 40;
        int h = 40;

        _left = new Rectangle(0, 40, w, h);
        _right = new Rectangle(80, 40, w, h);
        _top = new Rectangle(40, 0, w, h);
        _bottom = new Rectangle(40, 80, w, h);
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

        g.DrawLine(pen, 60, 0, 60, 120);
        g.DrawLine(pen, 0, 60, 120, 60);

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

        if (ValueSelected != null)
            ValueSelected(this, EventArgs.Empty);

        Invalidate();
    }
}