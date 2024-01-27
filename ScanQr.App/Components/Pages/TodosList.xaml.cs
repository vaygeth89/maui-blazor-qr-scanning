
using ScanQr.App.Components.Pages;
using ScanQr.App.Models;

namespace ScanQr.App.Components.Pages;
public partial class TodosList :ContentView
{
    protected IEnumerable<Todo> _todos = new List<Todo>();

    public TodosList(IEnumerable<Todo> todos)
    {
        _todos = todos;

        var verticalStackLayout = new VerticalStackLayout();
        verticalStackLayout.Children.ToList().AddRange(_todos.Select(todo => new TodoItem(todo)));
        Content = verticalStackLayout;
    }

    public TodosList()
    {
        
    }
}