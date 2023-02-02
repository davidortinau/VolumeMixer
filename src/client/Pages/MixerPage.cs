using CommunityToolkit.Maui.Markup;
using VolumeMixer.Resources.Styles;

namespace VolumeMixer.Pages;

public class MixerPage : ContentPage
{
    #region Styles
    ResourceDictionary createPageStyles()
    {
        Style<Slider> sliderStyle = new Style<Slider>(
        (Slider.RotationProperty, -90),
        (Slider.MinimumProperty, 0),
        (Slider.MaximumProperty, 100),
        (Slider.ThumbColorProperty, Color.FromArgb("#000000")),
        (Slider.MinimumTrackColorProperty, Color.FromArgb("#FF3300")),
        (Slider.MaximumTrackColorProperty, Color.FromArgb("#d1d1d1")),
        (Slider.VerticalOptionsProperty, LayoutOptions.Center)
    );

        Style<Button> buttonStyle = new Style<Button>(
            (Button.TextColorProperty, Colors.White),
            (Button.FontFamilyProperty, "FontAwesome"),
            (Button.FontSizeProperty, 28),
            (Button.BackgroundProperty, Colors.Transparent)
        );

        Style<Label> labelStyle = new Style<Label>(
            (Label.TextColorProperty, Colors.White),
            (Label.HorizontalOptionsProperty, LayoutOptions.Center)
        );

        return new ResourceDictionary { sliderStyle, buttonStyle, labelStyle };
    }
    #endregion

    enum Column { Slider1, Slider2, Slider3, Slider4 }
    enum Row { NavBar, Sliders, Buttons, Labels }

    void Build() {
        this.Resources = createPageStyles();

        this.BackgroundColor = Color.FromArgb("#1d1d1d");

        Content =
            new Grid
            {
                ColumnDefinitions = Columns.Define(
                    (Column.Slider1, Star),
                    (Column.Slider2, Star),
                    (Column.Slider3, Star),
                    (Column.Slider4, Star)),
                RowDefinitions = Rows.Define(
                    (Row.NavBar, 80),
                    (Row.Sliders, Star),
                    (Row.Buttons, 150),
                    (Row.Labels,40)
                ),

                Children =
                { 
                    new Slider()
                        .Column(Column.Slider1)
                        .Row(Row.Sliders),
                    new Slider()
                        .Column(Column.Slider2)
                        .Row(Row.Sliders),
                    new Slider()
                        .Column(Column.Slider3)
                        .Row(Row.Sliders),
                    new Slider()
                        .Column(Column.Slider4)
                        .Row(Row.Sliders),

                    new Button()
                        .Column(Column.Slider1)
                        .Row(Row.Buttons)
                        .Text(IconFont.VolumeUp),
                    new Button()
                        .Column(Column.Slider2)
                        .Row(Row.Buttons)
                        .Text(IconFont.VolumeMute),
                    new Button()
                        .Column(Column.Slider3)
                        .Row(Row.Buttons)
                        .Text(IconFont.VolumeMute),
                    new Button()
                        .Column(Column.Slider4)
                        .Row(Row.Buttons)
                        .Text(IconFont.VolumeUp),

                    new Label()
                        .Column(Column.Slider1)
                        .Row(Row.Labels)
                        .Text("Teams"),
                    new Label()
                        .Column(Column.Slider2)
                        .Row(Row.Labels)
                        .Text("Spotify"),
                    new Label()
                        .Column(Column.Slider3)
                        .Row(Row.Labels)
                        .Text("Discord"),
                    new Label()
                        .Column(Column.Slider4)
                        .Row(Row.Labels)
                        .Text("Main"),

                    new Button()
                        .Text(IconFont.Plug)
                        .ColumnSpan(4)
                        .Size(64,64)
                        .End()
                }
            };
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        BindingContext = new MainViewModel();

        Build();

#if DEBUG
        HotReloadService.UpdateApplicationEvent += ReloadUI;
#endif
    }
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

#if DEBUG
        HotReloadService.UpdateApplicationEvent -= ReloadUI;
#endif
    }

    private void ReloadUI(Type[] obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Build();
        });
    }
}
