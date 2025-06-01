using System.Reactive.Disposables;
using Reactive.Bindings;

namespace Btg.BrownianMotion.UI.Controls;

public class LinearChart : GraphicsView
{
    public static readonly BindableProperty ValuesProperty = BindableProperty.Create(propertyName: nameof(Values), returnType: typeof(double[]), declaringType: typeof(LinearChart), defaultValue: null, coerceValue: OnValuePropertyCoerce, propertyChanged: InvalidadeAsync);
    public static readonly BindableProperty StrokeSizeProperty = BindableProperty.Create(propertyName: nameof(StrokeSize), returnType: typeof(float), declaringType: typeof(LinearChart), defaultValue: 20f, propertyChanged: InvalidadeAsync);
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(propertyName: nameof(StrokeColor), returnType: typeof(Color), declaringType: typeof(LinearChart), defaultValue: Colors.CornflowerBlue, propertyChanged: InvalidadeAsync);
    public static readonly BindableProperty BackStrokeColorProperty = BindableProperty.Create(propertyName: nameof(BackStrokeColor), returnType: typeof(Color), declaringType: typeof(LinearChart), defaultValue: Colors.LightGray, propertyChanged: InvalidadeAsync);
    public static readonly BindableProperty AnimateCommandProperty = BindableProperty.Create(nameof(AnimateCommand), typeof(ReactiveCommand), declaringType: typeof(LinearChart), null, BindingMode.OneWayToSource);

    public LinearChart()
    {
        EndAngle = 0f;
        Drawable = new GraphicsDrawable(this);
        BackgroundColor = Colors.Transparent;
    }

    internal float EndAngle { get; private set; }

    public double[] Values
    {
        get => (double[])GetValue(ValuesProperty);
        set => SetValue(ValuesProperty, value);
    }

    public float StrokeSize
    {
        get => (float)GetValue(StrokeSizeProperty);
        set => SetValue(StrokeSizeProperty, value);
    }

    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    public Color BackStrokeColor
    {
        get => (Color)GetValue(BackStrokeColorProperty);
        set => SetValue(BackStrokeColorProperty, value);
    }

    public ReactiveCommand AnimateCommand
    {
        get => (ReactiveCommand)GetValue(AnimateCommandProperty);
        set => SetValue(AnimateCommandProperty, value);
    }

    private static void InvalidadeAsync(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is LinearChart control)
        {
            control.Invalidate();
        }
    }

    private static object OnValuePropertyCoerce(BindableObject _, object value)
    {
        if (value is double[] doubleValues)
        {
            return doubleValues;
        }
        else
        {
            return Array.Empty<double>();
        }
    }
}

public class GraphicsDrawable(LinearChart chart) : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var spacing = dirtyRect.Width / (chart.Values.Length - 1);
        var maxHeight = dirtyRect.Height;
        var startX = dirtyRect.Left;
        var startY = dirtyRect.Bottom;

        canvas.StrokeSize = chart.StrokeSize;
        canvas.StrokeColor = chart.StrokeColor;

        for (var i = 0; i < chart.Values.Length - 1; i++)
        {
            var x1 = startX + i * spacing;
            var y1 = startY - (float)(chart.Values[i] / chart.Values.Max() * maxHeight);
            var x2 = startX + (i + 1) * spacing;
            var y2 = startY - (float)(chart.Values[i + 1] / chart.Values.Max() * maxHeight);

            canvas.DrawLine(x1, y1, x2, y2);
        }
    }
}

//public class GraphicsDrawable(LinearChart chart) : IDrawable
//{
//    public void Draw(ICanvas canvas, RectF dirtyRect)
//    {
//        var x = chart.StrokeSize / 2;
//        var y = chart.StrokeSize / 2;
//        var size = Math.Min(dirtyRect.Width, dirtyRect.Height) - chart.StrokeSize;

//        canvas.StrokeSize = chart.StrokeSize;
//        canvas.StrokeColor = chart.BackStrokeColor;
//        canvas.DrawArc(x, y, size, size, 0, 359.9f, false, false);

//        canvas.Rotate(-90, dirtyRect.Center.X, dirtyRect.Center.Y);
//        canvas.StrokeColor = chart.StrokeColor;
//        canvas.DrawArc(x, y, size, size, 0, chart.EndAngle, true, false);
//    }
//}