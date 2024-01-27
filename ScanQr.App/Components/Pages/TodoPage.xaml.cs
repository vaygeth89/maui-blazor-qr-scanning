
using ScanQr.App.Models;

namespace ScanQr.App.Components.Pages;

public partial class TodoPage : ContentPage
{
    private List<Todo> todos = new List<Todo>()
    {
        new ()
        {
            Description = "Sample Todo",
            IsCompleted = false
        },
        new Todo()
        {
            Description = "Another Todo",
            IsCompleted = true
        },
    };
    public TodoPage()
    {
        InitializeComponent();
        Content = new TodosList(todos);
    }
}