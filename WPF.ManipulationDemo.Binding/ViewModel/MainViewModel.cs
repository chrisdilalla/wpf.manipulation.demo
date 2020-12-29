using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using System.Windows.Media;
using System.Windows;

namespace WPF.ManipulationDemo.Binding.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public TransformGroup transformGroup;
        private TranslateTransform translation;
        private ScaleTransform scale;
        private RotateTransform rotation;
        private Transform transform;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                Initialize();
            }
        }

        private void Initialize()
        {
            RectWidth = 200;
            RectHeight = 200;


            transformGroup = new TransformGroup();

            translation = new TranslateTransform(0, 0);
            scale = new ScaleTransform(1, 1);
            rotation = new RotateTransform(0);


            transformGroup.Children.Add(rotation);
            transformGroup.Children.Add(scale);
            transformGroup.Children.Add(translation);
        }

        public double RectWidth { get; set; }
        public double RectHeight { get; set; }

        public Transform RenderTransform
        {
            get
            {
                return transformGroup;
            }
            set
            {
                //if (value == transform) return;
                //transform = value;
                //RaisePropertyChanged<Transform>();
            }
        }

        public RelayCommand<ManipulationDeltaEventArgs> ManipulationDelta
            => new RelayCommand<ManipulationDeltaEventArgs>(OnManipulationDelta);

        private void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            Point center = new Point(RectWidth / 2.0, RectHeight / 2.0);
            // apply the rotation at the center of the rectangle if it has changed
            rotation.CenterX = center.X;
            rotation.CenterY = center.Y;
            rotation.Angle += e.DeltaManipulation.Rotation;

            // Scale is always uniform, by definition, so the x and y will always have the same magnitude
            scale.CenterX = center.X;
            scale.CenterY = center.Y;
            scale.ScaleX *= e.DeltaManipulation.Scale.X;
            scale.ScaleY *= e.DeltaManipulation.Scale.Y;

            // apply translation
            translation.X += e.DeltaManipulation.Translation.X;
            translation.Y += e.DeltaManipulation.Translation.Y;



            //// apply the rotation at the center of the rectangle if it has changed
            //rotation.CenterX = e.ManipulationOrigin.X;
            //rotation.CenterY = e.ManipulationOrigin.Y;
            //rotation.Angle += e.DeltaManipulation.Rotation;

            //// Scale is always uniform, by definition, so the x and y will always have the same magnitude
            //scale.CenterX = e.ManipulationOrigin.X;
            //scale.CenterY = e.ManipulationOrigin.Y;
            //scale.ScaleX *= e.DeltaManipulation.Scale.X;
            //scale.ScaleY *= e.DeltaManipulation.Scale.Y;

            ////// apply translation
            //translation.X += e.DeltaManipulation.Translation.X;
            //translation.Y += e.DeltaManipulation.Translation.Y;
        }
    }
}