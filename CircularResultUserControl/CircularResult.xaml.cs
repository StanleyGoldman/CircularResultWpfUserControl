﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CircularResultUserControl
{
    /// <summary>
    /// Interaction logic for CircularResult.xaml
    /// </summary>
    public partial class CircularResult : UserControl
    {
        public static readonly DependencyProperty ErrorCountProperty = DependencyProperty.Register(
            "ErrorCount", typeof(int), typeof(CircularResult), 
            new PropertyMetadata(0, (d, args) => ((CircularResult)d).ErrorCount = (int)args.NewValue));

        public static readonly DependencyProperty SuccessCountProperty = DependencyProperty.Register(
            "SuccessCount", typeof(int), typeof(CircularResult), 
            new PropertyMetadata(0, (d, args) => ((CircularResult)d).SuccessCount = (int)args.NewValue));

        public static readonly DependencyProperty PendingCountProperty = DependencyProperty.Register(
            "PendingCount", typeof(int), typeof(CircularResult), 
            new PropertyMetadata(0, (d, args) => ((CircularResult)d).PendingCount = (int)args.NewValue));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(CircularResult), 
            new PropertyMetadata((double)250, (d, args) => ((CircularResult)d).Radius = (double)args.NewValue));

        public static readonly DependencyProperty InnerRadiusProperty = DependencyProperty.Register(
            "InnerRadius", typeof(double), typeof(CircularResult), 
            new PropertyMetadata((double)200, (d, args) => ((CircularResult)d).InnerRadius = (double)args.NewValue));

        public IEnumerable<Point> GeneratePoints(float percentage)
        {
            double ToRadians(float val)
            {
                return (Math.PI / 180) * val;
            }

            if (float.IsNaN(percentage))
            {
                return Array.Empty<Point>();
            }

            if (percentage < 0 || percentage > 1)
            {
                throw new ArgumentException();
            }

            var diameter = Diameter;

            var leftEdge = XAdjust;
            var rightEdge = diameter + XAdjust;
            var topEdge = YAdjust;
            var bottomEdge = diameter + YAdjust;

            var topMiddle = new Point(Origin.X, topEdge);
            var topRight = new Point(rightEdge, topEdge);
            var bottomRight = new Point(rightEdge, bottomEdge);
            var bottomLeft = new Point(leftEdge, bottomEdge);
            var topLeft = new Point(leftEdge, topEdge);

            if (percentage == 1)
            {
                return new[] { topLeft, topRight, bottomRight, bottomLeft };
            }

            var degrees = percentage * 360;
            var adjustedDegrees = (degrees + 90) % 360;

            if (adjustedDegrees >= 90 && adjustedDegrees < 135)
            {
                var angleDegrees = adjustedDegrees - 90;
                var angleRadians = ToRadians(angleDegrees);
                var tan = Math.Tan(angleRadians);
                var oppositeEdge = tan * Radius;
                return new[] { Origin, topMiddle, new Point(topMiddle.X + oppositeEdge, topMiddle.Y) };
            }

            if (adjustedDegrees >= 135 && adjustedDegrees < 180)
            {
                var angleDegrees = adjustedDegrees - 135;
                var angleRadians = ToRadians(angleDegrees);
                var tan = Math.Tan(angleRadians);
                var oppositeEdge = tan * Radius;
                return new[] { Origin, topMiddle, topRight, new Point(topRight.X, topRight.Y + oppositeEdge) };
            }

            if (adjustedDegrees >= 180 && adjustedDegrees < 225)
            {
                var angleDegrees = adjustedDegrees - 180;
                var angleRadians = ToRadians(angleDegrees);
                var tan = Math.Tan(angleRadians);
                var oppositeEdge = tan * Radius;
                return new[] { Origin, topMiddle, topRight, new Point(topRight.X, topRight.Y + Radius + oppositeEdge) };
            }

            if (adjustedDegrees >= 225 && adjustedDegrees < 270)
            {
                var angleDegrees = adjustedDegrees - 225;
                var angleRadians = ToRadians(angleDegrees);
                var tan = Math.Tan(angleRadians);
                var oppositeEdge = tan * Radius;
                return new[] { Origin, topMiddle, topRight, bottomRight, new Point(bottomRight.X - oppositeEdge, bottomRight.Y) };
            }

            if (adjustedDegrees >= 270 && adjustedDegrees < 315)
            {
                var angleDegrees = adjustedDegrees - 270;
                var angleRadians = ToRadians(angleDegrees);
                var tan = Math.Tan(angleRadians);
                var oppositeEdge = tan * Radius;
                return new[] { Origin, topMiddle, topRight, bottomRight, new Point(bottomRight.X - Radius - oppositeEdge, bottomRight.Y) };
            }

            if (adjustedDegrees >= 315 && adjustedDegrees < 360)
            {
                var angleDegrees = adjustedDegrees - 315;
                var angleRadians = ToRadians(angleDegrees);
                var tan = Math.Tan(angleRadians);
                var oppositeEdge = tan * Radius;
                return new[] { Origin, topMiddle, topRight, bottomRight, bottomLeft, new Point(bottomLeft.X, bottomLeft.Y - oppositeEdge) };
            }

            if (adjustedDegrees >= 0 && adjustedDegrees < 45)
            {
                var angleDegrees = adjustedDegrees;
                var angleRadians = ToRadians(angleDegrees);
                var tan = Math.Tan(angleRadians);
                var oppositeEdge = tan * Radius;
                return new[] { Origin, topMiddle, topRight, bottomRight, bottomLeft, new Point(bottomLeft.X, bottomLeft.Y - Radius - oppositeEdge) };
            }

            if (adjustedDegrees >= 45 && adjustedDegrees < 90)
            {
                var angleDegrees = adjustedDegrees - 45;
                var angleRadians = ToRadians(angleDegrees);
                var tan = Math.Tan(angleRadians);
                var oppositeEdge = tan * Radius;
                return new[] { Origin, topMiddle, topRight, bottomRight, bottomLeft, topLeft, new Point(topLeft.X + oppositeEdge, topLeft.Y) };
            }

            throw new InvalidOperationException();
        }

        public CircularResult()
        {
            InitializeComponent();
            GeneratePolygons();
            GenerateMask();
        }

        private void GeneratePolygons()
        {
            ErrorPolygon.Points = new PointCollection(GeneratePoints((float)ErrorCount / TotalCount));
            SuccessPolygon.Points = new PointCollection(GeneratePoints((float)(SuccessCount + ErrorCount) / TotalCount));
            PendingPolygon.Points = new PointCollection(GeneratePoints((float)(SuccessCount + ErrorCount + PendingCount) / TotalCount));
        }

        private void GenerateMask()
        {
            var pendingPolygonClip = new CombinedGeometry(
                GeometryCombineMode.Exclude,
                new EllipseGeometry(Origin, Radius, Radius),
                new EllipseGeometry(Origin, InnerRadius, InnerRadius));

            PendingPolygon.Clip = pendingPolygonClip;
            SuccessPolygon.Clip = pendingPolygonClip;
            ErrorPolygon.Clip = pendingPolygonClip;
        }

        private Point Origin => new Point(Radius + XAdjust, Radius + YAdjust);

        private double Diameter => Radius * 2;

        private double XAdjust => (ActualWidth - Diameter) / 2;

        private double YAdjust => (ActualHeight - Diameter) / 2;

        private int TotalCount => ErrorCount + SuccessCount + PendingCount;

        public int ErrorCount
        {
            get => (int)GetValue(ErrorCountProperty);
            set
            {
                SetValue(ErrorCountProperty, value);
                GeneratePolygons();
            }
        }

        public int SuccessCount
        {
            get => (int)GetValue(SuccessCountProperty);
            set
            {
                SetValue(SuccessCountProperty, value);
                GeneratePolygons();
            }
        }

        public int PendingCount
        {
            get => (int)GetValue(PendingCountProperty);
            set
            {
                SetValue(PendingCountProperty, value);
                GeneratePolygons();
            }
        }

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set
            {
                SetValue(RadiusProperty, value);
                GenerateMask();
                GeneratePolygons();
            }
        }

        public double InnerRadius
        {
            get => (double)GetValue(InnerRadiusProperty);
            set
            {
                SetValue(InnerRadiusProperty, value);
                GenerateMask();
                GeneratePolygons();
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (sizeInfo.WidthChanged || sizeInfo.HeightChanged)
            {
                GenerateMask();
                GeneratePolygons();
            }
        }
    }
}
