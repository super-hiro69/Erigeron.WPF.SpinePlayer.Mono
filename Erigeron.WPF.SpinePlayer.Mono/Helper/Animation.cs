using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace Erigeron.WPF.SpinePlayer.Mono.Helper
{
    class Animation
    {
        #region 添加double动画
        public static Storyboard AddDoubleAnimaton(double? to, double mstime, DependencyObject value, string PropertyPath, Storyboard? sb, double? from = null, double decelerationRatio = 0.9, double accelerationRatio = 0)
        {
            sb ??= new();
            DoubleAnimation? da = new();
            if (from != null)
                da.From = from;
            if (to != null)
                da.To = to;
            da.Duration = TimeSpan.FromMilliseconds(mstime);
            da.DecelerationRatio = decelerationRatio;
            da.AccelerationRatio = accelerationRatio;
            Storyboard.SetTarget(da, value);
            Storyboard.SetTargetProperty(da, new PropertyPath(PropertyPath));
            sb.Children.Add(da);
            sb.FillBehavior = FillBehavior.Stop;
            sb.Completed += (sender, args) =>
            {
                da = null;
                sb = null;
            };
            return sb;
        }
        #endregion

        #region 添加thickness动画
        public static Storyboard AddThicknessAnimaton(Thickness? to, double mstime, DependencyObject value, string PropertyPath, Storyboard? sb, Thickness? from = null, double DecelerationRatio = 0.9)
        {
            sb ??= new();
            ThicknessAnimation? da = new();
            if (from != null)
                da.From = from;
            if (to != null)
                da.To = to;
            da.Duration = TimeSpan.FromMilliseconds(mstime);
            da.DecelerationRatio = DecelerationRatio;
            Storyboard.SetTarget(da, value);
            Storyboard.SetTargetProperty(da, new PropertyPath(PropertyPath));
            sb.Children.Add(da);
            sb.FillBehavior = FillBehavior.Stop;
            sb.Completed += delegate
            {
                da = null;
                sb = null;
            };
            return sb;
        }
        #endregion 
    }
}
