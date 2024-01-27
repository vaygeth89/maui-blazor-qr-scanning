using ScanQr.App.Models;

namespace ScanQr.App.Components.Pages;


public partial class TodoItem : ContentView
{
    public TodoItem(Todo todo)
    {
        InitializeComponent();
        var horizontalStackLayout = new HorizontalStackLayout();
        List<View> views = new List<View>()
        {
            new Label()
            {
                Text = todo.Description
            },
            new CheckBox()
            {
                IsChecked = todo.IsCompleted
            }
        };
        horizontalStackLayout.Children.ToList().AddRange(views);
        Content = horizontalStackLayout;
    }
}